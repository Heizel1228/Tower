using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Data : MonoBehaviour
{
    public int wave;
    public Enemy_Spawner es;
    // Start is called before the first frame update

    public static Save_Data instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

     void Start()
     {
            
     }

        // Update is called once per frame
     void Update()
     {
       
     }

    void FixedUpdate()
    {
        wave = es.waveIndex;
    }
}
