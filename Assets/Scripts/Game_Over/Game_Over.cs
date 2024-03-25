using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game_Over : MonoBehaviour
{
    public Animator Anim;
    public Audio_Manager AM;

    public TextMeshProUGUI roundsText;


    public Save_Data DateSaver;

    // Start is called before the first frame update
    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audio_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        DateSaver = GameObject.FindGameObjectWithTag("DataSaver").GetComponent<Save_Data>();


        roundsText.text = DateSaver.wave.ToString();
    }


    public void Continue_Anim()
    {
        Anim.Play("Casting");
        AM.Play("Horn_Sound");
    }

    public void Menu_Anim()
    {
        Anim.Play("Salute");
    }
}
