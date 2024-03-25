using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over_Player : MonoBehaviour
{
    public ParticleSystem ps;
    public Animator Soul_Anim;

    public Audio_Manager AM;

    // Start is called before the first frame update
    void Start()
    {
        ps.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Start_VFX()
    {
        ps.gameObject.SetActive(true);
        ps.Play();
    }

    public void Continue()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Soul()
    {
        Soul_Anim.Play("Soul");
    }

    public void DJ_Stop()
    {
        AM.Play("DJ_Stop");
    }

    public void YouLose()
    {
        AM.Play("YouLose");
    }

    public void GameOver_BGM()
    {
        AM.Play("GameOver_BGM");
    }


}
