using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 50f;
    public int damage = 20;
    public float explosionRadius = 0f;
    public GameObject impactEffect;
    public GameObject impactEffect2;

    public GameObject Trail_VFX;
    ParticleSystem ps;

    DamageFlash DF;

    // Start is called before the first frame update
    void Start()
    {
        if (Trail_VFX != null)
        {
            ps = Trail_VFX.GetComponent<ParticleSystem>();
            ps.Play();
        }      
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    /*
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("HIT enemy!");

            DF = col.GetComponent<DamageFlash>();
            DF.FlashStart();
        }
    }
    */

    public void HitTarget()
    {
        if(impactEffect != null)
        {
            GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f);
        }
                
        if(impactEffect2 != null)
        {
            GameObject effectIns2 = (GameObject)Instantiate(impactEffect2, transform.position, transform.rotation);

            Destroy(effectIns2, 2f);
        }
        
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);

        DF = target.gameObject.GetComponent<DamageFlash>();
        DF.Turret_FlashStart();
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if(e != null)
        {
            e.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
