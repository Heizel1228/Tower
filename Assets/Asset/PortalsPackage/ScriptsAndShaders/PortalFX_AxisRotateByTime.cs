using System;
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class PortalFX_AxisRotateByTime : MonoBehaviour {

    public Enemy_Spawner enemy_spawner;

    public Audio_Manager AM;
    public float Stone_Second;
    //bool isPlayed;

    public Vector3 RotateAxis = new Vector3(1, 5, 10);

	// Use this for initialization
	void Start ()
    {      
        StartCoroutine(Stone_Sound());
    }

	// Update is called once per frame
	void Update () {

        if(enemy_spawner.waveIndex >= 1)
        {
            transform.Rotate(RotateAxis * Time.deltaTime);           
        }        
	}

    
    IEnumerator Stone_Sound()
    {
        yield return new WaitForSeconds(5f);
        AM.Play("Stone_Move");
    }
    
}
