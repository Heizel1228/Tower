using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleTurret : MonoBehaviour
{
    public Turret turret;
    public Audio_Manager AM;

    // Start is called before the first frame update
    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audio_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {

    }
    public void shoot()
    {
        turret.Shoot();
    }

    public void Ice_Chage_Sound()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            AM.Play("Ice_Chage1");
        }
        else if (rand == 1)
        {
            AM.Play("Ice_Chage2");
        }
        else if (rand == 2)
        {
            AM.Play("Ice_Chage3");
        }
    }

    public void Ice_Shot_Sound()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            AM.Play("Ice_Shot1");
        }
        else if (rand == 1)
        {
            AM.Play("Ice_Shot2");
        }
        else if (rand == 2)
        {
            AM.Play("Ice_Shot3");
        }
    }
}
