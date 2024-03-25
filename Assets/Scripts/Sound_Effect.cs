using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Effect : MonoBehaviour
{
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

    public void Ground_Smashing()
    {
        AM.Play("Ground_Smash_Sound");
    }

    public void Ground_Smashing_Start()
    {
        AM.Play("Ground_Smash_Sound_Start");
    }

    public void Earth_Slam_First_Use()
    {
        AM.Play("Earth_Slam_First_Use");
    }

    public void Fight_Voice1()
    {
        AM.Play("Fight_Voice1");
    }

    public void Fight_Voice2()
    {
        AM.Play("Fight_Voice2");
    }

    public void Fight_Voice3()
    {
        AM.Play("Fight_Voice3");
    }

    public void Rolling_Sound()
    {
        AM.Play("Rolling_Sound");
    }

    public void Sword_Hit_Sound_2()
    {
        AM.Play("Sword_Hit_Sound_2");
    }

    public void Fire_Sound()
    {
        AM.Play("Fire_Sound");
    }

    public void Fire_Sound2()
    {
        AM.Play("Fire_Sound2");
    }

    /*
    public void Hit_Sound2()
    {
        AM.Play("Hit_Sound2");
    }

    public void Hit_Sound3()
    {
        AM.Play("Hit_Sound3");
    }

    public void Hit_Sound4()
    {
        AM.Play("Hit_Sound4");
    }

    public void Hit_Sound5()
    {
        AM.Play("Hit_Sound5");
    }
    */

    public void Hit_Sound()
    {
        int rand = Random.Range(0, 4);
        if(rand == 0)
        {
            AM.Play("Sword_Hit_Sound");
        } else if (rand == 1)
        {
            AM.Play("Hit_Sound2");
        } else if (rand == 2)
        {
            AM.Play("Hit_Sound3");
        } else if (rand == 3)
        {
            AM.Play("Hit_Sound4");
        } else if (rand == 4)
        {
            AM.Play("Hit_Sound5");
        }
    }

    public void Walk_Sound()
    {
        AM.Play("Walk_Sound");
    }

    public void SlowMotion_F_in()
    {
        AM.Play("SlowMotion");           
    }

    public void SlowMotion_Out()
    {
        AM.Play("SlowMotion_Out");
    }
}
