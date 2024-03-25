using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hit_Count : MonoBehaviour
{
    public TextMeshProUGUI Hit_Combo_Text;
    public Animator AM;

    public bool Hited;
    public bool start_Count;

    public float Hit_Num;
    public float Hit_Time_Left = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Hit_Combo_Text = GameObject.FindGameObjectWithTag("Combo_Count").GetComponent<TextMeshProUGUI>();
        AM = GameObject.FindGameObjectWithTag("Combo_Count").GetComponent<Animator>();

        Hit_Combo_Text.enabled = false;
        Hited = false;
    }

    void Update()
    {
        
        if (Hited != false)
        {
            start_Count = true;
            Count();


            if (Hit_Time_Left < 1)
            {
                Hit_Num = 0;
                Hit_Combo_Text.enabled = false;
                Hit_Time_Left = 5f;
                Hited = false;
                start_Count = false;               
            }

            if(Hit_Time_Left < 3)
            {
                AM.Play("Hit_Fade_Out");
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Hit_Combo_Text = GameObject.FindGameObjectWithTag("Combo_Count").GetComponent<TextMeshProUGUI>();
        AM = GameObject.FindGameObjectWithTag("Combo_Count").GetComponent<Animator>();

        Hit_Combo_Text.text = Hit_Num + " Hit";
    }

    public void Count()
    {
        if(start_Count == true)
        {
            Hit_Time_Left -= Time.deltaTime;
        }
    }

    public void Hit_plus()
    {
        Hit_Num += 1;
        AM.Play("Hit_Text");

        Hit_Combo_Text.enabled = true;
        Hited = true;

        Hit_Time_Left = 5f;
    }
}
