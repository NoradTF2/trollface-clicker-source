using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class Functions : MonoBehaviour
{
    public EnemyBehavior victim;
    public ClickerTexts texts;
    public Clicker clicker;
    public SkillTree skilltree;
    public UpgradeMenu upgrademenu;
    void Start()
    {
        InvokeRepeating("MoneyPerSecond", 3, 1);
        InvokeRepeating("AshPerMinute", 60, 60);
        InvokeRepeating("ReduceCooldowns", 0, 1);
        InvokeRepeating("OnApplicationQuit", 180, 180);
        clicker.LoadPlayer();
        upgrademenu.UpdateAllUpgradesText();

        if (clicker.lightningboltSkill > 0.0)
        {
            skilltree.LightningBoltButton.SetActive(true);
        }
        if (clicker.flamethrowerSkill > 0.0)
        {
            skilltree.FlamethrowerButton.SetActive(true);
        }
        if (clicker.plunderSkill > 0.0)
        {
            skilltree.PlunderButton.SetActive(true);
        }

        switch (clicker.soundPreset)
        {
            case 0:
                {
                    FindObjectOfType<SoundManager>().Play("normalBGM");
                    break;
                }
            case 1:
                {
                    FindObjectOfType<SoundManager>().Play("2000BGM");
                    break;
                }
            case 2:
                {
                    FindObjectOfType<SoundManager>().Play("ironyBGM");
                    break;
                }
        }
    }
    void OnApplicationQuit()
    {
        clicker.SavePlayer();
    }
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (clicker.flamethrowerActive && clicker.isAlive)
            {
                victim.DealDamage(((clicker.clickDamage * clicker.flamethrowerSkill * 4.0 * clicker.skillDamage * clicker.wisdom * clicker.efficiency * clicker.wisdomefficiency * clicker.skillefficiency * clicker.skillefficiency) / 50.0) / clicker.cooldownReduction, clicker.tapSkip + clicker.overallSkip + clicker.skillSkip, true, "fire");
                giveMoney(clicker.stealMoney * 2.0 / 9.0, true);
                victim.damagetakenMult += (clicker.tapsWeaken / 9.0);
                victim.hurtAnimation();
            }
        }
    }
    void MoneyPerSecond()
    {
        giveMoney(clicker.moneypersecond, true);
    }
    public void giveMoney(double moneyGained, bool textBool, bool negative = false)
    {
        if (negative)
        {
            moneyGained *= -1.0;
        }
        else
        {
            moneyGained *= clicker.moneyMult;
        }
        clicker.money += moneyGained;
        texts.UpdateMoney();
        if (gameObject.activeInHierarchy && moneyGained != 0.0 && textBool)
        {
            Text tempTextBox = Instantiate(texts.moneyTextAnimation, texts.moneyTextAnimation.transform) as Text;
            tempTextBox.transform.SetParent(victim.renderCanvas.transform, false);
            tempTextBox.text = texts.GiveScientificForm(moneyGained, true);
            Destroy(tempTextBox.gameObject, 0.3f);
        }
    }
    void AshPerMinute()
    {
        giveAsh(clicker.ashPerMinute, true);
    }
    public void giveAsh(double ashGained, bool textBool, bool negative = false)//souls.
    {
        if (negative)
        {
            ashGained *= -1.0;
        }
        clicker.ash += ashGained;
        texts.UpdateAsh();
        if (gameObject.activeInHierarchy && ashGained != 0.0 && textBool)
        {
            Text tempTextBox = Instantiate(texts.ashTextAnimation, texts.ashTextAnimation.transform) as Text;
            tempTextBox.transform.SetParent(victim.renderCanvas.transform, false);
            tempTextBox.text = texts.GiveScientificForm(ashGained, true);
            Destroy(tempTextBox.gameObject, 0.3f);
        }
    }
    public void Flames()
    {
        if (clicker.isAlive && clicker.flameDPS > 0.0)
        {
            victim.DealDamage(clicker.flameDPS * clicker.clickDamage, (clicker.tapSkip + clicker.overallSkip), true, "fire");
        }
    }
    public void DPS()
    {
        if (clicker.isAlive)
        {
            double damageDealt = clicker.autoDPS;
            victim.damagetakenMult += clicker.dpsWeaken;
            if (victim.isFirstDPSStrike == true)
            {
                damageDealt *= clicker.firstStrike;
                victim.isFirstDPSStrike = false;
            }
            if (clicker.lightningarrow > 0.0)
            {
                damageDealt += clicker.clickDamage * 10.0f * clicker.lightningboltSkill * clicker.lightningarrow;
                giveMoney(clicker.stealMoney * 1.5 * 2.0, false);
            }
            victim.DealDamage(damageDealt, (clicker.dpsSkip + clicker.overallSkip), true, "ally");
        }
    }
    public void ArtilleryCannon()
    {
        if (clicker.isAlive)
        {
            if (clicker.artillery == true)
            {
                double damageDealt = clicker.autoDPS * 50.0f;
                victim.damagetakenMult += clicker.dpsWeaken;
                damageDealt *= clicker.firstStrike;
                victim.DealDamage(damageDealt, 3.0 * (clicker.dpsSkip + clicker.overallSkip), true, "ally");
                victim.majorHurtAnimation();
                clicker.ArtilleryCountdown = 15;
            }
        }
    }
    public void LightningStrike()
    {
        if (clicker.lightningboltCooldown <= 0.0 && clicker.lightningboltSkill > 0)
        {
            double damageDealt = clicker.clickDamage * 100.0f * clicker.lightningboltSkill * clicker.skillDamage * clicker.wisdom * clicker.efficiency * clicker.wisdomefficiency * clicker.skillefficiency;
            clicker.lightningboltCooldown = 10.0 * clicker.cooldownReduction / clicker.skillefficiency;
            victim.DealDamage(damageDealt, 1.5 * (clicker.skillSkip + clicker.tapSkip + clicker.overallSkip) * clicker.lightningboltSkill, true, "ally");
            victim.majorHurtAnimation();
            giveMoney(clicker.stealMoney * 2.0 * 10.0, false);
        }
    }
    public void Flamethrower()
    {
        if (clicker.flamethrowerCooldown <= 0.0 && clicker.flamethrowerSkill > 0)
        {
            clicker.flamethrowerCooldown = 3.0;
            if (clicker.flamethrowerActive == true)
            {
                clicker.flamethrowerActive = false;
            }
            else
            {
                clicker.flamethrowerActive = true;
            }
        }
    }
    public void Plunder()
    {
        if (clicker.plunderCooldown <= 0.0 && clicker.plunderSkill > 0)
        {
            clicker.plunderCooldown = 300.0 * clicker.cooldownReduction;
            clicker.plunderActive = true;
        }
    }
    void ReduceCooldowns()
    {
        clicker.lightningboltCooldown -= 1.0;
        clicker.flamethrowerCooldown -= 1.0;
        clicker.plunderCooldown -= 1.0;

        if (clicker.lightningboltCooldown < 0.0)
        {
            clicker.lightningboltCooldown = 0.0;
        }
        if (clicker.flamethrowerCooldown < 0.0)
        {
            clicker.flamethrowerCooldown = 0.0;
        }
        if (clicker.plunderCooldown < 0.0)
        {
            clicker.plunderCooldown = 0.0;
        }
        texts.UpdateSkillTexts();
    }
}
