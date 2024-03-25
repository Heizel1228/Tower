using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretUI : MonoBehaviour
{
    private Build target;

    public TextMeshProUGUI upgradeCost;
    public Button upgradeButton;

    public TextMeshProUGUI sellAmount;

    public Audio_Manager AM;

    public GameObject UI;

    public void SetTarget(Build _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

       

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$ " + target.turretBlueprint.GetSellAmount();


        UI.SetActive(true);
    }

    public void Hide()
    {
        UI.SetActive(false);
    }

    public void upgrade()
    {
        target.UpgardeTurret();
        Build_Manager.instance.DeselectTurret();

        Building_Sound();
    }

    public void Sell()
    {
        target.SellTurret();
        Build_Manager.instance.DeselectTurret();

        Sell_Sound();
    }

    public void Sell_Sound()
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
