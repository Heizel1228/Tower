using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour
{
    [Header("Start Shake")]
    public bool start = false;

    public AnimationCurve curve;
    public float duration = 1f;

    public void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());

            Debug.Log("In Start");
        }
    }

    public IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;

            //Debug.Log("In Shake");
        }

        //transform.position = startPosition;
        //transform.position = new Vector3(0, 0, 0);
    }
}

