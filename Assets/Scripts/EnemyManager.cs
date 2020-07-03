using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public Transform[] SpawnPos;
    public Transform TheEnemyPrefab;
    public Transform ThePlayer;

    private float timeSpawn = 5f;
    private float timer = 2f;

    private int rangeRandom;
    public static int ENEMY_LIMIT = 10;

    private int spawnCount = 1;

    void Start()
    {
        rangeRandom = SpawnPos.Length;
    }

    void ResetEM()
    {
        timeSpawn = 5f;
        timer = 0;
        ENEMY_LIMIT = 10;
        spawnCount = 1;
    }

    void OnGameOver()
    {
        if (GameManager.GameOver)
        {
            ResetEM();
            gameObject.SetActive(false);
        }
    }

    void OnWaveOver()
    {
        if (GameManager.WaveOver)
        {
            int wave = GameManager.WAVE;
            ENEMY_LIMIT = 10 + wave * 5;
            timeSpawn = 5 - wave * 0.5f;
            if(timeSpawn <= 2)
            {
                timeSpawn = 2;
            }
            spawnCount = wave * 2;
            gameObject.SetActive(false);
        }
    }

    void Update()
    {

        OnGameOver();

        OnWaveOver();

        if (ENEMY_LIMIT <= 0) return;

        timer += Time.deltaTime;
        if(timer >= timeSpawn)
        {
            timer = 0;

            Vector3 previousPos = new Vector3(0,0,0);
            Vector3 pos = previousPos;
            for(int i = 0;i< Random.Range(1,spawnCount+1); i++)
            {
                while(previousPos == pos)
                {
                    pos = SpawnPos[Random.Range(0, rangeRandom)].position;
                }
                Transform theEnemy = (Transform)Instantiate(TheEnemyPrefab,pos, Quaternion.identity);
                theEnemy.GetComponent<NavMeshAgent>().SetDestination(ThePlayer.position);
                previousPos = pos;
                ENEMY_LIMIT--;
                if(ENEMY_LIMIT <= 0)
                {
                    break;
                }
            }
        }
        
    }
}
