using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy_Spawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform SpawnPoint;

    public TextMeshProUGUI Wave_Incoming_Text;

    public float timeBetweenWaves = 5f;
    public float countdown = 2f;

    public int waveIndex = 0;
    private int last_waveIndex;

    [Header("Enemy_Wave_Health")]
    //public float TotalenemyAddHealth;
    public float each_round_add_health;

    void Start()
    {
        //Wave_Incoming_Text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        Wave_Incoming_Text.text = "Enemy Wave Incoming in " + string.Format("{0:00.00}", countdown);
    }

    public void FixedUpdate()
    {
        /*
        if (last_waveIndex != waveIndex)
        {
            TotalenemyAddHealth += each_round_add_health;

            last_waveIndex = waveIndex;
        }
        */
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i = 0; i< waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, SpawnPoint.position, SpawnPoint.rotation);
        Enemy enemy = enemyPrefab.GetComponent<Enemy>();
        enemy.starthealth += each_round_add_health;
    }
}
