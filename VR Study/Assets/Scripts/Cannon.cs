using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Vector3 powerDirection = default!;
    [SerializeField] private float moveSpeed = 3.0f;
    public GameObject ball;
    private Vector3 defaultPos;
    private float elapsedTime = 0f;
   
    // Start is called before the first frame update
    void Start()
    {
        defaultPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (ball == isActiveAndEnabled)
        {
           // Debug.Log("Active!");
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 4f)
            {
                Destroy(ball);
                elapsedTime = 0f;
               // Debug.Log("Deleted!");
                _gameManager.RandomGenerateTiming();
            }
        }
        else
        {
            //Debug.Log("notActive");
            elapsedTime = 0f;
        }
    }

    private void FixedUpdate()
    {
        //左右の往復移動 movespeedで調整
        transform.position = new Vector3(Mathf.Sin(Time.time) * moveSpeed, defaultPos.y, defaultPos.z);
    }

    public void BallGenerateAndShoot()
    {
       
        Rigidbody ballRigidbody;
        
        ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);

        ballRigidbody = ball.GetComponent<Rigidbody>();
            
        ballRigidbody.AddForce(powerDirection, ForceMode.Impulse);
        
    }
}
