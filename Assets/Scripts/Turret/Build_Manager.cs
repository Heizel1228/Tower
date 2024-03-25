using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Manager : MonoBehaviour
{
    public static Build_Manager instance;

    public TurreyBlueprint turretToBuild;
    public Build selectBuild;

    public TurretUI turretUI;

    public GameObject BuildEffect;
    public GameObject sellEffect;

    public Audio_Manager AM;

    public Money_UI money_UI;


    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than obe buildManager in scene!");
            return;
        }
        instance = this;
    }

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return Player_Stats.Money >= turretToBuild.cost; } }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectBuild(Build build)
    {
        if (selectBuild == build)
        {
            DeselectTurret();
            return;
        }

        selectBuild = build;
        turretToBuild = null;

        turretUI.SetTarget(build);
    }

    public void DeselectTurret()
    {
        selectBuild = null;
        turretUI.Hide();
    }


    public void SelectTurretToBuild(TurreyBlueprint turret)
    {
        turretToBuild = turret;

        DeselectTurret();
    }

    public TurreyBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void Building_Sound()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            AM.Play("Building_Sound1");
        }
        else if (rand == 1)
        {
            AM.Play("Building_Sound2");
        }
        else if (rand == 2)
        {
            AM.Play("Building_Sound3");
        }
    }

}
