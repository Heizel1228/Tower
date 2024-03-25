using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTurret : MonoBehaviour
{
    public Transform target;

    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;

    [Header("Setup")]
    public string enemyTag = "Enemy";

    public Transform RotateTurret;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject Fire_VFX;
    public GameObject Fire_VFX2;
    ParticleSystem ps;
    ParticleSystem ps2;

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
        ps2 = Fire_VFX2.GetComponent<ParticleSystem>();
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audio_Manager>();
        build_ps = build_VFX.GetComponent<ParticleSystem>();

        Anim = GetComponent<Animator>();
        Anim.Play("Turret_Pop");

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRoation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(RotateTurret.rotation, lookRoation, Time.deltaTime * turnSpeed).eulerAngles;
        RotateTurret.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            StartCoroutine(Fire_());
            fireCountdown = 1f / fireRate;

           
        }
        fireCountdown -= Time.deltaTime;
    }

    public void Shoot()
    {
        GameObject bulletFire = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletFire.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }

        ps.Play();
       
        //Debug.Log("Shooting");
    }

    IEnumerator Fire_()
    {
        ps2.Play();

        yield return new WaitForSeconds(0.3f);
        ps.Play();

        Shoot();
        Fire_Sound();
    }


    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
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
            AM.Play("Turret2_1");
        }
        else if (rand == 1)
        {
            AM.Play("Turret2_2");
        }
        else if (rand == 2)
        {
            AM.Play("Turret2_3");
        }
        else if (rand == 3)
        {
            AM.Play("Turret2_4");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void Build_VFX()
    {
        build_ps.Play();
        Destroy(build_ps, 2f);
    }
}
