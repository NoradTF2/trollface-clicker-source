using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeMenu : MonoBehaviour
{
    public Functions functions;
    public ClickerTexts texts;
    public Clicker clicker;
    public Text[] upgradeText;
    public double currentPerBuy = 1.0;
    public void UpdateAllUpgradesText()
    {
        UpgradeTapDamage(true);
        UpgradeFlameConversion(true);
        UpgradeTapStealing(true);
        UpgradeTapsWeaken(true);
        UpgradeDPS(true);
        UpgradeFirstStrike(true);
        UpgradeCompoundingDPS(true);
        UpgradeArtillery(true);
        UpgradeMoneyGenerationRaw(true);
        UpgradeMoneyMultiplier(true);
        UpgradeKillMoney(true);
        UpgradeAshProduction(true);
        UpgradeTapSkip(true);
        UpgradeDPSSkip(true);
        UpgradeSkillSkip(true);
        UpgradeOverAllSkip(true);
        UpgradeLightningBolt(true);
        UpgradeFlamethrower(true);
        UpgradePlunder(true);
        UpgradeCooldownReduction(true);
    }
    //Tapping Upgrades//
    public void UpgradeTapDamage(bool forText = false)
    {
        double initial = 70.0 / clicker.discount;
        double scalefactor = 100.0 / clicker.discount;
        double cost = (clicker.upgrades[0] * scalefactor) + initial;
        double nextcost = ((clicker.upgrades[0] + 1) * scalefactor) + initial;
        double currentPerBuyMaximum = currentPerBuy;
        // currentPerBuy == -1.0 means that it is selected to buy max
        if (currentPerBuy == -1.0)
        {
            currentPerBuyMaximum = System.Math.Floor(((-2.0 * cost)+scalefactor+System.Math.Sqrt(System.Math.Pow((2.0 * cost)-scalefactor, 2.0)+(scalefactor*8.0)*clicker.money))/(scalefactor*2.0));//lmao have fun deciphering this.
            if(currentPerBuyMaximum <= 0.0)
            {
                currentPerBuyMaximum = 1.0;
            }
        }
        if(currentPerBuyMaximum > 1.0)
        {
            nextcost = (cost + (scalefactor * (currentPerBuyMaximum) / 2.0)) * (currentPerBuyMaximum + 1.0);
            cost = (cost + (scalefactor * (currentPerBuyMaximum - 1.0) / 2.0)) * (currentPerBuyMaximum);
        }
        if (forText)
        {
            upgradeText[0].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum, false, 0, "F0") + " Tap Damage | $" + texts.GiveScientificForm(cost, false, 0, "F0") + " | " + texts.GiveScientificForm(clicker.clickDamage) + " Base Tap Damage";
            return;
        }
        if (clicker.money >= cost)
        {
            functions.giveMoney(cost, true, true);
            clicker.upgrades[0] += (int)currentPerBuyMaximum;
            clicker.clickDamage += currentPerBuyMaximum;
            upgradeText[0].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum, false, 0, "F0") + " Tap Damage | $" + texts.GiveScientificForm(nextcost,false,0,"F0") + " | " + texts.GiveScientificForm(clicker.clickDamage) + " Base Tap Damage";
            texts.UpdateMoney();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    public void UpgradeFlameConversion(bool forText = false)
    {
        double initial = 50.0 / clicker.discount;
        double scalefactor = 150.0 / clicker.discount;
        double cost = (clicker.upgrades[1] * scalefactor) + initial;
        double nextcost = ((clicker.upgrades[1] + 1) * scalefactor) + initial;
        double currentPerBuyMaximum = currentPerBuy;

        if (currentPerBuy == -1.0)
        {
            currentPerBuyMaximum = System.Math.Floor(((-2.0 * cost) + scalefactor + System.Math.Sqrt(System.Math.Pow((2.0 * cost) - scalefactor, 2.0) + (scalefactor * 8.0) * clicker.money)) / (scalefactor * 2.0));//lmao have fun deciphering this.
            if (currentPerBuyMaximum <= 0.0)
            {
                currentPerBuyMaximum = 1.0;
            }
        }
        if (currentPerBuyMaximum + (double)clicker.upgrades[1] > 50.0)
        {
            currentPerBuyMaximum = 50.0 - (double)clicker.upgrades[1]; // Cap out the maximum purchases to 50.
            if(currentPerBuyMaximum < 1.0)
            {
                cost = 0.0;
                nextcost = 0.0;
            }
        }
        if (currentPerBuyMaximum > 1.0)
        {
            nextcost = (cost + (scalefactor * (currentPerBuyMaximum) / 2.0)) * (currentPerBuyMaximum + 1.0);
            cost = (cost + (scalefactor * (currentPerBuyMaximum - 1.0) / 2.0)) * (currentPerBuyMaximum);
        }
        if (forText)
        {
            upgradeText[1].text = "+" + texts.GiveScientificForm(30.0 * currentPerBuyMaximum, false, 0, "F0") + "% Fire Conversion | $" + texts.GiveScientificForm(nextcost, false, 0, "F0") + " | " + (clicker.flameDPS * 100.0).ToString("F0") + "% Tap -> DPS";
            return;
        }
        if (clicker.money >= cost)
        {
            functions.giveMoney(cost, true, true);
            clicker.upgrades[1] += (int)currentPerBuyMaximum;
            clicker.flameDPS += 0.3 * currentPerBuyMaximum;
            upgradeText[1].text = "+" + texts.GiveScientificForm(30.0 * currentPerBuyMaximum, false, 0, "F0") + "% Fire Conversion | $" + texts.GiveScientificForm(nextcost, false, 0, "F0") + " | " + (clicker.flameDPS*100.0).ToString("F0") + "% Tap -> DPS";
            texts.UpdateMoney();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    public void UpgradeTapStealing(bool forText = false)
    {
        double initial = 500.0 / clicker.discount;
        double scalefactor = 600.0 / clicker.discount;
        double cost = (clicker.upgrades[2] * scalefactor) + initial;
        double nextcost = ((clicker.upgrades[2] + 1) * scalefactor) + initial;
        double currentPerBuyMaximum = currentPerBuy;
        // currentPerBuy == -1.0 means that it is selected to buy max
        if (currentPerBuy == -1.0)
        {
            currentPerBuyMaximum = System.Math.Floor(((-2.0 * cost) + scalefactor + System.Math.Sqrt(System.Math.Pow((2.0 * cost) - scalefactor, 2.0) + (scalefactor * 8.0) * clicker.money)) / (scalefactor * 2.0));//lmao have fun deciphering this.
            if (currentPerBuyMaximum <= 0.0)
            {
                currentPerBuyMaximum = 1.0;
            }
        }
        if (currentPerBuyMaximum > 1.0)
        {
            nextcost = (cost + (scalefactor * (currentPerBuyMaximum) / 2.0)) * (currentPerBuyMaximum + 1.0);
            cost = (cost + (scalefactor * (currentPerBuyMaximum - 1.0) / 2.0)) * (currentPerBuyMaximum);
        }
        if (forText)
        {
            upgradeText[2].text = "+" + texts.GiveScientificForm(2.0 * currentPerBuyMaximum, false, 0, "F0") + " Taps Give Money | $" + texts.GiveScientificForm(cost, false, 0, "F0") + " | " + texts.GiveScientificForm(clicker.stealMoney) + " Stealing Power";
            return;
        }
        if (clicker.money >= cost)
        {
            functions.giveMoney(cost, true, true);
            clicker.upgrades[2] += (int)currentPerBuyMaximum;
            clicker.stealMoney += 2.0 * currentPerBuyMaximum;
            upgradeText[2].text = "+" + texts.GiveScientificForm(2.0 * currentPerBuyMaximum, false, 0, "F0") + " Taps Give Money | $" + texts.GiveScientificForm(nextcost,false,0,"F0") + " | " + texts.GiveScientificForm(clicker.stealMoney) + " Stealing Power";
            texts.UpdateMoney();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    public void UpgradeTapsWeaken(bool forText = false)
    {
        double initial = 2500.0 / clicker.discount;
        double scalefactor = 5000.0 / clicker.discount;
        double cost = (clicker.upgrades[3] * scalefactor) + initial;
        double nextcost = ((clicker.upgrades[3] + 1) * scalefactor) + initial;
        double currentPerBuyMaximum = currentPerBuy;
        // currentPerBuy == -1.0 means that it is selected to buy max
        if (currentPerBuy == -1.0)
        {
            currentPerBuyMaximum = System.Math.Floor(((-2.0 * cost) + scalefactor + System.Math.Sqrt(System.Math.Pow((2.0 * cost) - scalefactor, 2.0) + (scalefactor * 8.0) * clicker.money)) / (scalefactor * 2.0));//lmao have fun deciphering this.
            if (currentPerBuyMaximum <= 0.0)
            {
                currentPerBuyMaximum = 1.0;
            }
        }
        if(currentPerBuyMaximum + (double)clicker.upgrades[3] > 10.0)
        {
            currentPerBuyMaximum = 10.0 - (double)clicker.upgrades[3]; // Cap out the maximum purchases to 10.
            if (currentPerBuyMaximum < 1.0)
            {
                cost = 0.0;
                nextcost = 0.0;
            }
        }
        if (currentPerBuyMaximum > 1.0)
        {
            nextcost = (cost + (scalefactor * (currentPerBuyMaximum) / 2.0)) * (currentPerBuyMaximum + 1.0);
            cost = (cost + (scalefactor * (currentPerBuyMaximum - 1.0) / 2.0)) * (currentPerBuyMaximum);
        }
        if (forText)
        {
            upgradeText[3].text = "+" + texts.GiveScientificForm(5.0 * currentPerBuyMaximum, false, 0, "F0") + "% Taps Weaken Victim | $" + texts.GiveScientificForm(cost, false, 0, "F0") + " | " + texts.GiveScientificForm(clicker.tapsWeaken * 100.0) + "% Dmg boost on hit";
            return;
        }
        if (clicker.money >= cost)
        {
            functions.giveMoney(cost, true, true);
            clicker.upgrades[3] += (int)currentPerBuyMaximum;
            clicker.tapsWeaken += 0.05 * currentPerBuyMaximum;
            upgradeText[3].text = "+" + texts.GiveScientificForm(5.0 * currentPerBuyMaximum, false, 0, "F0") + "% Taps Weaken Victim | $" + texts.GiveScientificForm(nextcost,false,0,"F0") + " | " + texts.GiveScientificForm(clicker.tapsWeaken*100.0) + "% Dmg boost on hit";
            texts.UpdateMoney();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    //DPS Upgrades//
    public void UpgradeDPS(bool forText = false)
    {
        double initial = 50.0 / clicker.discount;
        double scalefactor = 50.0 / clicker.discount;
        double cost = (clicker.upgrades[4] * scalefactor) + initial;
        double nextcost = ((clicker.upgrades[4] + 1) * scalefactor) + initial;
        double currentPerBuyMaximum = currentPerBuy;
        // currentPerBuy == -1.0 means that it is selected to buy max
        if (currentPerBuy == -1.0)
        {
            currentPerBuyMaximum = System.Math.Floor(((-2.0 * cost) + scalefactor + System.Math.Sqrt(System.Math.Pow((2.0 * cost) - scalefactor, 2.0) + (scalefactor * 8.0) * clicker.money)) / (scalefactor * 2.0));//lmao have fun deciphering this.
            if (currentPerBuyMaximum <= 0.0)
            {
                currentPerBuyMaximum = 1.0;
            }
        }
        if (currentPerBuyMaximum > 1.0)
        {
            nextcost = (cost + (scalefactor * (currentPerBuyMaximum) / 2.0)) * (currentPerBuyMaximum + 1.0);
            cost = (cost + (scalefactor * (currentPerBuyMaximum - 1.0) / 2.0)) * (currentPerBuyMaximum);
        }
        if (forText)
        {
            upgradeText[4].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum, false, 0, "F0") + " DPS | $" + texts.GiveScientificForm(cost, false, 0, "F0") + " | " + texts.GiveScientificForm(clicker.autoDPS) + " Base DPS";
            return;
        }
        if (clicker.money >= cost)
        {
            functions.giveMoney(cost, true, true);
            clicker.upgrades[4] += (int)currentPerBuyMaximum;
            clicker.autoDPS += currentPerBuyMaximum;
            upgradeText[4].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum, false, 0, "F0") + " DPS | $" + texts.GiveScientificForm(nextcost,false,0,"F0") + " | " + texts.GiveScientificForm(clicker.autoDPS) + " Base DPS";
            texts.UpdateMoney();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    public void UpgradeFirstStrike(bool forText = false)
    {
        double initial = 200.0 / clicker.discount;
        double scalefactor = 100.0 / clicker.discount;
        double cost = (clicker.upgrades[5] * scalefactor) + initial;
        double nextcost = ((clicker.upgrades[5] + 1) * scalefactor) + initial;
        double currentPerBuyMaximum = currentPerBuy;
        // currentPerBuy == -1.0 means that it is selected to buy max
        if (currentPerBuy == -1.0)
        {
            currentPerBuyMaximum = System.Math.Floor(((-2.0 * cost) + scalefactor + System.Math.Sqrt(System.Math.Pow((2.0 * cost) - scalefactor, 2.0) + (scalefactor * 8.0) * clicker.money)) / (scalefactor * 2.0));//lmao have fun deciphering this.
            if (currentPerBuyMaximum <= 0.0)
            {
                currentPerBuyMaximum = 1.0;
            }
        }
        if (currentPerBuyMaximum > 1.0)
        {
            nextcost = (cost + (scalefactor * (currentPerBuyMaximum) / 2.0)) * (currentPerBuyMaximum + 1.0);
            cost = (cost + (scalefactor * (currentPerBuyMaximum - 1.0) / 2.0)) * (currentPerBuyMaximum);
        }
        if (forText)
        {
            upgradeText[5].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum * 30.0, false, 0, "F0") + "% First Hit DMG | $" + texts.GiveScientificForm(cost, false, 0, "F0") + " | " + texts.GiveScientificForm(clicker.firstStrike, false, 0,"F1") + "x DMG";
            return;
        }
        if (clicker.money >= cost)
        {
            functions.giveMoney(cost, true, true);
            clicker.upgrades[5] += (int)currentPerBuyMaximum;
            clicker.firstStrike += 0.3 * currentPerBuyMaximum;
            upgradeText[5].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum * 30.0, false, 0, "F0") + "% First Hit DMG | $" + texts.GiveScientificForm(nextcost,false,0,"F0") + " | " + texts.GiveScientificForm(clicker.firstStrike,false,0,"F1") + "x DMG";
            texts.UpdateMoney();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    public void UpgradeCompoundingDPS(bool forText = false)
    {
        double initial = 1000.0 / clicker.discount;
        double scalefactor = 1000.0 / clicker.discount;
        double cost = (clicker.upgrades[6] * scalefactor) + initial;
        double nextcost = ((clicker.upgrades[6] + 1) * scalefactor) + initial;
        double currentPerBuyMaximum = currentPerBuy;
        // currentPerBuy == -1.0 means that it is selected to buy max
        if (currentPerBuy == -1.0)
        {
            currentPerBuyMaximum = System.Math.Floor(((-2.0 * cost) + scalefactor + System.Math.Sqrt(System.Math.Pow((2.0 * cost) - scalefactor, 2.0) + (scalefactor * 8.0) * clicker.money)) / (scalefactor * 2.0));//lmao have fun deciphering this.
            if (currentPerBuyMaximum <= 0.0)
            {
                currentPerBuyMaximum = 1.0;
            }
        }
        if (currentPerBuyMaximum + (double)clicker.upgrades[6] > 15.0)
        {
            currentPerBuyMaximum = 15.0 - (double)clicker.upgrades[6]; // Cap out the maximum purchases to 15.
            if (currentPerBuyMaximum < 1.0)
            {
                cost = 0.0;
                nextcost = 0.0;
            }
        }
        if (currentPerBuyMaximum > 1.0)
        {
            nextcost = (cost + (scalefactor * (currentPerBuyMaximum) / 2.0)) * (currentPerBuyMaximum + 1.0);
            cost = (cost + (scalefactor * (currentPerBuyMaximum - 1.0) / 2.0)) * (currentPerBuyMaximum);
        }
        if (forText)
        {
            upgradeText[6].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum * 5.0, false, 0, "F0") + "% DPS per second | $" + texts.GiveScientificForm(cost, false, 0, "F0") + " | " + texts.GiveScientificForm(clicker.dpsWeaken * 100.0) + "% DMG Bonus";
            return;
        }
        if (clicker.money >= cost)
        {
            functions.giveMoney(cost, true, true);
            clicker.upgrades[6] += (int)currentPerBuyMaximum;
            clicker.dpsWeaken += 0.05 * currentPerBuyMaximum;
            upgradeText[6].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum * 5.0, false, 0, "F0") + "% DPS per second | $" + texts.GiveScientificForm(nextcost,false,0,"F0") + " | " + texts.GiveScientificForm(clicker.dpsWeaken*100.0) + "% DMG Bonus";
            texts.UpdateMoney();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    public void UpgradeArtillery(bool forText = false)
    {
        double cost = 5000.0 / clicker.discount;
        if (forText)
        {
            string toggle = " | DISABLED";
            if (clicker.artillery)
            {
                toggle = " | ENABLED";
            }
            upgradeText[7].text = "Explosives | $" + cost.ToString("F0") + toggle;
            return;
        }
        if ((clicker.upgrades[7] < 1) && clicker.money >= cost)
        {
            functions.giveMoney(cost, true, true);
            clicker.upgrades[7]++;
            clicker.artillery = true;
            upgradeText[7].text = "Explosives | $" + cost.ToString("F0") + " | ENABLED";
            texts.UpdateMoney();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    //Generation Upgrades//
    public void UpgradeMoneyGenerationRaw(bool forText = false)
    {
        double initial = 30.0 / clicker.discount;
        double scalefactor = 30.0 / clicker.discount;
        double cost = (clicker.upgrades[8] * scalefactor) + initial;
        double nextcost = ((clicker.upgrades[8] + 1) * scalefactor) + initial;
        double currentPerBuyMaximum = currentPerBuy;
        // currentPerBuy == -1.0 means that it is selected to buy max
        if (currentPerBuy == -1.0)
        {
            currentPerBuyMaximum = System.Math.Floor(((-2.0 * cost) + scalefactor + System.Math.Sqrt(System.Math.Pow((2.0 * cost) - scalefactor, 2.0) + (scalefactor * 8.0) * clicker.money)) / (scalefactor * 2.0));//lmao have fun deciphering this.
            if (currentPerBuyMaximum <= 0.0)
            {
                currentPerBuyMaximum = 1.0;
            }
        }
        if (currentPerBuyMaximum > 1.0)
        {
            nextcost = (cost + (scalefactor * (currentPerBuyMaximum) / 2.0)) * (currentPerBuyMaximum + 1.0);
            cost = (cost + (scalefactor * (currentPerBuyMaximum - 1.0) / 2.0)) * (currentPerBuyMaximum);
        }
        if (forText)
        {
            upgradeText[8].text = "+$" + texts.GiveScientificForm(currentPerBuyMaximum * 3.0, false, 0, "F0") + "/S | $" + texts.GiveScientificForm(cost, false, 0, "F0") + " | $" + texts.GiveScientificForm(clicker.moneypersecond) + " Per Second";
            return;
        }
        if (clicker.money >= cost)
        {
            functions.giveMoney(cost, true, true);
            clicker.upgrades[8] += (int)currentPerBuyMaximum;
            clicker.moneypersecond += 3.0 * currentPerBuyMaximum;
            upgradeText[8].text = "+$" + texts.GiveScientificForm(currentPerBuyMaximum * 3.0, false, 0, "F0") + "/S | $" + texts.GiveScientificForm(nextcost, false, 0, "F0") + " | $" + texts.GiveScientificForm(clicker.moneypersecond) + " Per Second";
            texts.UpdateMoney();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    public void UpgradeMoneyMultiplier(bool forText = false)
    {
        double initial = 300.0 / clicker.discount;
        double scalefactor = 300.0 / clicker.discount;
        double cost = (clicker.upgrades[9] * scalefactor) + initial;
        double nextcost = ((clicker.upgrades[9] + 1) * scalefactor) + initial;
        double currentPerBuyMaximum = currentPerBuy;
        // currentPerBuy == -1.0 means that it is selected to buy max
        if (currentPerBuy == -1.0)
        {
            currentPerBuyMaximum = System.Math.Floor(((-2.0 * cost) + scalefactor + System.Math.Sqrt(System.Math.Pow((2.0 * cost) - scalefactor, 2.0) + (scalefactor * 8.0) * clicker.money)) / (scalefactor * 2.0));//lmao have fun deciphering this.
            if (currentPerBuyMaximum <= 0.0)
            {
                currentPerBuyMaximum = 1.0;
            }
        }
        if (currentPerBuyMaximum > 1.0)
        {
            nextcost = (cost + (scalefactor * (currentPerBuyMaximum) / 2.0)) * (currentPerBuyMaximum + 1.0);
            cost = (cost + (scalefactor * (currentPerBuyMaximum - 1.0) / 2.0)) * (currentPerBuyMaximum);
        }
        if (forText)
        {
            upgradeText[9].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum * 20.0, false, 0, "F0") + "% $ Production | $" + texts.GiveScientificForm(cost, false, 0, "F0") + " | $" + texts.GiveScientificForm(clicker.moneyMult, false, 0, "F1") + "x Gained";
            return;
        }
        if (clicker.money >= cost)
        {
            functions.giveMoney(cost, true, true);
            clicker.upgrades[9] += (int)currentPerBuyMaximum;
            clicker.moneyMult += 0.2 * currentPerBuyMaximum;
            upgradeText[9].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum * 20.0, false, 0, "F0") + "% $ Production | $" + texts.GiveScientificForm(nextcost,false,0,"F0") + " | $" + texts.GiveScientificForm(clicker.moneyMult,false,0,"F1") + "x Gained";
            texts.UpdateMoney();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    public void UpgradeKillMoney(bool forText = false)
    {
        double initial = 600.0 / clicker.discount;
        double scalefactor = 1200.0 / clicker.discount;
        double cost = (clicker.upgrades[10] * scalefactor) + initial;
        double nextcost = ((clicker.upgrades[10] + 1) * scalefactor) + initial;
        double currentPerBuyMaximum = currentPerBuy;
        // currentPerBuy == -1.0 means that it is selected to buy max
        if (currentPerBuy == -1.0)
        {
            currentPerBuyMaximum = System.Math.Floor(((-2.0 * cost) + scalefactor + System.Math.Sqrt(System.Math.Pow((2.0 * cost) - scalefactor, 2.0) + (scalefactor * 8.0) * clicker.money)) / (scalefactor * 2.0));//lmao have fun deciphering this.
            if (currentPerBuyMaximum <= 0.0)
            {
                currentPerBuyMaximum = 1.0;
            }
        }
        if (currentPerBuyMaximum > 1.0)
        {
            nextcost = (cost + (scalefactor * (currentPerBuyMaximum) / 2.0)) * (currentPerBuyMaximum + 1.0);
            cost = (cost + (scalefactor * (currentPerBuyMaximum - 1.0) / 2.0)) * (currentPerBuyMaximum);
        }
        if (forText)
        {
            upgradeText[10].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum, false, 0, "F0") + " Kills Gain Money | $" + texts.GiveScientificForm(cost, false, 0, "F0") + " | " + texts.GiveScientificForm(clicker.moneyperkill, false, 0, "F1") + " Stealing Power";
            return;
        }
        if (clicker.money >= cost)
        {
            functions.giveMoney(cost, true, true);
            clicker.upgrades[10] += (int)currentPerBuyMaximum;
            clicker.moneyperkill += 1.0 * currentPerBuyMaximum;
            upgradeText[10].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum, false, 0, "F0") + " Kills Gain Money | $" + texts.GiveScientificForm(nextcost,false,0,"F0") + " | " + texts.GiveScientificForm(clicker.moneyperkill,false,0,"F1") + " Stealing Power";
            texts.UpdateMoney();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    public void UpgradeAshProduction(bool forText = false)
    {
        double initial = 3000.0 / clicker.discount;
        double scalefactor = 2500.0 / clicker.discount;
        double cost = (clicker.upgrades[11] * scalefactor) + initial;
        double nextcost = ((clicker.upgrades[11] + 1) * scalefactor) + initial;
        double currentPerBuyMaximum = currentPerBuy;
        // currentPerBuy == -1.0 means that it is selected to buy max
        if (currentPerBuy == -1.0)
        {
            currentPerBuyMaximum = System.Math.Floor(((-2.0 * cost) + scalefactor + System.Math.Sqrt(System.Math.Pow((2.0 * cost) - scalefactor, 2.0) + (scalefactor * 8.0) * clicker.money)) / (scalefactor * 2.0));//lmao have fun deciphering this.
            if (currentPerBuyMaximum <= 0.0)
            {
                currentPerBuyMaximum = 1.0;
            }
        }
        if (currentPerBuyMaximum > 1.0)
        {
            nextcost = (cost + (scalefactor * (currentPerBuyMaximum) / 2.0)) * (currentPerBuyMaximum + 1.0);
            cost = (cost + (scalefactor * (currentPerBuyMaximum - 1.0) / 2.0)) * (currentPerBuyMaximum);
        }
        if (forText)
        {
            upgradeText[11].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum, false, 0, "F0") + "/m Attention Generation | $" + texts.GiveScientificForm(cost, false, 0, "F0") + " | " + texts.GiveScientificForm(clicker.ashPerMinute) + "/ m Attention";
            return;
        }
        if (clicker.money >= cost)
        {
            functions.giveMoney(cost, true, true);
            clicker.upgrades[11] += (int)currentPerBuyMaximum;
            clicker.ashPerMinute += 1.0 * currentPerBuyMaximum;
            upgradeText[11].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum, false, 0, "F0") + "/m Attention Generation | $" + texts.GiveScientificForm(nextcost,false,0,"F0") + " | " + texts.GiveScientificForm(clicker.ashPerMinute) + "/ m Attention";
            texts.UpdateMoney();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    //Skipping Upgrades
    public void UpgradeTapSkip(bool forText = false)
    {
        double cost = (clicker.upgrades[12] * 15.0) + 15.0;
        double nextcost = ((clicker.upgrades[12] + 1) * 15.0) + 15.0;
        if (forText)
        {
            upgradeText[12].text = "+1 Tap Skipping | " + texts.GiveScientificForm(cost, false, 0, "F0") + " Attention | " + clicker.tapSkip.ToString("F0") + " Stages/Kill";
            return;
        }
        if (clicker.upgrades[12] < 4.0 && clicker.ash >= cost)
        {
            functions.giveAsh(cost, true, true);
            clicker.upgrades[12]++;
            clicker.tapSkip += 1.0;
            upgradeText[12].text = "+1 Tap Skipping | " + texts.GiveScientificForm(nextcost,false,0,"F0") + " Attention | " + clicker.tapSkip.ToString("F0") + " Stages/Kill";
            texts.UpdateAsh();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    public void UpgradeDPSSkip(bool forText = false)
    {
        double cost = (clicker.upgrades[13] * 17.0) + 20.0;
        double nextcost = ((clicker.upgrades[13] + 1) * 17.0) + 20.0;
        if (forText)
        {
            upgradeText[13].text = "+1 DPS Skipping | " + texts.GiveScientificForm(cost, false, 0, "F0") + " Attention | " + clicker.dpsSkip.ToString("F0") + " Stages/Kill";
            return;
        }
        if (clicker.upgrades[13] < 4.0 && clicker.ash >= cost)
        {
            functions.giveAsh(cost, true, true);
            clicker.upgrades[13]++;
            clicker.dpsSkip += 1.0;
            upgradeText[13].text = "+1 DPS Skipping | " + texts.GiveScientificForm(nextcost,false,0,"F0") + " Attention | " + clicker.dpsSkip.ToString("F0") + " Stages/Kill";
            texts.UpdateAsh();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    public void UpgradeSkillSkip(bool forText = false)
    {
        double cost = (clicker.upgrades[14] * 20.0) + 15.0;
        double nextcost = ((clicker.upgrades[14] + 1) * 20.0) + 15.0;
        if (forText)
        {
            upgradeText[14].text = "+1 Skill Skipping | " + texts.GiveScientificForm(cost, false, 0, "F0") + " Attention | " + clicker.skillSkip.ToString("F0") + " Stages/Kill";
            return;
        }
        if (clicker.upgrades[14] < 4.0 && clicker.ash >= cost)
        {
            functions.giveAsh(cost, true, true);
            clicker.upgrades[14]++;
            clicker.skillSkip += 1.0;
            upgradeText[14].text = "+1 Skill Skipping | " + texts.GiveScientificForm(nextcost,false,0,"F0") + " Attention | " + clicker.skillSkip.ToString("F0") + " Stages/Kill";
            texts.UpdateAsh();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    public void UpgradeOverAllSkip(bool forText = false)
    {
        double cost = (clicker.upgrades[15] * 30.0) + 40.0;
        double nextcost = ((clicker.upgrades[15] + 1) * 30.0) + 40.0;
        if (forText)
        {
            upgradeText[15].text = "+1 Skip to All | " + texts.GiveScientificForm(cost, false, 0, "F0") + " Attention | " + clicker.overallSkip.ToString("F0") + " Stages/Kill";
            return;
        }
        if (clicker.upgrades[15] < 5.0 && clicker.ash >= cost)
        {
            functions.giveAsh(cost, true, true);
            clicker.upgrades[15]++;
            clicker.overallSkip += 1.0;
            upgradeText[15].text = "+1 Skip to All | " + texts.GiveScientificForm(nextcost,false,0,"F0") + " Attention | " + clicker.overallSkip.ToString("F0") + " Stages/Kill";
            texts.UpdateAsh();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    //Skill
    public void UpgradeLightningBolt(bool forText = false)//SkillDamage
    {
        double initial = 30.0;
        double scalefactor = 30.0;
        double cost = (clicker.upgrades[16] * scalefactor) + initial;
        double nextcost = ((clicker.upgrades[16] + 1) * scalefactor) + initial;
        double currentPerBuyMaximum = currentPerBuy;
        // currentPerBuy == -1.0 means that it is selected to buy max
        if (currentPerBuy == -1.0)
        {
            currentPerBuyMaximum = System.Math.Floor(((-2.0 * cost) + scalefactor + System.Math.Sqrt(System.Math.Pow((2.0 * cost) - scalefactor, 2.0) + (scalefactor * 8.0) * clicker.ash)) / (scalefactor * 2.0));//lmao have fun deciphering this.
            if (currentPerBuyMaximum <= 0.0)
            {
                currentPerBuyMaximum = 1.0;
            }
        }
        if (currentPerBuyMaximum > 1.0)
        {
            nextcost = (cost + (scalefactor * (currentPerBuyMaximum) / 2.0)) * (currentPerBuyMaximum + 1.0);
            cost = (cost + (scalefactor * (currentPerBuyMaximum - 1.0) / 2.0)) * (currentPerBuyMaximum);
        }
        if (forText)
        {
            upgradeText[16].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum * 10.0, false, 0, "F0") + "% Skill Damage | " + texts.GiveScientificForm(cost, false, 0, "F0") + " Attention | " + texts.GiveScientificForm(clicker.skillDamage, false, 0, "F2") + "x Skill Damage";
            return;
        }
        if (clicker.ash >= cost)
        {
            functions.giveAsh(cost, true, true);
            clicker.upgrades[16] += (int)currentPerBuyMaximum;
            clicker.skillDamage += 0.1 * currentPerBuyMaximum;
            upgradeText[16].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum * 10.0, false, 0, "F0") + "% Skill Damage | " + texts.GiveScientificForm(nextcost,false,0,"F0") + " Attention | " + texts.GiveScientificForm(clicker.skillDamage,false,0,"F2") + "x Skill Damage";
            texts.UpdateAsh();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    public void UpgradeFlamethrower(bool forText = false)//SkillpointMultiplier
    {
        double initial = 300.0;
        double scalefactor = 300.0;
        double cost = (clicker.upgrades[17] * scalefactor) + initial;
        double nextcost = ((clicker.upgrades[17] + 1) * scalefactor) + initial;
        double currentPerBuyMaximum = currentPerBuy;
        // currentPerBuy == -1.0 means that it is selected to buy max
        if (currentPerBuy == -1.0)
        {
            currentPerBuyMaximum = System.Math.Floor(((-2.0 * cost) + scalefactor + System.Math.Sqrt(System.Math.Pow((2.0 * cost) - scalefactor, 2.0) + (scalefactor * 8.0) * clicker.ash)) / (scalefactor * 2.0));//lmao have fun deciphering this.
            if (currentPerBuyMaximum <= 0.0)
            {
                currentPerBuyMaximum = 1.0;
            }
        }
        if (currentPerBuyMaximum > 1.0)
        {
            nextcost = (cost + (scalefactor * (currentPerBuyMaximum) / 2.0)) * (currentPerBuyMaximum + 1.0);
            cost = (cost + (scalefactor * (currentPerBuyMaximum - 1.0) / 2.0)) * (currentPerBuyMaximum);
        }
        if (forText)
        {
            upgradeText[17].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum * 15.0, false, 0, "F0") + "% Skill Point Gain | " + texts.GiveScientificForm(cost, false, 0, "F0") + " Attention | " + texts.GiveScientificForm(clicker.skillpointMultiplier, false, 0, "F2") + "x Skill Points";
            return;
        }
        if (clicker.ash >= cost)
        {
            functions.giveAsh(cost, true, true);
            clicker.upgrades[17] += (int)currentPerBuyMaximum;
            clicker.skillpointMultiplier += 0.15 * currentPerBuyMaximum;
            upgradeText[17].text = "+" + texts.GiveScientificForm(currentPerBuyMaximum * 15.0, false, 0, "F0")  + "% Skill Point Gain | " + texts.GiveScientificForm(nextcost,false,0,"F0") + " Attention | " + texts.GiveScientificForm(clicker.skillpointMultiplier,false,0,"F2") + "x Skill Points";
            texts.UpdateAsh();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    public void UpgradePlunder(bool forText = false)//CheaperSkills
    {
        double cost = (clicker.upgrades[18] * 700.0) + 300.0;
        double nextcost = ((clicker.upgrades[18] + 1) * 700.0) + 300.0;
        if (forText)
        {
            upgradeText[18].text = "-5% Skill Point Costs | " + texts.GiveScientificForm(cost, false, 0, "F0") + " Attention | " + clicker.skillcostMultiplier.ToString("F2") + "x Costs";
            return;
        }
        if (clicker.upgrades[18] < 10.0 && clicker.ash >= cost)
        {
            functions.giveAsh(cost, true, true);
            clicker.upgrades[18]++;
            clicker.skillcostMultiplier -= 0.05;
            upgradeText[18].text = "-5% Skill Point Costs | " + texts.GiveScientificForm(nextcost,false,0,"F0") + " Attention | " + clicker.skillcostMultiplier.ToString("F2") + "x Costs";
            texts.UpdateAsh();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
    public void UpgradeCooldownReduction(bool forText = false)
    {
        double cost = (clicker.upgrades[19] * 300.0) + 100.0;
        double nextcost = ((clicker.upgrades[19] + 1) * 300.0) + 100.0;
        if (forText)
        {
            upgradeText[19].text = "-5% Cooldown Reduction | " + texts.GiveScientificForm(cost, false, 0, "F0") + " Attention | -" + (100.0 - (clicker.cooldownReduction * 100.0)).ToString("F0") + "% Reduction";
            return;
        }
        if (clicker.upgrades[19] < 10.0 && clicker.ash >= cost)
        {
            functions.giveAsh(cost, true, true);
            clicker.upgrades[19]++;
            clicker.cooldownReduction -= 0.05;
            upgradeText[19].text = "-5% Cooldown Reduction | " + texts.GiveScientificForm(nextcost, false, 0, "F0") + " Attention | -" + (100.0 - (clicker.cooldownReduction * 100.0)).ToString("F0") + "% Reduction";
            texts.UpdateAsh();
            FindObjectOfType<SoundManager>().Play("buttonClick");
        }
    }
}
