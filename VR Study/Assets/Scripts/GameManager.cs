using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    [SerializeField] private Cannon _canon;
    private bool isballDeleted = false;
    private float randNextGenerateTime;
    private float elapsedTime;
    public int remainingAmmo = 10;
    private const int BOARD_SIZE = 5;
    public bool[,] mass;
    
    [Header("SE用audiosource")][SerializeField] public AudioSource SE_AudioSource = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
       // Time.timeScale = 0f;
    }

    void Start()
    {
        RandomGenerateTiming();
        isballDeleted = true;
        remainingAmmo--;
        restartButton.SetActive(false);
        endButton.SetActive(false);
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
    public void PlaySE(AudioClip clip)
    {
        if (SE_AudioSource != null && clip != null)
        {
            SE_AudioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("オーディオソースが設定されていません");
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

   
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject endButton;
    public void StartButton()
    {
        Time.timeScale = 1f;
        startButton.SetActive(false);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndButton()
    {
    #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
    #else
       Application.Quit();//ゲームプレイ終了
    #endif
    }
    private void Fail()
    {
        restartButton.SetActive(true);
        endButton.SetActive(true);
        Debug.Log("Fail");
        Time.timeScale = 0;
    }

    private void Clear()
    {
        restartButton.SetActive(true);
        endButton.SetActive(true);
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
