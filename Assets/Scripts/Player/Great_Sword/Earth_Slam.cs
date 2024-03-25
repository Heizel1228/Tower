using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_Slam : MonoBehaviour
{
    public Player_Abilities_Icon Player_Abilities;


    public BoxCollider BoxCol;
    public Audio_Manager AM;
    DamageFlash DF;

    Vector3 temp;
    float speed = 4f;


    // Start is called before the first frame update
    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audio_Manager>();

        BoxCol = GetComponent<BoxCollider>();


        Player_Abilities = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Abilities_Icon>();
    }

    // Update is called once per frame
    void Update()
    {
        

        Destroy(this.gameObject, 1f);

        temp = BoxCol.size;
        temp.x += Time.deltaTime * speed;
        temp.y += Time.deltaTime * speed;
        temp.z += Time.deltaTime * speed;

        BoxCol.size = temp;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //Debug.Log("HIT enemy!");

            Enemy Enemy = col.gameObject.GetComponent<Enemy>();
            hit_sound();

            Enemy.un_health -= 50;

            Enemy.healthBar.fillAmount = Enemy.un_health / Enemy.starthealth ;

            if (Enemy.un_health <= 0)
            {
                Enemy.Die();
            }


            DF = col.GetComponent<DamageFlash>();
            DF.FlashStart();

            if(Player_Abilities.Ability1_on == true)
            {
                Enemy.Fire_Up();               
                //Debug.Log("Get Enemy");
            }
        }
    }

    void hit_sound()
    {
        //AM.Play("Sword_Hit_Sound");
        //Debug.Log("Sound");

        int rand = Random.Range(0, 4);
        if (rand == 0)
        {
            AM.Play("Sword_Hit_Sound");
        }
        else if (rand == 1)
        {
            AM.Play("Hit_Sound2");
        }
        else if (rand == 2)
        {
            AM.Play("Hit_Sound3");
        }
        else if (rand == 3)
        {
            AM.Play("Hit_Sound4");
        }
        else if (rand == 4)
        {
            AM.Play("Hit_Sound5");
        }
    }

  
}
