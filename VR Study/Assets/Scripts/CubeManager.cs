using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private Material hitMaterial = default!;
    [SerializeField] int row = default!;
    [SerializeField] int col = default!;
    [SerializeField] private GameManager _gameManager;
    private Material defaultMaterial;
    void Start()
    {
        defaultMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        
    }
    [SerializeField] private AudioClip HitS = default!;
    [SerializeField] private Enemy _enemy = default!;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            GameManager.instance.PlaySE(HitS);

            if (_enemy.CheckSand() == true)
            {
                //クリア   
                GameManager.instance.Clear();
            }
            
          　 //色替えと盤面チェック
            if (GameManager.instance.mass[row, col] == false)
            {
                GetComponent<Renderer>().material = hitMaterial;
                GameManager.instance.mass[row, col] = true;
                GameManager.instance.CheckBoard();
            }
            else
            {
                GetComponent<Renderer>().material = defaultMaterial;
                GameManager.instance.mass[row, col] = false;
            }
        
            //弾を消すのと次の発射準備
            Destroy(other.gameObject);
            GameManager.instance.RandomGenerateTiming();
        }
    }
}
