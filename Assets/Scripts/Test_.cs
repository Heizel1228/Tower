using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ : MonoBehaviour
{
    public GameObject This_Panel;

    public Player_Health_UI player_health;

    // Start is called before the first frame update
    void Start()
    {
        This_Panel = this.gameObject;

        player_health = GameObject.FindGameObjectWithTag("Player_Health").GetComponent<Player_Health_UI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            OpenPanel();
        }
    }

    public void OpenPanel()
    {
        if (This_Panel != null)
        {
            Animator anim = This_Panel.GetComponent<Animator>();
            if (anim != null)
            {
                bool isOpen = anim.GetBool("Open_Panel");
                //Building_Mode = false;

                anim.SetBool("Open_Panel", !isOpen);

                /*
                if (!isOpen)
                {
                    Building_Mode = true;
                }
                */
            }
        }
    }

    public void Add_Moeny()
    {
        Player_Stats.Money += 1000;
    }

    public void Base_Boom()
    {
        Player_Stats.Lives = 0;
    }

    public void Player_Dead()
    {
        player_health.health = 0;
    }

    public void Player_Revive()
    {
        player_health.revive_CountDown = 0;
    }

    public void Enemy_Dead()
    {
        GameObject[] enemys;

        enemys = GameObject.FindGameObjectsWithTag("Enemy");

       foreach(GameObject Enemys in enemys)
        {
            Destroy(Enemys);
            //Enemys.GetComponent<Enemy>().Die();
        }
    }

}
