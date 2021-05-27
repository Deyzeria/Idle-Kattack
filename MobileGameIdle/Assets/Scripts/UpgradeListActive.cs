using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeListActive : MonoBehaviour
{
    public static UpgradeListActive Instance;

    public bool autoStart = false;
    public float waitReduction;

    [Space]
    public List<int> upgradeLevelsDamage;
    public List<double> upgradeCostsDamage;
    public List<float> upgradeCostsIncreaseDamage;
    public List<float> upgradeAmountDamage;

    [Space]
    public List<int> upgradeLevelsArmor;
    public List<double> upgradeCostsArmor;
    public List<float> upgradeCostsIncreaseArmor;
    public List<float> upgradeAmountArmor;

    [Space]
    public double healingPotion = 100;

    private void Awake()
    {
        Instance = this;
    }
}
