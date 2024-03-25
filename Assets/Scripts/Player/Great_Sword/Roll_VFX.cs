using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll_VFX : MonoBehaviour
{
    public float dashSpeed;
    //public Rigidbody rig;
    public bool isDashing;

    public GameObject Trail_Point;
    public GameObject trail;

    public Animator AM;

    // Start is called before the first frame update
    void Start()
    {
        //trail = transform.Find("Trail").GetComponent<ParticleSystem>();
        AM = GetComponent<Animator>();

        //rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dashing();
            isDashing = true;
        }
    }

    private void FixedUpdate()
    {
        /*
        if (isDashing)
        {
            
        }
        */
    }

    public void Dashing()
    {
        //rig.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);

        //transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
        //transform.position += new Vector3(dashSpeed * Time.deltaTime, 0.1f, 0.0f);
        AM.Play("Rolling");

        //isDashing = false;

        var NewTrail = Instantiate(trail, Trail_Point.transform.position, Trail_Point.transform.rotation);
        NewTrail.transform.parent = gameObject.transform;

        ParticleSystem ps = NewTrail.GetComponent<ParticleSystem>();
        ps.Play();

        /*
        if (AM.GetCurrentAnimatorStateInfo(0).length > AM.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            Dash_finish();
            
        }
        */

    }

    public void Dash_finish()
    {
        isDashing = false;
        //Debug.Log("Dashing finish");
    }
}
