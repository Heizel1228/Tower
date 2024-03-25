using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Drop_Soul : MonoBehaviour
{
    public int moneyDrop = 10;
    public Money_UI money_UI;
    public Animator Anim;
    public Audio_Manager AM;
    public Player_Health_UI Player_Health;

    // Start is called before the first frame update
    void Start()
    {
        money_UI = GameObject.FindGameObjectWithTag("Money").GetComponent<Money_UI>();
        Anim = GetComponent<Animator>();
        Anim.Play("Enemy_Drop_Soul");

        Player_Health = GameObject.FindGameObjectWithTag("Player_Health").GetComponent<Player_Health_UI>();

        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audio_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("Player Get!");

            Player_Health.health += 10;

            AM.Play("Soul_Get");

            money_UI.pop_moneyText.text = "+ $" + moneyDrop.ToString();

            money_UI.Pop_Up_Money_Get();

            Player_Stats.Money += moneyDrop;

            Destroy(this.gameObject);
        }
    }

}
