using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{

   enum MoveDirection
    {
        Left,
        Right,
        Up,
        Down,
    };
   
    void Start()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager not found!");
            return;
        }
        
        row = defaultPos[0];
        col = defaultPos[1];
    }
   
    private float elapsedTime;
    [SerializeField] private float referenceValue = default;
    void Update()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager not found!");
            return;
        }
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= referenceValue)
        {
            MoveEnemy();
            elapsedTime = 0f;
        }
    }
    
    private int direction;
    [Header("0:row 1:col")]
    [SerializeField] private int[] defaultPos;
    private int row;
    private int col;
  
    private void MoveEnemy()
    {
        do
        {
            //盤面の更新
            // obj = GameObject.Find("GameManager");
            // _GM = obj.GetComponent<GameManager>();
            // _GM = _GM.GetComponent<GameManager>();
            int newRow = row;
            int newCol = col;
            direction = Random.Range(0, 4);

            if (direction == (int)MoveDirection.Left)
            {
                newCol--;
            }
            else if (direction == (int)MoveDirection.Right)
            {
                newCol++;
            }
            else if (direction == (int)MoveDirection.Up)
            {
                newRow--;
            }
            else if (direction == (int)MoveDirection.Down)
            {
                newRow++;
            }
            else
            {
                Debug.Log("Error");
            }
            
            if (CanMoveCheck(newRow, newCol))
            {
                row = newRow;
                col = newCol;
                SetPos();
                break;
            }
            

            // if (CheckSand() == true)
            // {
            //     _GM.Clear();
            //     break;
            // }
            //     
        
        } while (CheckSand() == false);
        
        if (CheckSand() == true)
            GameManager.instance.Clear();
    }

    private bool CanMoveCheck(int row, int col)
    {
        //盤面の更新
        /*obj = GameObject.Find("GameManager");
        _GM = obj.GetComponent<GameManager>(); */
        if (GameManager.instance == null)
        {
            return false;
        }
        
        if (row < 0 || 4 < row)
            return false;

        if (col < 0 || 4 < col)
            return false;
        
        if (GameManager.instance.mass[row, col] == true) // 色を変えた的の条件を確認
            return false;

        return true;

    }

     [SerializeField] private float xValue = default!;
     [SerializeField] private float yValue = default!;
    private void SetPos()
    {
        
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
        
        //盤面の座標とunity上の座標は符号逆
        if (direction == (int) MoveDirection.Left)
        {
            pos.x -= xValue;
        }
        else if (direction == (int) MoveDirection.Right)
        {
            pos.x += xValue;
        }
        else if (direction == (int) MoveDirection.Up)
        {
            pos.y += yValue;
        }
        else if (direction == (int) MoveDirection.Down)
        {
            pos.y -= yValue;
        }

        myTransform.position = pos;
        Debug.Log("row = " + row);
        Debug.Log("col = " + col);
    }

   public bool CheckSand()
    {
        /*obj = GameObject.Find("GameManager");
        _GM = obj.GetComponent<GameManager>();*/
        
        //敵の位置から四方向CanMoveCheck()を実行してすべてfalseが帰ってくる＝挟まれている
        if (CanMoveCheck(row - 1, col) == true) //上
            return false;
        if (CanMoveCheck(row + 1, col) == true) //下
            return false;
        if (CanMoveCheck(row, col - 1) == true) //左
            return false;
        if (CanMoveCheck(row, col + 1) == true) //右
            return false;

        return true;
    }

}
