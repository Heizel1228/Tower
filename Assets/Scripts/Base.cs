using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public Game_Manager GM;

    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("Build_Manager").GetComponent<Game_Manager>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Player_Stats.Lives == 0)
        {
            GM.GameOver();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy in Base!");

            Player_Stats.Lives--;

            //Enemy_Spawner.EnemiesAlive--;

            Destroy(col.gameObject);
        }
    }
}
