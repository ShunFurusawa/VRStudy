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
    void Start()
    {
        
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
                _gameManager.Clear();
            }
            
          　 //色替えと盤面チェック
            GetComponent<Renderer>().material = hitMaterial;
            _gameManager.mass[row, col] = true;
            _gameManager.CheckBoard();
            
            //弾を消すのと次の発射準備
            Destroy(other.gameObject);
            _gameManager.RandomGenerateTiming();
        }
    }
}
