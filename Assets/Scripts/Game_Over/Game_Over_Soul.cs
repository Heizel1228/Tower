using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over_Soul : MonoBehaviour
{

    //public Audio_Manager AM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Continue()
    {
        SceneManager.LoadScene("Game");
    }

    public void GameOver_BGM_Stop()
    {
        //AM.StopPlaying("GameOver_BGM");
    }
}
