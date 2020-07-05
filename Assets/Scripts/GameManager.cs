using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static float move_velocity = 5.0f;

    public static int speed_level = 0;
    public static int damage_level = 0;
    public static float[] DAMAGE_VALUE = { 1, 1.5f, 2, 3, 5, 8 };
    public static int[] DAMAGE_PRICE = { 5, 10, 250, 500, 800, 1500 };
    public static float[] SPEED_VALUE = { 1.5f, 1.2f, 1.0f, 0.8f, 0.6f, 0.4f };
    public static int[] SPEED_PRICE = { 100, 200, 500, 800, 1200, 2000 };
    public Color[] LESSER_COLOR;
    public Color[] GUN_COLOR;
    public Text damageValueText;
    public Text damagePriceText;
    public Text speedValueText;
    public Text speedPriceText;


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
        GvrViewer.Instance.VRModeEnabled = !GvrViewer.Instance.VRModeEnabled;
        LESSER_COLOR = new Color[6];
        LESSER_COLOR[0] = new Color(0.823529411765f, 0.873529411765f, 0.823529411765f, 1f);
        LESSER_COLOR[1] = new Color(0.2f, 1f, 0.270588235294f, 1f);
        LESSER_COLOR[2] = new Color(0.376470588235f, 0.972549019608f, 1f, 1f);
        LESSER_COLOR[3] = new Color(0.701960784314f, 0.227450980392f, 1f, 1f);
        LESSER_COLOR[4] = new Color(1f, 0.823529411765f, 0.360784313725f, 1f);
        LESSER_COLOR[5] = new Color(1f, 0.376470588235f, 0.388235294118f, 1f);
        GUN_COLOR = new Color[6];
        GUN_COLOR[0] = new Color(0.9215294f, 0.9215294f, 0.9215294f);
        GUN_COLOR[1] = new Color(0f, 1.492f, 0.3510663f);
        GUN_COLOR[2] = new Color(0f, 1.306786f, 1.492f);
        GUN_COLOR[3] = new Color(0.730566f, 0f, 1.492f);
        GUN_COLOR[4] = new Color(1.492f, 0.7099862f, 0f);
        GUN_COLOR[5] = new Color(1.492f, 0f, 0f);
        GameObject.FindGameObjectWithTag("Lesser").gameObject.GetComponent<Renderer>().material.SetColor("_Color", LESSER_COLOR[damage_level]);
        GameObject.FindGameObjectWithTag("Gun").gameObject.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", GUN_COLOR[damage_level]);

    }

    public void BuyDamage()
    {
        if(MONEY >= DAMAGE_PRICE[damage_level])
        {
            MONEY -= DAMAGE_PRICE[damage_level];
            damage_level++;
            Debug.Log(LESSER_COLOR[damage_level]);
            Debug.Log(new Color(0.2f, 1f, 0.270588235294f, 1f));
            GameObject.FindGameObjectWithTag("Lesser").gameObject.GetComponent<Renderer>().material.SetColor("_Color", LESSER_COLOR[damage_level]);
            GameObject.FindGameObjectWithTag("Gun").gameObject.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", GUN_COLOR[damage_level]);

        }
    }

    public void BuySpeed()
    {
        if (MONEY >= SPEED_PRICE[speed_level])
        {
            MONEY -= SPEED_PRICE[speed_level];
            speed_level++;
        }
    }


    void UpdateText()
    {
        LifeText.text = "" + LIFE;
        WaveText.text = "" + WAVE;
        KillText.text = "" + KILLS;
        EnemyText.text = EnemyManager.ENEMY_LIMIT + "/" + EnemyLimit;
        speedValueText.text = "" + SPEED_VALUE[speed_level];
        speedPriceText.text = "" + SPEED_PRICE[speed_level];
        damageValueText.text = "" + DAMAGE_VALUE[damage_level];
        damagePriceText.text = "" + DAMAGE_PRICE[damage_level];
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
