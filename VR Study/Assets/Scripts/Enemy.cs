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
        row = defaultPos[0];
        col = defaultPos[1];
    }
   
    private float elapsedTime;
    [SerializeField] private float referenceValue = default;
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= referenceValue)
        {
            MoveEnemy();
            elapsedTime = 0f;
        }
    }
    
    [SerializeField] private GameManager _GM;
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
            _GM = _GM.GetComponent<GameManager>();

            direction = Random.Range(0, 4);

            if (direction == (int)MoveDirection.Left)
            {
                col--;
            }
            else if (direction == (int)MoveDirection.Right)
            {
                col++;
            }
            else if (direction == (int)MoveDirection.Up)
            {
                row--;
            }
            else if (direction == (int)MoveDirection.Down)
            {
                row++;
            }
            else
            {
                Debug.Log("Error");
            }

            if (CanMoveCheck(row, col) == true)
            {
                SetPos();
                break;
            }
            
        } while (true);
    }

    private bool CanMoveCheck(int row, int col)
    {
        
        if (row < 0 || 4 < row)
            return false;

        if (col < 0 || 4 < col)
            return false;
        
        if (_GM.mass[row, col] == true)
            return false;

        return true;

    }

     [SerializeField] private float xValue = default!;
     [FormerlySerializedAs("zValue")] [SerializeField] private float yValue = default!;
    private void SetPos()
    {
        Debug.Log("row = " + row);
        Debug.Log("col = " + col);
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
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
            pos.y -= yValue;
        }
        else if (direction == (int) MoveDirection.Down)
        {
            pos.y += yValue;
        }

        myTransform.position = pos;
    }
}
