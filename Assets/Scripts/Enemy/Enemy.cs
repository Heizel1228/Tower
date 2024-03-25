using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject PS;
    public DamageFlash DF;
    public Audio_Manager AM;
    public Animator Anim;
    public Money_UI money_UI;
    public Player_Health_UI player_health;
    public Image healthBar;

    public NavMeshAgent agent;

    public GameObject Player;
    public Player_Abilities_Icon Player_Abilities;
    public Roll_VFX player_roll;

    public GameObject Base;

    public float lookRadius = 5f;

    public bool isFireing;
    public bool isSlowing;

    public GameObject Dead_VFX;

    [Header("Enemy Value")]
    public float starthealth = 100;
    public float un_health;
    public int moneyDrop = 5;
    public float startSpeed;
    public float speed;

    [Header("Soul Drop")]
    public int dropChance;
    public GameObject Drop_Soul;

    [Header("Timer")]
    public bool start_Count;
    public float Timer_Countdown = 0.5f;
    public bool Fire_Time_Count;
    public float Fire_Time_Countdown = 3f;

    [Header("Slow_Timer")]
    public float slowAmount = 0.5f;
    public bool Slow_Time_Count;
    public float Slow_Time_Countdown = 5f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Base = GameObject.FindGameObjectWithTag("Base");
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audio_Manager>();
        Anim = GetComponent<Animator>();
        money_UI = GameObject.FindGameObjectWithTag("Money").GetComponent<Money_UI>();

        Player = GameObject.FindGameObjectWithTag("Player");
        Player_Abilities = Player.GetComponent<Player_Abilities_Icon>();
        DF = GetComponent<DamageFlash>();
        player_health = GameObject.FindGameObjectWithTag("Player_Health").GetComponent<Player_Health_UI>();
        player_roll = Player.GetComponent<Roll_VFX>();

        startSpeed = agent.speed;
        speed = startSpeed;

        un_health = starthealth;


        PS.SetActive(false);

        start_Count = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(Base.transform.position);
        Anim.SetBool("Walk", true);       
        Anim.SetBool("Attack", false);

        if (isFireing == true)
        {
            Count();
            start_Count = true;
            Fire_Time();
            Fire_Time_Count = true;

            if(Timer_Countdown < 0.1)
            {
                DF.FlashStart();
                Fire_Damage();
                hit_sound();

                if (un_health <= 0)
                {
                    Die();
                }

                Timer_Countdown = 0.5f;               
            }

            if(Fire_Time_Countdown < 0.1)
            {
                isFireing = false;
                PS.SetActive(false);

                Fire_Time_Countdown = 3f;
                Fire_Time_Count = false;

                start_Count = false;
                Timer_Countdown = 0.5f;
            }
        }

        if (isSlowing == true)
        {
            Slow_Time();
            Slow_Time_Count = true;

            if (Slow_Time_Countdown < 0.1)
            {
                isSlowing = false;

                Slow_Time_Countdown = 5f;
                Slow_Time_Count = false;
            }

            if(Slow_Time_Countdown > 0.1)
            {
                speed = startSpeed * (1f - slowAmount);
                agent.speed = speed;
            }
         

        }


        if(player_health.isDead == false)
        {
            float distrance = Vector3.Distance(Player.transform.position, transform.position);

            if (distrance <= lookRadius)
            {
                agent.SetDestination(Player.transform.position);
            }

            if (distrance <= agent.stoppingDistance)
            {
                FaceTarget();
                Anim.SetBool("Attack", true);
                Anim.SetBool("Walk", false);
                //Debug.Log("Attack!");
            }
        }     
    }

    private void FixedUpdate()
    {
        if(isSlowing == false)
        {
            agent.speed = startSpeed;
        }
    }

    public void Hit_Player()
    {
        if(player_roll.isDashing == false)
        {
            player_health.health -= 5;
        }

           
    }

    public void FaceTarget()
    {
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void TakeDamage(float amount)
    {
        un_health -= amount;

        healthBar.fillAmount = un_health / starthealth ;

        //Debug.Log("Take Damage");

        if(un_health <= 0)
        {
            Die();
        }
    }

    public void slow(float pct)
    {
        speed = startSpeed * (1f - pct);
        agent.speed = speed;
    }
    


    public void Die()
    {
        Player_Stats.Money += moneyDrop;

        int calc_dropChance = Random.Range(0, 101);

        money_UI.pop_moneyText.text = "+ $" + moneyDrop.ToString();

        money_UI.Pop_Up_Money_Get();

        //Enemy_Spawner2.EnemiesAlive--;

        //Debug.Log("Drop Chance is " +  calc_dropChance);

        if (calc_dropChance <= dropChance)
        {
           Drop_sound();
           Instantiate(Drop_Soul, gameObject.transform.position, gameObject.transform.rotation);
        }

         Destroy(gameObject);

        GameObject effect = (GameObject)Instantiate(Dead_VFX, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

    }
    
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Block")
        {
            Physics.IgnoreCollision(col.collider, col.collider);
        }
    }
    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void Fire_Up()
    {
        PS.SetActive(true);
        ParticleSystem ps = PS.GetComponent<ParticleSystem>();
        ps.Play();
        //un_health -= 5;
        isFireing = true;
    }

    public void Fire_Damage()
    {
        un_health -= 5f;

        healthBar.fillAmount = un_health / starthealth;
    }

    public void Count()
    {
        if (start_Count == true)
        {
            Timer_Countdown -= Time.deltaTime;
        }
    }

    public void Fire_Time()
    {
        if(Fire_Time_Count == true)
        {
            Fire_Time_Countdown -= Time.deltaTime;
        }
    }

    public void Slow_Time()
    {
        if(Slow_Time_Count == true)
        {
            Slow_Time_Countdown -= Time.deltaTime;
        }
    }

    void hit_sound()
    {     
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

    void Drop_sound()
    {
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            AM.Play("Soul_Drop1");
        }
        else if (rand == 1)
        {
            AM.Play("Soul_Drop2");
        }
        else if (rand == 2)
        {
            AM.Play("Soul_Drop3");
        }
        else if (rand == 3)
        {
            AM.Play("Soul_Drop4");
        }
    }

    /*
    IEnumerator Fire_Damage()
    {
        yield return new WaitForSeconds(0.2f);
    }
    */
}
