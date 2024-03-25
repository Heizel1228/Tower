using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    Build_Manager buildManager;

    public GameObject Shop_panel;

    [Header("Revive")]
    public Player_Health_UI player_health;
    public GameObject Revive_But;
    public Money_UI money_UI;
    public Audio_Manager AM;
    public GameObject heal_VFX;

    [Header("Building Mode Bool")]
    public bool Building_Mode;

    public TextMeshProUGUI Building_Mode_Text;

    [Header("TurretBlueprint")]
    public TurreyBlueprint standardTurret;
    public TurreyBlueprint CannonTurret;
    public TurreyBlueprint LaserTurret;
    public TurreyBlueprint SlowTurret;

    void Start()
    {
        buildManager = Build_Manager.instance;
        Building_Mode = false;

        Building_Mode_Text.enabled = false;

        heal_VFX.SetActive(false);

        
    }

    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret press");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectCannonTurret()
    {
        Debug.Log("Cannon Turret press");
        buildManager.SelectTurretToBuild(CannonTurret);
    }

    public void SelectLaserTurret()
    {
        Debug.Log("Laser Turret press");
        buildManager.SelectTurretToBuild(LaserTurret);
    }

    public void SelectSlowTurret()
    {
        Debug.Log("Laser Turret press");
        buildManager.SelectTurretToBuild(SlowTurret);
    }

    public void Player_Revive()
    {
        if (player_health.isDead)
        {
            Revive_But.SetActive(true);
        }
        else
        {
            Revive_But.SetActive(false);
        }
    }

    public void Revive_Button()
    {
        player_health.revive_CountDown = 0;

        Player_Stats.Money -= 50;

        AM.Play("Heal");

        heal_VFX.SetActive(true);
        ParticleSystem ps = heal_VFX.GetComponent<ParticleSystem>();
        ps.Play();

        buildManager.money_UI.pop_moneyText.text = "- $ 50" ;

        buildManager.money_UI.PopUp_Money();

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {           
            OpenShop();    
        }

        if(Building_Mode == true)
        {
            Building_Mode_Text.enabled = true;
        }
        else
        {
            Building_Mode_Text.enabled = false; 
        }

        Player_Revive();
    }

    public void OpenShop()
    {
        if(Shop_panel != null)
        {
            Animator anim = Shop_panel.GetComponent<Animator>();
            if(anim != null)
            {
                bool isOpen = anim.GetBool("Shop_PopUp");
                Building_Mode = false;

                anim.SetBool("Shop_PopUp", !isOpen);

                if (!isOpen)
                {
                    Building_Mode = true;
                }
            }
        }
    }
}
