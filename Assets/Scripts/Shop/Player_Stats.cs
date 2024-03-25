using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{

    public static int Money;
    public int startMoney = 200;

    public static int Lives;
    public int startLives = 20;


    void Start()
    {
        Money = startMoney;
        Lives = startLives;

    }
}
