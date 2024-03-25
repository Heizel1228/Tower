using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Build : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    public Renderer rend;
    public Color startColor;

    public Color NotEnoughMoneyColor;

    [Header("Get")]
    Build_Manager buildManager;
    public Shop shop;
    public Player_Other_Anim player_anim;
    public Player_Health_UI player_health;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurreyBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;


    [Header("Building Mode Bool")]
    public bool Building_Mode;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        player_health = GameObject.FindGameObjectWithTag("Player_Health").GetComponent<Player_Health_UI>();

        //shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>();
      
        buildManager = Build_Manager.instance;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        Building_Mode = shop.Building_Mode;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;


        if (Building_Mode == true)
        {
            if (turret != null)
            {
                buildManager.SelectBuild(this);
                return;
            }
        }

        if (Building_Mode == true)
        {
            if(player_health.isDead == false)
            {
                player_anim.Casting_Anim();
            }
            

            BuildTurret(buildManager.GetTurretToBuild());
        }      
    }

    void BuildTurret(TurreyBlueprint blueprint)
    {
        if (Player_Stats.Money < blueprint.cost)
        {
            Debug.Log("Not enough moeny to build that!");
            return;
        }

        Player_Stats.Money -= blueprint.cost;

        buildManager.money_UI.pop_moneyText.text = "- $" + blueprint.cost.ToString();

        buildManager.money_UI.PopUp_Money();

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.BuildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        buildManager.Building_Sound();

        //Debug.Log("Turret build! Money left : " + Player_Stats.Money);
    }

    public void UpgardeTurret()
    {
        if (Player_Stats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough moeny to Upgrade!");
            return;
        }

        Player_Stats.Money -= turretBlueprint.upgradeCost;

        buildManager.money_UI.pop_moneyText.text = "- $" + turretBlueprint.upgradeCost.ToString();

        buildManager.money_UI.PopUp_Money();

        //Get rid of old turret
        Destroy(turret);

        // Building a new one
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.BuildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        isUpgraded = true;

        Debug.Log("Turret Upgraded!");

        buildManager.Building_Sound();
    }

    public void SellTurret()
    {
        Player_Stats.Money += turretBlueprint.GetSellAmount();
        Destroy(turret);
        turretBlueprint = null;

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        //buildManager.money_UI.pop_moneyText.text = "+ $" + turretBlueprint.GetSellAmount();

        //buildManager.money_UI.PopUp_Money();
    }



    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (Building_Mode == true)
        {
            if (buildManager.HasMoney)
            {
                rend.material.color = hoverColor;
            }
            else
            {
                rend.material.color = NotEnoughMoneyColor;
            }
        }
    }

    void OnMouseExit()
    {
        if (Building_Mode == true)
        {
            rend.material.color = startColor;
        }          
    }
}
