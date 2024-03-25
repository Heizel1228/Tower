using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Attack : MonoBehaviour
{
    public Animator Anim;
    public Animator Ice_Anim;
    public Audio_Manager AM;

    public GameObject Ice_VFX;
    public ParticleSystem ps;

    public GameObject Ice_Prefab;
    public Transform Shot_Point;



    // Start is called before the first frame update
    void Start()
    {
        Ice_VFX.SetActive(false);
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audio_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Anim.GetBool("Attack"))
        {
            ps = Ice_VFX.GetComponent<ParticleSystem>();

            Ice_VFX.SetActive(true);
            ps.Play();
            Ice_Anim.SetBool("Using_Ice", true);

        }
        else
        {
            Ice_Anim.SetBool("Using_Ice", false);
            Ice_VFX.SetActive(false);
        }
    }

    public void Attack()
    {
        GameObject bulletFire = (GameObject)Instantiate(Ice_Prefab, Shot_Point.position, Shot_Point.rotation);
    }

    public void Fire_Sound()
    {
        int rand = Random.Range(0, 7);
        if (rand == 0)
        {
            AM.Play("Casting_Ice1");
        }
        else if (rand == 1)
        {
            AM.Play("Casting_Ice2");
        }
        else if (rand == 2)
        {
            AM.Play("Casting_Ice3");
        }
        else if (rand == 3)
        {
            AM.Play("Casting_Ice4");
        }

    }
}
