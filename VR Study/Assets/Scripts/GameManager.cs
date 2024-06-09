using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Cannon _canon;
    private bool isballDeleted = false;
    private float randNextGenerateTime;
    private float elapsedTime;
    public int remainingAmmo = 10;
    private const int BOARD_SIZE = 3;
    public bool[,] mass;
    void Start()
    {
        RandomGenerateTiming();
        isballDeleted = true;
        remainingAmmo--;
        
        //ボード初期化
        mass = new bool[BOARD_SIZE, BOARD_SIZE];
       for(int row = 0; row < BOARD_SIZE; row++)
        {
            for (int col = 0; col < BOARD_SIZE; col++)
            {
                mass[row, col] = false;
            }
        }
    }

    void Update()
    {
        if (isballDeleted == false)
            return;
        
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= randNextGenerateTime)
        {
            //弾発射の一連処理
            RandomGenerateTiming();     //次の生成タイミングを設定
            _canon.BallGenerateAndShoot();
            isballDeleted = false;
            remainingAmmo--;
            elapsedTime = 0;
        }
        
    }

    public void RandomGenerateTiming()
    {
        //ランダムな生成タイミングの更新とボールが消えたことのお知らせを兼ねている
        isballDeleted = true;
        if (remainingAmmo <= 0)
        {
            Fail();
        }
        randNextGenerateTime = Random.Range(3.0f, 7.0f);
    }

    private void Fail()
    {
        Debug.Log("Fail");
        Time.timeScale = 0;
    }

    private void Clear()
    {
        Debug.Log("Clear");
        Time.timeScale = 0;
    }
    public void CheckBoard()
    {
        //横
        for(int row = 0; row < BOARD_SIZE; row++)
        {
            //最初がfalseだったら次の行へ
            if (mass[row, 0] == false)
                continue;
            
            //列を調べる。falseがある(当たってない箱がある)と次の行へ
            int col = 1;
            for (; col < BOARD_SIZE; col++)
            {
                if (mass[row, col] == false)
                    break;
            }
            
            //breakされてなければ一列光ってるからクリア
            if (col == BOARD_SIZE)
                Clear();
        }
        
        //縦
        for(int col = 0; col < BOARD_SIZE; col++)
        {
            if (mass[0, col] == false) 
                continue;
            
            int row = 1;
            for (; row < BOARD_SIZE; row++)
            {
                if (mass[row, col] == false) 
                    break;
            }
           
            if (row == BOARD_SIZE)
                Clear();
        }

        //斜め
        {
            //左上から調べる
            if (mass[0, 0] == true)
            {
                int idx = 1;
                for (; idx < BOARD_SIZE; idx++)
                {
                    if (mass[idx, idx] == false)
                        break;
                }

                if (idx == BOARD_SIZE)
                    Clear();
            }
        }
        {
            //左下から調べる
            if (mass[BOARD_SIZE - 1, 0] == true)
            {
                int idx = 1;
                for (; idx < BOARD_SIZE; idx++)
                {
                    if (mass[idx - 1 - idx, idx] == false)
                        break;
                }

                if (idx == BOARD_SIZE)
                    Clear();
            }
        }
    
    }

}
