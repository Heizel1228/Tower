using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Money_UI : MonoBehaviour
{
    public TextMeshProUGUI moenyText;

    public TextMeshProUGUI pop_moneyText;
    public Animator pop_money_anim;
    public Audio_Manager AM;

    public TurreyBlueprint turretToBuild;
    // Start is called before the first frame update
    void Start()
    {
        pop_moneyText.enabled = false;
        pop_money_anim = pop_moneyText.GetComponent<Animator>();
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audio_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        moenyText.text = "$" + Player_Stats.Money.ToString();
    }

    public void PopUp_Money()
    {
        StartCoroutine(popup_money());
    }

    public void Pop_Up_Money_Get()
    {
        StartCoroutine(popup_money_get());
    }

    IEnumerator popup_money()
    {
        pop_moneyText.enabled = true;
        pop_money_anim.Play("Popup_Money");
        Moeny_sound();

        yield return new WaitForSeconds(0.5f);

        pop_moneyText.enabled = false;
    }

    IEnumerator popup_money_get()
    {
        pop_moneyText.enabled = true;
        pop_money_anim.Play("Popup_Money");
        //Moeny_sound();

        yield return new WaitForSeconds(0.5f);

        pop_moneyText.enabled = false;
    }


    void Moeny_sound()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            AM.Play("Use_Money_Sound1");
        }
        else if (rand == 1)
        {
            AM.Play("Use_Money_Sound2");
        }
        else if (rand == 2)
        {
            AM.Play("Use_Money_Sound3");
        }
    }
}
