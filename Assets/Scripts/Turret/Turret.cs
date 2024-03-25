using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public Enemy targetEnemy;

    [Header("General")]
    public float range = 15f;

    [Header("Use Bullets (Default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public int damageOverTime = 15;
    public float slowAmount = 0.2f;

    public LineRenderer lineRenderer;
    public ParticleSystem Laser_Hit_VFX;

    [Header("Use People")]
    public Animator P_Anim;


    [Header("Setup")]
    public string enemyTag = "Enemy";

    public Transform RotateTurret;
    public float turnSpeed = 10f;
   
    public Transform firePoint;
    public GameObject Fire_VFX;
    ParticleSystem ps;

    [Header("Get")]
    public Audio_Manager AM;
    public Animator Anim;

    [Header("Build VFX")]
    public GameObject build_VFX;
    public ParticleSystem build_ps;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        ps = Fire_VFX.GetComponent<ParticleSystem>();
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audio_Manager>();
        Anim = GetComponent<Animator>();
        build_ps = build_VFX.GetComponent<ParticleSystem>();


        //Anim.SetBool("Turret_Pop", true);
        Anim.Play("Turret_Pop");

    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            if(lineRenderer != null)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    Laser_Hit_VFX.Stop();
                }
            }
          
            return;
        }

        LookOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                if(P_Anim != null)
                {
                    P_Anim.Play("Casting");
                

                    targetEnemy.isSlowing = true;
                    targetEnemy.Slow_Time_Countdown = 0.5f;
                }
                else
                {
                    Shoot();
                }

                
                fireCountdown = 1f / fireRate;

                Fire_Sound();
            }

            fireCountdown -= Time.deltaTime;
        }   
        
    }

    void LookOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRoation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(RotateTurret.rotation, lookRoation, Time.deltaTime * turnSpeed).eulerAngles;
        RotateTurret.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {

        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.slow(slowAmount);


        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;

            Laser_Hit_VFX.Play();
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
        //lineRenderer.SetPosition(1, new Vector3(target.transform.position.x, target.transform.position.y +1, target.transform.position.z));

        Vector3 dir = firePoint.position - target.position;

        Laser_Hit_VFX.transform.position = target.position + dir.normalized;

        Laser_Hit_VFX.transform.rotation = Quaternion.LookRotation(dir);
    }

    public void Shoot()
    {
        GameObject bulletFire = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletFire.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }

                      
        ps.Play();

        //Debug.Log("Shooting");
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    public void Fire_Sound()
    {
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            AM.Play("Turret1_1");
        }
        else if (rand == 1)
        {
            AM.Play("Turret1_2");
        }
        else if (rand == 2)
        {
            AM.Play("Turret1_3");
        }
        else if (rand == 3)
        {
            AM.Play("Turret1_4");
        }      
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    /*
    public void Build_VFX()
    {
        build_ps.Play();
        Destroy(build_ps, 2f);
    }
    */
}
