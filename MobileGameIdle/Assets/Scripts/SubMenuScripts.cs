using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuScripts : MonoBehaviour
{
    public List<GameObject> TabsDamage;
    public List<GameObject> TabsArmor;
    int currentlyActive = 0;
    int currentPage = 0;
    public Button upButton;
    public Button downButton;

    // Start is called before the first frame update
    void Start()
    {
        if(currentPage == 0)
        {
            upButton.interactable = false;
        }
    }

    public void SwitchPage(int switchTo)
    {
        TabsDamage[currentPage].SetActive(false);
        TabsArmor[currentPage].SetActive(false);
        currentlyActive = switchTo;
        currentPage = 0;
        if(currentlyActive == 0)
        {
            TabsDamage[currentPage].SetActive(true);
            if (currentPage >= TabsDamage.Count - 1)
            {
                downButton.interactable = false;
            }
        } else if(currentlyActive == 1)
        {
            TabsArmor[currentPage].SetActive(true);
            if (currentPage >= TabsArmor.Count - 1)
            {
                downButton.interactable = false;
            }
        }
    }

    public void SwitchTab(bool downQues)
    {
        upButton.interactable = true;
        downButton.interactable = true;
        if (downQues)
        {
            if (currentlyActive == 0)
            {
                TabsDamage[currentPage].SetActive(false);
                currentPage++;
                TabsDamage[currentPage].SetActive(true);
                if (currentPage >= TabsDamage.Count - 1)
                {
                    downButton.interactable = false;
                }
            }
            else
            {
                TabsArmor[currentPage].SetActive(false);
                currentPage++;
                TabsArmor[currentPage].SetActive(true);
                if (currentPage >= TabsArmor.Count - 1)
                {
                    downButton.interactable = false;
                }
            }
        } else
        {
            if (currentlyActive == 0)
            {
                TabsDamage[currentPage].SetActive(false);
                currentPage--;
                TabsDamage[currentPage].SetActive(true);
                if (currentPage - 1 < 0)
                {
                    upButton.interactable = false;
                }
            }
            else
            {
                TabsArmor[currentPage].SetActive(false);
                currentPage--;
                TabsArmor[currentPage].SetActive(true);
                if (currentPage - 1 < 0)
                {
                    upButton.interactable = false;
                }
            }
        }
    }
}
