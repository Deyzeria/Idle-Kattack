using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MoneyScript : MonoBehaviour
{
    public static MoneyScript Instance;
    public int GameState = 0;
    public TextMeshProUGUI DisplayedNumber;
    public double moneyNumber;
    public double activeIncome = 5;
    //0.42e2 = 42 || 42e2 = 4200

    private void Start()
    {
        Instance = this;
        updateNumber();
    }

    public void NumberToAddNormal()
    {
        moneyNumber += activeIncome;
        updateNumber();
    }

    public void NumberToAddBoss()
    {
        moneyNumber += activeIncome*10;
        updateNumber();
    }

    public void NumberToSubtract(double sumToSubt)
    {
        moneyNumber = moneyNumber - sumToSubt;
        updateNumber();
    }


    public void updateNumber()
    {
        DisplayedNumber.SetText("{0}", (float)moneyNumber);
    }


}
