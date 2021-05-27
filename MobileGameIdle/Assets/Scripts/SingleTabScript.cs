using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SingleTabScript : MonoBehaviour
{
    public int status = 0;
    //status- 0- damage upgrade, 1- armor upgrade
    public int thisItemNumber;
    public TextMeshProUGUI numberText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI priceText;

    private void Start()
    {
        confirmLevel();
    }

    void confirmLevel()
    {
        if (status == 0)
        {
            numberText.SetText("Effect: + {0} <sprite=0>", UpgradeListActive.Instance.upgradeAmountDamage[thisItemNumber]);
            levelText.SetText("Level: {0}", UpgradeListActive.Instance.upgradeLevelsDamage[thisItemNumber]);
            priceText.SetText("Price: {0} <sprite=2>", (float)UpgradeListActive.Instance.upgradeCostsDamage[thisItemNumber]);
        } else
        {
            numberText.SetText("Effect: + {0} <sprite=1>", UpgradeListActive.Instance.upgradeAmountArmor[thisItemNumber]);
            levelText.SetText("Level: {0}", UpgradeListActive.Instance.upgradeLevelsArmor[thisItemNumber]);
            priceText.SetText("Price: {0} <sprite=2>", (float)UpgradeListActive.Instance.upgradeCostsArmor[thisItemNumber]);
        }
    }


}
