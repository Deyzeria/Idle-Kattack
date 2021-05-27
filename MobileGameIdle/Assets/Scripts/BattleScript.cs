using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{
    public Image MCHealthBar;
    public Image ENHealthBar;
    public List<GameObject> Chars;
    public List<Vector2> CharPos;
    public Vector2 targetPos;
    public List<GameObject> UIElements;

    [Space]
    public double MCHealth = 10;
    public double MCArmor = 0f;
    double currentMCHealth;
    public double MCDamage = 0.1d;
    public int multiAttackMC = 1;
    public int MCSpeed = 0;

    [Space]
    public double ENHealth = 1;
    public double ENArmor = 0f;
    double currentENHealth;
    public double ENDamage = 0.1d;
    public int multiAttackEN = 1;
    public int ENSpeed = 0;

    [Space]
    bool Reached = true;
    bool ReachedOld = true;
    int curTurn = 1;
    int prevTurn = 0;
    bool fighting = true;

    [Space]
    public GameObject currentLevel;
    public EnemyList GenericEnemyList;

    private void Awake()
    {
        currentMCHealth = MCHealth;

        currentLevel = GameObject.FindWithTag("CurrentLvl");
        GenericEnemyList = currentLevel.GetComponent<EnemyList>();

        FightFound();
    }

    public void EnemyPassData(double HP, double Damage, int Speed)
    {
        ENHealth = HP;
        currentENHealth = ENHealth;
        ENDamage = Damage;
        ENSpeed = Speed;
    }

    private void Update()
    {
        //Moving towards the middle off the battlefield
        if (!Reached)
        {
            //Moving previous turn to his side of the battlefield
            if (!ReachedOld)
            {
                float distanceOld = Chars[prevTurn].transform.position.x - CharPos[prevTurn].x;
                Chars[prevTurn].transform.position = Vector2.MoveTowards(Chars[prevTurn].transform.position, CharPos[prevTurn], 0.5f);

                if (distanceOld == 0)
                {
                    ReachedOld = true;
                }

            }
            //Moving to strike
            if (ReachedOld == true)
            {
                float distance = Chars[curTurn].transform.position.x - targetPos.x;

                Chars[curTurn].transform.position = Vector2.MoveTowards(Chars[curTurn].transform.position, targetPos, 0.5f);

                if (distance == 0)
                {
                    ReachedOld = false;
                    Reached = true;
                    prevTurn = curTurn;
                    if (curTurn == 1) { curTurn = 0; } else { curTurn++; }

                }
            }
        }
    }

    public void StartFight()
    {
        UIElements[7].GetComponent<Button>().interactable = false; //Fight button disable
        UIElements[0].SetActive(true); //Activating Cubes that show the result of the Speed roll
        UIElements[1].SetActive(true);

        int resultsRandom = Random.Range(1, 21);
        UIElements[2].GetComponent<Text>().text = resultsRandom.ToString() + " <color=green>+ " + MCSpeed + "</color>";
        int resultsRandom2 = Random.Range(1, 21);
        UIElements[3].GetComponent<Text>().text = resultsRandom2.ToString() + " <color=green>+ " + ENSpeed + "</color>";

        resultsRandom += MCSpeed;
        resultsRandom2 += ENSpeed;
        if (resultsRandom < resultsRandom2)
        {
            curTurn = 0;
        }
        else
        {
            curTurn = 1;
        }
        fighting = true;
        StartCoroutine(Fight());
    }

    IEnumerator Fight()
    {
        while (fighting == true)
        {
            Reached = false;

            yield return new WaitForSeconds(0.5f);
            if (Reached)
            {
                if (prevTurn == 0)
                {
                    //Main Character attacks Enemy
                    for (int i = 0; i < multiAttackMC && currentENHealth > 0; i++)
                    {
                        yield return new WaitForSeconds(1f);
                        double damageDealt;
                        if(MCDamage- ENArmor > 0)
                        {
                            damageDealt = MCDamage - ENArmor;
                        } else
                        {
                            damageDealt = 0;
                        }
                        currentENHealth -= damageDealt;
                        double hpBarFill;
                        hpBarFill = currentENHealth / ENHealth;
                        ENHealthBar.fillAmount = (float)hpBarFill;
                    }
                }
                else
                {
                    //Enemy attacks Main Character
                    for (int i = 0; i < multiAttackEN && currentMCHealth > 0; i++)
                    {
                        yield return new WaitForSeconds(1f);
                        double damageDealt;
                        if (ENDamage - MCArmor > 0)
                        {
                            damageDealt = ENDamage - MCArmor;
                        }
                        else
                        {
                            damageDealt = 0;
                        }
                        currentMCHealth -= damageDealt;
                        double hpBarFill = currentMCHealth / MCHealth;
                        MCHealthBar.fillAmount = (float)hpBarFill;
                    }
                }
            }

            if (currentMCHealth <= 0 || currentENHealth <= 0)
            {
                UIElements[0].SetActive(false);
                UIElements[1].SetActive(false);
                if (currentENHealth <= 0) // Enemy lost
                {
                    Reached = false;
                    Chars[0].transform.position = CharPos[0];
                    Chars[1].SetActive(false);
                    fighting = false;
                    MoneyScript.Instance.NumberToAddNormal();
                    BetweenFightsScript.Instance.BetweenBattleWait();
                }
                else // Hero lost
                {
                    Reached = false;
                    Chars[0].SetActive(false);
                    fighting = false;
                    // Lost Condition here
                }
            }
        }
    }


    //============================

    public void FightFound()
    {
        ENHealth = 1;
        currentENHealth = ENHealth;
        ENHealthBar.fillAmount = 1;
        Chars[1].transform.position = CharPos[1];
        GameObject Enemy = Instantiate(GenericEnemyList.GenericEnemy[0], Chars[1].transform.position, Quaternion.identity);
        Enemy.transform.parent = Chars[1].transform;
        Chars[1].SetActive(true);


        //Here add the check for having an upgradde to autoskip the start
            UIElements[7].GetComponent<Button>().interactable = true;
        
    }

    public void HealingReceive()
    {
        double healingTemp = UpgradeListActive.Instance.healingPotion;
        if (currentMCHealth + healingTemp >= MCHealth)
        {
            currentMCHealth = MCHealth;
        }
        else
        {
            currentMCHealth += healingTemp;
        }
        double hpBarFill = currentMCHealth / MCHealth;
        MCHealthBar.fillAmount = (float)hpBarFill;
    }
}