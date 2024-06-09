using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeChangeColor : MonoBehaviour
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            GetComponent<Renderer>().material = hitMaterial;
            _gameManager.mass[row, col] = true;
            _gameManager.CheckBoard();
        }
    }
}
