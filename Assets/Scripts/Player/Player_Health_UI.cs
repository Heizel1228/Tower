using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class Player_Health_UI : MonoBehaviour
{
    public Slider playerSlider3D;
    Slider playerSlider2D;

    public int start_health;
    public int health;
    public float add_health_value = 0.5f;
    public float add_health;

    [Header("Dead Value")]
    public bool isDead;

    public float revive_CountDown = 5f;
    private float start_revive_CountDown;
    public bool start_revive_Count;

    [Header("Text")]
    public TextMeshProUGUI Dead_Text;
    public TextMeshProUGUI Dead_Text2;


    [Header("Get")]
    public Animator Anim;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        playerSlider2D = GetComponent<Slider>();

        playerSlider2D.maxValue = health;
        playerSlider3D.maxValue = health;

        start_health = health;
        start_revive_CountDown = revive_CountDown;

        isDead = false;
        Dead_Text.enabled = false;
        Dead_Text2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerSlider2D.value = health;
        playerSlider3D.value = playerSlider2D.value;

        if (health < start_health)
        {
            add_health = add_health_value * Time.deltaTime;
            health += (int)add_health;
        }

        if(health <= 0)
        {
            Dead();
        }

        if (start_revive_Count)
        {
            revive_CountDown -= Time.deltaTime;

            Dead_Text.text = "REVIVE TIME : " + revive_CountDown.ToString("0");
            
        }

        if (isDead)
        {
            Anim.SetBool("Dead", true);
        }

        Revive();
    }

    public void Dead()
    {      
        isDead = true;

        agent.enabled = false;

        start_revive_Count = true;

        Dead_Text.enabled = true;
        Dead_Text2.enabled = true;

    }

    public void Revive()
    {
        if (revive_CountDown <= 0)
        {
            isDead = false;
            start_revive_Count = false;
            revive_CountDown = start_revive_CountDown;
            Anim.SetBool("Dead", false);

            health = start_health;

            Dead_Text.enabled = false;
            Dead_Text2.enabled = false;

            agent.enabled = true;
        }
    }

    void FixedUpdate()
    {
        

    }


}
