using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Abilities_Icon : MonoBehaviour
{
    Animator anim;
    RaycastHit hit;
    Movement moveScript;

    

    [Header("Ability 1")]
    public Image abilityImage1;
    public float cooldown1 = 5;
    bool isCooldown = false;
    public KeyCode ability1;
    public GameObject PS;
    public GameObject PS2;
    public bool Ability1_on;

    [Header("Ability 2")]
    public Image abilityImage2;
    public float cooldown2 = 5;
    bool isCooldown2 = false;
    public KeyCode ability2;
    public bool canSkillshot = true;
    public GameObject projPrefab;
    public Transform projSpawnPoint;
    public bool Ability2_on;

    //Ability 2 Input Variables
    Vector3 position;
    public Canvas ability2Canvas;
    public Image skillshot;
    public Transform player;

    [Header("Ability 3")]
    public Image abilityImage3;
    public float cooldown3 = 5;
    bool isCooldown3 = false;
    public KeyCode ability3;
    public bool CanUseAbility3 = true;
    public Transform projectSpawnPoint;
    public bool Ability3_on;

    //Ability 3 Input Variables
    public Image targetCircle;
    public Image indicatorRangeCircle;
    public Canvas ability3Canvas;
    private Vector3 posUp;
    public float maxAvility2Distance;

    [Header("Othes")]
    public Combo_Hit CH;
    public Audio_Manager AM;
    public Shop shop;
    public Player_Health_UI player_health;
    public bool Building_Mode;

    void Start()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;

        //enabled the ability indicators when have not press the skill
        skillshot.GetComponent<Image>().enabled = false;
        targetCircle.GetComponent<Image>().enabled = false;
        indicatorRangeCircle.GetComponent<Image>().enabled = false;

        CH = GetComponent<Combo_Hit>();

        moveScript = GetComponent<Movement>();
        anim = GetComponent<Animator>();
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audio_Manager>();
        AM.Play("BGM");

        PS.SetActive(false);
        PS2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player_health.isDead == false)
        {
            Ability1();
            Ability2();
            Ability3();

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Ability 2 Input
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }

            //Ability 3 Input
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject != this.gameObject)
                {
                    posUp = new Vector3(hit.point.x, 10f, hit.point.z);
                    position = hit.point;
                }
            }

            //Ability 2 Canvas Inputs
            Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
            transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, transRot.eulerAngles.z);

            ability2Canvas.transform.rotation = Quaternion.Lerp(transRot, ability2Canvas.transform.rotation, 0f);

            //Ability 3 Canvas Inputs
            var hitPosDir = (hit.point - transform.position).normalized;
            float distance = Vector3.Distance(hit.point, transform.position);
            distance = Mathf.Min(distance, maxAvility2Distance);

            var newHitPos = transform.position + hitPosDir * distance;
            ability3Canvas.transform.position = (newHitPos);

            if (Input.GetMouseButtonDown(1))
            {
                skillshot.GetComponent<Image>().enabled = false;
                targetCircle.GetComponent<Image>().enabled = false;
                indicatorRangeCircle.GetComponent<Image>().enabled = false;

                //Ability1_on = false;
                Ability2_on = false;
                Ability3_on = false;

                //Cursor
                Cursor.visible = true;
            }
        }

        void Ability1()
        {
            if (Building_Mode == false)
            {
                if (Input.GetKey(ability1) && isCooldown == false)
                {
                    isCooldown = true;
                    abilityImage1.fillAmount = 1;

                    StartCoroutine(Power_Up());

                    Ability1_on = true;

                    PS.SetActive(true);
                    ParticleSystem ps = PS.GetComponent<ParticleSystem>();
                    ps.Play();

                    AM.Play("Fight_Voice1");
                }
            }

            if (isCooldown)
            {
                abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

                if (abilityImage1.fillAmount <= 0)
                {
                    abilityImage1.fillAmount = 0;
                    isCooldown = false;
                    Ability1_on = false;
                }
            }
        }

        IEnumerator Power_Up()
        {
            anim.SetBool("Casting", true);

            PS2.SetActive(true);
            ParticleSystem ps2 = PS.GetComponent<ParticleSystem>();
            ps2.Play();

            yield return new WaitForSeconds(0.3f);

            PS2.SetActive(false);
            anim.SetBool("Casting", false);
        }

        void Ability2()
        {
            if (Building_Mode == false)
            {
                if (Input.GetKey(ability2) && isCooldown2 == false)
                {
                    skillshot.GetComponent<Image>().enabled = true;
                    Ability2_on = true;

                    //Disable Other UI
                    indicatorRangeCircle.GetComponent<Image>().enabled = false;
                    targetCircle.GetComponent<Image>().enabled = false;

                    //Cursor
                    Cursor.visible = false;
                }

                if (skillshot.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
                {
                    //Rotate player facing the mouse click
                    Quaternion rotationToLookAt = Quaternion.LookRotation(position - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
                        ref moveScript.rotateVelocity, 0);

                    transform.eulerAngles = new Vector3(0, rotationY, 0);

                    moveScript.agent.SetDestination(transform.position);
                    moveScript.agent.stoppingDistance = 0;

                    if (canSkillshot)
                    {
                        isCooldown2 = true;
                        abilityImage2.fillAmount = 1;

                        //Call the ability 2
                        StartCoroutine(corAbility2());

                        Cursor.visible = true;

                        canSkillshot = false;
                    }
                }
            }

            if (isCooldown2)
            {
                abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
                skillshot.GetComponent<Image>().enabled = false;

                if (abilityImage2.fillAmount <= 0)
                {
                    abilityImage2.fillAmount = 0;
                    isCooldown2 = false;

                    canSkillshot = true;
                }
            }
        }

        IEnumerator corAbility2()
        {
            canSkillshot = false;
            anim.SetBool("Sword_skill_2", true);

            yield return new WaitForSeconds(0.7f);

            anim.SetBool("Sword_skill_2", false);
        }

        void Ability3()
        {
            if (Building_Mode == false)
            {
                if (Input.GetKey(ability3) && isCooldown3 == false)
                {
                    //Enabled the indicator
                    indicatorRangeCircle.GetComponent<Image>().enabled = true;
                    targetCircle.GetComponent<Image>().enabled = true;

                    Ability3_on = true;

                    //Disable Skillshot UI
                    skillshot.GetComponent<Image>().enabled = false;

                    //Cursor
                    Cursor.visible = false;

                }

                if (targetCircle.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
                {
                   //Rotate player facing the mouse click
                    Quaternion rotationToLookAt = Quaternion.LookRotation(position - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
                        ref moveScript.rotateVelocity, 0);

                    transform.eulerAngles = new Vector3(0, rotationY, 0);

                    moveScript.agent.SetDestination(transform.position);
                    moveScript.agent.stoppingDistance = 0;

                    if (CanUseAbility3)
                    {
                        isCooldown3 = true;
                        abilityImage3.fillAmount = 1;

                        StartCoroutine(corAbility3());

                        Ability3_on = false;
                        //Cursor
                        Cursor.visible = true;
                    }
                }
            }

            IEnumerator corAbility3()
            {
                CanUseAbility3 = false;
                anim.SetBool("High_Spin_Attack", true);

                yield return new WaitForSeconds(1f);

                anim.SetBool("High_Spin_Attack", false);
            }

            if (isCooldown3)
            {
                abilityImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;

                indicatorRangeCircle.GetComponent<Image>().enabled = false;
                targetCircle.GetComponent<Image>().enabled = false;

                if (abilityImage3.fillAmount <= 0)
                {
                    abilityImage3.fillAmount = 0;
                    isCooldown3 = false;

                    CanUseAbility3 = true;
                }
            }
        }
      

    }

    public void FixedUpdate()
    {
        Building_Mode = shop.Building_Mode;
    }

}
