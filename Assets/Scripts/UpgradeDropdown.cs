using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDropdown : MonoBehaviour
{
    public GameObject UpgradeMenuBackground;
    public GameObject[] menus;
    public void HandleInputData(int val)//You could probably make this a lot better, but that's lame and all.
    {
        if (val != 0)
        {
            for (int i = 0; i < menus.Length; i++)
            {
                menus[i].SetActive(false);
                if (val-1 == i)
                {
                    menus[i].SetActive(true);
                }
            }
            UpgradeMenuBackground.SetActive(true);
        }
        else
        {
            for (int i = 0; i < menus.Length; i++)
            {
                menus[i].SetActive(false);
            }
            UpgradeMenuBackground.SetActive(false);
        }
    }
}
