using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Vector3 powerDirection = default!;
    [SerializeField] private float moveSpeed = 3.0f;

    private Vector3 defaultPos;
    // Start is called before the first frame update
    void Start()
    {
        defaultPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //左右の往復移動 movespeedで調整
        transform.position = new Vector3(Mathf.Sin(Time.time) * moveSpeed, defaultPos.y, defaultPos.z);
    }

    public void BallGenerateAndShoot()
    {
        GameObject ball;
        Rigidbody ballRigidbody;
        
        ball = Instantiate(ballPrefab, SpawnPoint.position, Quaternion.identity);

        ballRigidbody = ball.GetComponent<Rigidbody>();
            
        ballRigidbody.AddForce(powerDirection, ForceMode.Impulse);
        
    }
}
