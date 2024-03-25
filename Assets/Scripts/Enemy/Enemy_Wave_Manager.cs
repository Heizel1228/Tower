using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy_Wave_Manager : MonoBehaviour
{
    public Enemy_Spawner Enemy_Spawner;

    public int Now_Wave;

    public TextMeshProUGUI Now_Wave_Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Now_Wave = Enemy_Spawner.waveIndex;

        Now_Wave_Text.text = "Wave " + Now_Wave.ToString();
    }
}
