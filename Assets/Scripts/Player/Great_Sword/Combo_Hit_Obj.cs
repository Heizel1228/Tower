using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo_Hit_Obj : MonoBehaviour
{
    Vector3 temp;
    float speed = 2f;
    float fly_speed = 10f;

    public Audio_Manager AM;
    DamageFlash DF;

    public Camera_Shake CameraShake;

    // Start is called before the first frame update
    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audio_Manager>();
        CameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Shake>();

        StartCoroutine(CameraShake.Shaking());
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 2);

        temp = transform.localScale;
        temp.x += Time.deltaTime * speed;
        temp.y += Time.deltaTime * speed;
        temp.z += Time.deltaTime * speed;

        transform.localScale = temp;

        gameObject.transform.TransformDirection(Vector3.forward);
        gameObject.transform.Translate(new Vector3(0, 0, fly_speed * Time.deltaTime));
        gameObject.transform.Rotate(0, 0, 100 * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("HIT enemy!");

            Enemy Enemy = col.gameObject.GetComponent<Enemy>();

            Enemy.un_health -= 50;

            Enemy.healthBar.fillAmount = Enemy.un_health / Enemy.starthealth;

            if (Enemy.un_health <= 0)
            {
                Enemy.Die();
            }

            hit_sound();

           // Enemy enemyHurt = col.gameObject.GetComponent<Enemy>();
           // enemyHurt.DeductHealth(weapons_skill.Skill_Damage);
            //Destroy(this.gameObject);
            //Destroy(col.gameObject);
            DF = col.GetComponent<DamageFlash>();
            DF.FlashStart();
            //AM.Play("Sword_hit");
            
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
}
