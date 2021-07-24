using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickerTexts : MonoBehaviour
{
    public Clicker clicker;
    public UpgradeMenu upgrades;
    public Text stageText;
    public Text enemyHealthText;

    public Text infoText;
    public Text ashText;
    public Text skillPointsText;

    public Text moneyTextAnimation;
    public Text ashTextAnimation;

    public Text LightningBoltText;
    public Text FlamethrowerText;
    public Text PlunderText;
    public EnemyBehavior victim;

    public void UpdateSkillTexts()
    {
        if (clicker.lightningboltSkill > 0.0)
        {
            if (clicker.lightningboltCooldown > 0.0)
            {
                LightningBoltText.text = clicker.lightningboltCooldown.ToString("F0");
            }
            else
            {
                LightningBoltText.text = "MEDIA\nREADY";
            }
        }
        if(clicker.flamethrowerSkill > 0.0)
        {
            if (clicker.flamethrowerCooldown > 0.0)
            {
                FlamethrowerText.text = clicker.flamethrowerCooldown.ToString("F0");
            }
            else
            {
                FlamethrowerText.text = "V-AMP\nREADY";
            }
        }
        if (clicker.plunderSkill > 0.0)
        {
            if (clicker.plunderCooldown > 0.0)
            {
                PlunderText.text = clicker.plunderCooldown.ToString("F0");
            }
            else
            {
                PlunderText.text = "READY";
            }
        }
    }
    public void UpdateEnemyHealth()
    {
        string bufferTxt = "HP : " + GiveScientificForm(victim.currentHealth);
        if (clicker.stage % victim.bossStage == 0)
        {
            bufferTxt = "Boss HP : " + GiveScientificForm(victim.currentHealth);
        }
        if (victim.currentHealth <= 0.0)
        {
            bufferTxt = "HP : DEAD";
        }
        enemyHealthText.text = bufferTxt;
    }
    public void UpdateStage()
    {
        stageText.text = "STAGE\n" + clicker.stage.ToString();
    }
    public void UpdateMoney()
    {
        string MoneyText = "$ : " + GiveScientificForm(clicker.money);
        infoText.text = MoneyText;
        if (upgrades.currentPerBuy == -1.0)
        {
            upgrades.UpdateAllUpgradesText();
        }
    }
    public void UpdateAsh()
    {
        string AshText = "Attention : " + GiveScientificForm(clicker.ash);
        ashText.text = AshText;
    }
    public void UpdateSkillpoints()
    {
        string SPText = "Skill Points : " + GiveScientificForm(clicker.skillPoints, false, 0, "F2");
        skillPointsText.text = SPText;
    }
    public string GiveScientificForm(double number, bool activeSymbol = false, int forcedSymbol = 0, string decimalPlaces = "F0")
    {
        string symbol = "+";
        string output = "";
        if (number < 0.0)
        {
            symbol = "";
        }
        if(forcedSymbol == 1)
        {
            symbol = "+";
        }
        else if(forcedSymbol == 2)
        {
            symbol = "-";
        }
        if(activeSymbol)
        {
            output = symbol;
        }
        if (number >= 1000000.0)
        {
            double Exponent = System.Math.Floor(System.Math.Log10(System.Math.Abs(number)));
            double Mantissa = (number / System.Math.Pow(10, Exponent));
            output += Mantissa.ToString("F2") + "e" + Exponent.ToString("F0");
        }
        else
        {
            output += number.ToString(decimalPlaces);
        }
        return output;
    }
}
