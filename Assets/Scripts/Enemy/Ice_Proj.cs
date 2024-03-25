using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Proj : MonoBehaviour
{
    private Transform Player;
    public Roll_VFX player_roll;
    public Player_Health_UI player_health;

    public float speed = 50f;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        player_health = GameObject.FindGameObjectWithTag("Player_Health").GetComponent<Player_Health_UI>();
        player_roll = Player.GetComponent<Roll_VFX>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Player.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
           // HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(Player);
    }

    public void HitTarget()
    {
        //GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);

       // Destroy(effectIns, 2f);

       // Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(effectIns, 2f);

            Destroy(gameObject);

            if(player_roll.isDashing == false)
            {
                player_health.health -= 5;
            }

        }
    }
}
