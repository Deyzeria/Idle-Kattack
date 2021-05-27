using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetweenFightsScript : MonoBehaviour
{
    public static BetweenFightsScript Instance;

    public Button buttonLeft;
    public Image buttonLeftCooldown;

    private void Awake()
    {
        Instance = this;
    }

    public void HealthPotion()
    {
        gameObject.GetComponent<BattleScript>().HealingReceive();
        buttonLeftCooldown.GetComponent<Image>().fillAmount = 1;
        buttonLeft.GetComponent<Button>().interactable = false;
        StartCoroutine(HealthPotionCD());
    }

    IEnumerator HealthPotionCD()
    {
        for(float i = 60; i > -1; i--)
        {
            yield return new WaitForSecondsRealtime(1);
            Debug.Log(i);
            buttonLeftCooldown.GetComponent<Image>().fillAmount = i / 60;
        }
        buttonLeft.GetComponent<Button>().interactable = true;
    }

    public void BetweenBattleWait()
    { 
        float WaitTime = Random.Range(0, 60-UpgradeListActive.Instance.waitReduction);
        Debug.Log(WaitTime);
        StartCoroutine(WaitingTime(WaitTime));
    }

    IEnumerator WaitingTime(float WaitTime)
    {
        yield return new WaitForSecondsRealtime(WaitTime);
        gameObject.GetComponent<BattleScript>().FightFound();
    }
}
