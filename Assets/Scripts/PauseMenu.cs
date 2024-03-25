using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject UI;
    public GameObject HowToPlay_obj;

    public SceneFader sceneFader;
    // Start is called before the first frame update
    void Start()
    {
        HowToPlay_obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        UI.SetActive(!UI.activeSelf);

        if (UI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo("SampleScene");
    }

    public void Retry_Test()
    {
        Toggle();
        sceneFader.FadeTo("Game");
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo("MainMenu");
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
