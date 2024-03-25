using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Other_Anim : MonoBehaviour
{
    public Animator AM;
    public Shop shop;

    [Header("Build_VFX")]
    public GameObject Building_VFX;
    public ParticleSystem ps;

    [Header("Building Mode Bool")]
    public bool Building_Mode;

    // Start is called before the first frame update
    void Start()
    {
        ps = Building_VFX.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FixedUpdate()
    {
        Building_Mode = shop.Building_Mode;
    }

    public void Casting_Anim()
    {
        //AM.SetBool("Casting", true);
        AM.Play("Casting2");
    }

    public void Builing_VFX_Play()
    {
        ps.Play();
    }
}
