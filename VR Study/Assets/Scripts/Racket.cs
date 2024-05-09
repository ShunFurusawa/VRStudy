using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Racket : MonoBehaviour
{
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float power = 0.01f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            GameObject ball;
            Rigidbody ballRigidbody;
            
            //Aボタンが押されたときの処理
            Debug.Log("A");

            ball = Instantiate(ballPrefab, SpawnPoint.position, Quaternion.identity);

            ballRigidbody = ball.GetComponent<Rigidbody>();
            
            ballRigidbody.AddForce(new Vector3(0f, power, 0f), ForceMode.Impulse);

        }
    }
}
