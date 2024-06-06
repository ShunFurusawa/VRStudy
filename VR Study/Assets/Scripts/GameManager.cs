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
    // Start is called before the first frame update
    void Start()
    {
        RandomGenerateTiming();
    }

    // Update is called once per frame
    void Update()
    {
        if (isballDeleted == true)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= randNextGenerateTime)
            {
                _canon.BallGenerateAndShoot();
                isballDeleted = false;
            }
        }
    }

    public void RandomGenerateTiming()
    {
        isballDeleted = true;
        randNextGenerateTime = Random.Range(5.0f, 10.0f);
    }
}
