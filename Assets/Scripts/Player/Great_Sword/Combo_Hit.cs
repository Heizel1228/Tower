 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo_Hit : MonoBehaviour
{
    [Header("Anim")]
    private Animator anim;
    public Animator Anim_Cam;

    [Header("Cooldown")]
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;

    [Header("Set Up")]
    public GameObject Player;
    public Transform Skill_Point;
    public Transform Player_Ground;
    public GameObject Sword_breath_Obj;
    public GameObject Earth_Slam_Obj;
    public GameObject Fire_Tomado_Obj;

    [Header("Attributes")]
    public float speed = 5f;
    public float Skill_Damage;

    [Header("Get")]
    public Player_Abilities_Icon Skill_Indicators;
    public Shop shop;
    public Camera_Shake CameraShake;
    public SlowMotion play_slowmotion;
    public Audio_Manager AM;

    [Header("SlowMotion")]
    public int calc_SlowChance;
    public int SlowChance = 15;
    public bool inSlowMotion;

    [Header("Building Mode Bool")]
    public bool Building_Mode;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        Skill_Indicators = GetComponent<Player_Abilities_Icon>();
        Player = this.gameObject;

        CameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Shake>();
        shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>();
        play_slowmotion = GameObject.FindGameObjectWithTag("Player").GetComponent<SlowMotion>();
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audio_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Cheak are the player keep hitting the mouse and using the combo, if not if won't be a combo attack.
        if (Building_Mode == false)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit_1"))
            {
                anim.SetBool("hit_1", false);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit_2"))
            {
                anim.SetBool("hit_2", false);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit_3"))
            {
                anim.SetBool("hit_3", false);
                noOfClicks = 0;
            }

        }

        //Combo cooldown
        if(Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if (Building_Mode == false)
        {
            if (Time.time > nextFireTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (Skill_Indicators.Ability2_on == false)
                    {
                        if (Skill_Indicators.Ability3_on == false)
                        {
                            OnClick();
                        }
                    }

                }
            }
        }
    }

    void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;
        if(noOfClicks == 1)
        {
            anim.SetBool("hit_1", true);

            //SlowMotion Effect
            if (inSlowMotion)
            {
                play_slowmotion.StopSlowMotion();
                inSlowMotion = false;
                AM.Play("SlowMotion_Out");
            }

        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        //Cheak are the animation is finish playing, and move to the next hit animation 
        if(noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit_1"))
        {
            anim.SetBool("hit_1", false);
            anim.SetBool("hit_2", true);
        }

        if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit_2"))
        {
            anim.SetBool("hit_2", false);
            anim.SetBool("hit_3", true);
        }
    }

    public void FixedUpdate()
    {
        Building_Mode = shop.Building_Mode;
    }

    public void Combo_Hit_fire_obj()
    {
        Skill_Indicators.canSkillshot = true;

        GameObject skill = Instantiate(Sword_breath_Obj, Skill_Point.position, Skill_Point.rotation);
    }

    public void Earth_Slam()
    {
        GameObject skill = Instantiate(Earth_Slam_Obj, Player_Ground.position, Player_Ground.rotation);
        ParticleSystem ps = skill.GetComponent<ParticleSystem>();
        ps.Play();
        //Rigidbody rig = skill.GetComponent<Rigidbody>();
    }

    public void Fire_Tomado()
    {
        GameObject skill = Instantiate(Fire_Tomado_Obj, Player.transform.position, Player.transform.rotation);
    }

    public void inslowmotion()
    {
        calc_SlowChance = Random.Range(0, 101);
        Debug.Log("EarthSlam SlowMotion chance is " + calc_SlowChance);

        if (calc_SlowChance <= SlowChance)
        {
            play_slowmotion.StartSlowMotion();
            inSlowMotion = true;

            AM.Play("SlowMotion");
        }
    }

    public void outslowmotion()
    {
        if (inSlowMotion)
        {
            play_slowmotion.StopSlowMotion();

            inSlowMotion = false;

            AM.Play("SlowMotion_Out");
        }
    }

    public void Anim_Shake()
    {
        Anim_Cam.Play("Camera_Shake");
    }
   
}
