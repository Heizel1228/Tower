using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash2 : MonoBehaviour
{
    public Animator AM;
    //MeshRenderer meshRenderer;
   // Color origColor;
    float flashTime = .15f;

    public Hit_Count Hit_count;

    void Start()
    {

        //meshRenderer = GetComponent<MeshRenderer>();
       // origColor = meshRenderer.material.color;

        Hit_count = GameObject.FindGameObjectWithTag("Player").GetComponent<Hit_Count>();
        AM = GetComponent<Animator>();
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlashStart();
        }
        */
    }

    public void FlashStart()
    {
        //meshRenderer.material.color = Color.red;
        Invoke("FlashStop", flashTime);
        //AM.Play("Red");

        Hit_count.Hit_plus();
    }

    void FlashStop()
    {
        //meshRenderer.material.color = origColor;
    }

    /*
    IEnumerator EFlash()
    {
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(flashTime);
        meshRenderer.material.color = origColor;
    }
    */
}
