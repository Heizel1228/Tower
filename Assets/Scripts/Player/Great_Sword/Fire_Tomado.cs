using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Tomado : MonoBehaviour
{
    public SlowMotion play_slowmotion;

    Vector3 temp;
    float speed = 0.3f;
    public float fly_speed = 3f;

    //public float Hit_Time_Cooldown = 0.2f;
    public float Hit_Time;

    public bool start_Count;

    public Audio_Manager AM;
    DamageFlash DF;

    public bool inSlowMotion;

    public Camera_Shake CameraShake;
    public Animator Anim_Cam;

    public int calc_SlowChance;
    public int SlowChance = 15;

    //public Camera_Shake Camera_shake;

    // Start is called before the first frame update
    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audio_Manager>();

        CameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Shake>();

        Anim_Cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();

        play_slowmotion = GameObject.FindGameObjectWithTag("Player").GetComponent<SlowMotion>();

        calc_SlowChance = Random.Range(0, 101);
        Debug.Log("SlowMotion chance is " + calc_SlowChance);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 5);
        StartCoroutine(start_flying());

        temp = transform.localScale;
        temp.x += Time.deltaTime * speed;
        temp.y += Time.deltaTime * speed;
        temp.z += Time.deltaTime * speed;

        transform.localScale = temp;

        Hit_Time += Time.deltaTime;
        if (Hit_Time > 0.15)
        {
            Hit_Time = 0;
        }

    }

    void FixedUpdate()
    {

    }

    IEnumerator start_flying()
    {
        yield return new WaitForSeconds(1f);

           
        gameObject.transform.TransformDirection(Vector3.forward);
        gameObject.transform.Translate(new Vector3(0, 0, fly_speed * Time.deltaTime));

        StartCoroutine(CameraShake.Shaking());
        //Debug.Log("Flying");
    }

    public void inslowmotion()
    {
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

    /*
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("HIT enemy!");

            hit_sound();

            DF = col.GetComponent<DamageFlash>();
            DF.FlashStart();
        }
    }
    */


    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Enemy" && Hit_Time == 0)
        {
            Debug.Log("HIT enemy!");
            start_Count = true;
            hit_sound();

            Enemy Enemy = col.gameObject.GetComponent<Enemy>();

            Enemy.un_health -= 15;

            Enemy.healthBar.fillAmount = Enemy.un_health / Enemy.starthealth;

            if (Enemy.un_health <= 0)
            {
                Enemy.Die();
            }

            DF = col.GetComponent<DamageFlash>();
            DF.FlashStart();
        }
    }

    /*
    public void Count()
    {
        if (start_Count == true)
        {
            Hit_Time_Cooldown -= Time.deltaTime;
        }
    }
    */

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

    public void FT_Explosion_Sound()
    {
        AM.Play("FT_Explosion_Sound");
    }

    public void FT_Fire_T()
    {
        AM.Play("FT_Fire_T");
    }

    public void FT_Wind_Sound()
    {
        AM.Play("FT_Wind_Sound");
    }

    public void Fight_Voice_3()
    {
        AM.Play("Fight_Voice3");
    }

    public void Anim_Shake()
    {
        Anim_Cam.Play("Camera_Shake");
    }
}
