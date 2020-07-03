using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Button TheButton;

    public static int LIFE = 10;
    public GameObject Life_Canvas;
    public Text LifeText;

    public static int WAVE = 1;
    public GameObject Wave_Canvas;
    public Text WaveText;

    public static int KILLS = 0;
    public GameObject KILL_Canvas;
    public Text KillText;
    public int EnemyLimit;

    public static int MONEY = 0;

    public GameObject Enemy_Canvas;
    public Text EnemyText;

    public Text countdownText;
    public int counter = 3;

    public GameObject EM;
    
    private float time = 1;
    private float timer = 0;

    private bool StartCount = false;
    public static bool GameOver = false;
    public static bool WaveOver = false;

    public bool GameStarted = false;
    public GameObject Restart_Canvas;

    

    void Start()
    {
        
    }

    void UpdateText()
    {
        LifeText.text = "" + LIFE;
        WaveText.text = "" + WAVE;
        KillText.text = "" + KILLS;
        EnemyText.text = EnemyManager.ENEMY_LIMIT + "/" + EnemyLimit;
    }

    void IfGameOver()
    {
        if(LIFE <= 0)
        {
            GameOver = true;
        }
    }

    void OnCounter()
    {
        if (StartCount && counter > -1)
        {
            timer += Time.deltaTime;
            if (timer >= time)
            {
                timer = 0;
                counter--;
                countdownText.text = (counter <= 0 ? "GO!!" : "" + counter);
                GameStarted = true;
            }
        }

        if (counter <= -1 && StartCount)
        {
            countdownText.text = "";
            StartCount = false;
            EM.SetActive(true);
            Life_Canvas.SetActive(true);
            Wave_Canvas.SetActive(true);
            Enemy_Canvas.SetActive(true);

            EnemyLimit = EnemyManager.ENEMY_LIMIT;
            KILL_Canvas.SetActive(true);
        }
    }

    void checkEnemyonStage()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemyCount <= 0 && EnemyManager.ENEMY_LIMIT <= 0 && !WaveOver)
        {
            Enemy_Canvas.SetActive(false);
            WaveOver = true;
            WAVE++;
            GameStarted = false;
        }
    }

    void OnWaveOver()
    {
        if (WaveOver && !GameOver)
        {
            TheButton.GetComponentInChildren<Text>().text = "Start wave " + WAVE;
            TheButton.gameObject.SetActive(true);
        }
    }

    public void ResetToNextWave()
    {
        if (WaveOver)
        {
            WaveOver = false;
            GameStarted = false;
            counter = 3;
            StartCount = true;
        }

    }

    public void ResetGame()
    {
        LIFE = 10;
        WAVE = 1;
        KILLS = 0;
        counter = 3;
        SceneManager.LoadScene(0);
    }

    void OnGameOver()
    {
        if (GameOver)
        {
            EM.SetActive(false);
            Life_Canvas.SetActive(false);
            //Wave_Canvas.SetActive(false);
            Enemy_Canvas.SetActive(false);
            //KILL_Canvas.SetActive(false);
            Restart_Canvas.SetActive(true);
        }
    }
    
	void Update () {

        UpdateText();

        IfGameOver();

        OnCounter();

        if(GameStarted)
        checkEnemyonStage();

        OnWaveOver();

        OnGameOver();
        
	}

    public void Count()
    {
        StartCount = true;
    }
}
