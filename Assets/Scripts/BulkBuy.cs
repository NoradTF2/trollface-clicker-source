using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulkBuy : MonoBehaviour
{
    public UpgradeMenu upgrademenu;
    public void HandleInputData(int val)//You could probably make this a lot better, but that's lame and all.
    {
        switch(val)
        {
            case 0:
                {
                    upgrademenu.currentPerBuy = 1.0;
                    break;
                }
            case 1:
                {
                    upgrademenu.currentPerBuy = 10.0;
                    break;
                }
            case 2:
                {
                    upgrademenu.currentPerBuy = 100.0;
                    break;
                }
            case 3:
                {
                    upgrademenu.currentPerBuy = 1000.0;
                    break;
                }
            
            case 4:
                {
                    upgrademenu.currentPerBuy = -1.0;//for buy max
                    break;
                }
        }
        upgrademenu.UpdateAllUpgradesText();
    }
}
