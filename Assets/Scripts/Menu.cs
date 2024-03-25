using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Audio_Manager AM;

    public SceneFader sceneFader;

    public GameObject HowToPlay_obj;

    // Start is called before the first frame update
    void Start()
    {
        // AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audio_Manager>();
        HowToPlay_obj.SetActive(false);

        AM.Play("BGM");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        sceneFader.FadeTo("SampleScene");
        AM.Play("Horn_Sound");
    }

    
    public void TestGame()
    {
        sceneFader.FadeTo("Game");
        AM.Play("Horn_Sound");
    }

    public void HowToPlay()
    {
        //_Toggle();
        HowToPlay_obj.SetActive(true);
    }

    public void HowToPlay_close()
    {
        HowToPlay_obj.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();

        Debug.Log("Quit!");
    }

}
