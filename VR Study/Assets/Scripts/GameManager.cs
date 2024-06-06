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
    private GameObject[] balls;
    
    // Start is called before the first frame update
    void Start()
    {
        RandomGenerateTiming();
        isballDeleted = true;
        remainingAmmo--;
    }

    // Update is called once per frame
    void Update()
    {
        if (isballDeleted == false)
            return;
        
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= randNextGenerateTime)
        {
            RandomGenerateTiming();
            _canon.BallGenerateAndShoot();
            isballDeleted = false;
            remainingAmmo--;
            elapsedTime = 0;
        }
        
    }

    public void RandomGenerateTiming()
    {
        //ランダムな経過時間の基準値の更新とボールが消えたことのお知らせを兼ねている
        isballDeleted = true;
        if (remainingAmmo <= 0)
        {
            Finish();
        }
        randNextGenerateTime = Random.Range(3.0f, 7.0f);
    }

    private void Finish()
    {
        Time.timeScale = 0;
    }

}
