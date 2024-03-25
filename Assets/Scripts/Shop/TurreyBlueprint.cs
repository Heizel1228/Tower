using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurreyBlueprint
{
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
