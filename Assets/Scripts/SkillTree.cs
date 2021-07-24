using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public Text skillDescriptionText;
    public EnemyBehavior victim;
    public GameObject upgradebutton;
    public UpgradeMenu upgrademenu;
    public ClickerTexts texts;
    public Clicker clicker;
    public int skillSelected = -1;
    public int prestigeStage = 500;
    public Text PrestigeContent;

    public GameObject LightningBoltButton;
    public GameObject FlamethrowerButton;
    public GameObject PlunderButton;

    public void UpdatePrestigeValues()
    {
        if (clicker.stage > prestigeStage)
        {
            PrestigeContent.text = "You will gain " + texts.GiveScientificForm(System.Math.Pow((clicker.stage - prestigeStage), 1.2) * 0.5 * clicker.skillpointMultiplier) + " skill points from this & restart at stage " + (System.Math.Floor((double)clicker.stage * 0.35)).ToString("F0") + ". You must be at stage " + ((int)System.Math.Floor((double)clicker.higheststage * 0.9)).ToString() + " or higher to prestige.";
        }
    }
    public void Prestige()
    {
        //Prestige is allowed at stage 100 and above & must be 90% of the highest stage.
        if (clicker.stage >= prestigeStage && clicker.stage >= (int)System.Math.Floor((double)clicker.higheststage * 0.9))
        {
            clicker.skillPoints += System.Math.Pow((clicker.stage - prestigeStage), 1.2) * 0.5 * clicker.skillpointMultiplier;
            //This cuts the time of prestiging by a lot!
            clicker.money = System.Math.Floor(1500.0 + (System.Math.Pow(clicker.stage, 1.7) * 30.0));
            clicker.stage = (int)System.Math.Floor((double)clicker.stage * 0.35);
            clicker.highestcurrentstage = clicker.stage;
            //Prestige resets everything except skills.
            clicker.clickDamage = 1.0;
            clicker.autoDPS = 1.0;
            clicker.flameDPS = 0.0;
            clicker.tapsWeaken = 0.0;
            clicker.dpsWeaken = 0.0;
            clicker.firstStrike = 1.0;
            clicker.tapSkip = 1.0;
            clicker.dpsSkip = 1.0;
            clicker.skillSkip = 1.0;
            clicker.overallSkip = 0.0;
            clicker.ash = 0.0;
            clicker.ashPerMinute = 0.0;
            clicker.moneypersecond = 1.0;
            clicker.moneyMult = 1.0;
            clicker.moneyperkill = 0.0;
            clicker.stealMoney = 0.0;
            clicker.skillpointMultiplier = 1.0;
            clicker.skillcostMultiplier = 1.0;
            clicker.cooldownReduction = 1.0;
            clicker.skillDamage = 1.0;
            clicker.artillery = false;
            clicker.flamethrowerActive = false;
            clicker.plunderActive = false;
            clicker.ArtilleryCountdown = 15;
            texts.UpdateStage();
            texts.UpdateMoney();
            //stat counter.
            clicker.prestiges++;
            //clear upgrade array for costs
            Array.Clear(clicker.upgrades, 0, clicker.upgrades.Length);
            victim.SetEnemyHealth();
            //text
            PrestigeContent.text = "You have prestiged!";
            upgrademenu.UpdateAllUpgradesText();

            switch (clicker.soundPreset)
            {
                case 0:
                    {
                        FindObjectOfType<SoundManager>().Play("normalprestige");
                        break;
                    }
                case 1:
                    {
                        FindObjectOfType<SoundManager>().Play("2000prestige");
                        break;
                    }
                case 2:
                    {
                        FindObjectOfType<SoundManager>().Play("ironyprestige");
                        break;
                    }
            }
        }
    }
    public void ResetSkilltree()
    {
        clicker.skillPoints += clicker.fireSpent;
        clicker.skillPoints += clicker.miracleSpent;
        clicker.skillPoints += clicker.strengthSpent;
        clicker.skillPoints += clicker.leadershipSpent;
        clicker.skillPoints += clicker.cognitiveSpent;

        clicker.fireSpent = 0.0;
        clicker.miracleSpent = 0.0;
        clicker.strengthSpent = 0.0;
        clicker.leadershipSpent = 0.0;
        clicker.cognitiveSpent = 0.0;
        Array.Clear(clicker.skills, 0, clicker.upgrades.Length);
    }
    public void onSkillUpgraded()
    {
        int skillNum = skillSelected;
        if (skillNum >= 0 && skillNum <= clicker.skills.Length)
        {
            double skillpointCost = 1.0;
            bool boughtSkill = false;
            switch (skillNum)
            {
                //Fire
                case 0:
                    {
                        skillpointCost = (1.0 + System.Math.Pow(clicker.skills[skillNum], 2.0)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost)
                        {
                            boughtSkill = true;
                            clicker.pyromancer += 0.4 + System.Math.Pow(clicker.skills[skillNum], 0.4);
                            clicker.skillPoints -= skillpointCost;
                            clicker.fireSpent += skillpointCost;
                        }
                        break;
                    }
                case 1:
                    {
                        skillpointCost = (2.0 + System.Math.Pow(clicker.skills[skillNum], 1.5)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.fireSpent > 5.0)
                        {
                            boughtSkill = true;
                            clicker.wildflame += 0.15 + System.Math.Pow(clicker.skills[skillNum], 0.2);
                            clicker.skillPoints -= skillpointCost;
                            clicker.fireSpent += skillpointCost;
                        }
                        break;
                    }
                case 2:
                    {
                        skillpointCost = (2.0 + System.Math.Pow(clicker.skills[skillNum], 1.8)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.fireSpent > 5.0)
                        {
                            boughtSkill = true;
                            clicker.lingeringflame += 0.125 + System.Math.Pow(clicker.skills[skillNum], 0.125);
                            clicker.skillPoints -= skillpointCost;
                            clicker.fireSpent += skillpointCost;
                        }
                        break;
                    }
                case 3:
                    {
                        skillpointCost = (2.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.fireSpent > 5.0)
                        {
                            boughtSkill = true;
                            clicker.flamethrowerSkill += 0.2 + System.Math.Pow(clicker.skills[skillNum], 0.4);
                            clicker.skillPoints -= skillpointCost;
                            clicker.fireSpent += skillpointCost;
                            FlamethrowerButton.SetActive(true);
                        }
                        break;
                    }
                case 4:
                    {
                        skillpointCost = (4.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.fireSpent > 50.0)
                        {
                            boughtSkill = true;
                            clicker.oldchaos += 0.2 + System.Math.Pow(clicker.skills[skillNum], 0.2);
                            clicker.skillPoints -= skillpointCost;
                            clicker.fireSpent += skillpointCost;
                        }
                        break;
                    }
                case 5:
                    {
                        skillpointCost = (4.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.fireSpent > 50.0)
                        {
                            boughtSkill = true;
                            clicker.firecrackers += 0.1 + System.Math.Pow(clicker.skills[skillNum], 0.12);
                            clicker.skillPoints -= skillpointCost;
                            clicker.fireSpent += skillpointCost;
                        }
                        break;
                    }
                case 6:
                    {
                        skillpointCost = (4.0 + System.Math.Pow(clicker.skills[skillNum], 1.7)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.fireSpent > 50.0)
                        {
                            boughtSkill = true;
                            clicker.controlledflame += 0.1 + System.Math.Pow(clicker.skills[skillNum], 0.15);
                            clicker.skillPoints -= skillpointCost;
                            clicker.fireSpent += skillpointCost;
                        }
                        break;
                    }
                case 7:
                    {
                        skillpointCost = (10.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.fireSpent > 200.0)
                        {
                            boughtSkill = true;
                            clicker.fireefficiency += 0.05 + System.Math.Pow(clicker.skills[skillNum], 0.05);
                            clicker.skillPoints -= skillpointCost;
                            clicker.fireSpent += skillpointCost;
                        }
                        break;
                    }
                //Miracles
                case 8:
                    {
                        skillpointCost = (1.0 + System.Math.Pow(clicker.skills[skillNum], 2.0)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost)
                        {
                            boughtSkill = true;
                            clicker.clerical += 0.4 + System.Math.Pow(clicker.skills[skillNum], 0.4);
                            clicker.skillPoints -= skillpointCost;
                            clicker.miracleSpent += skillpointCost;
                        }
                        break;
                    }
                case 9:
                    {
                        skillpointCost = (2.0 + System.Math.Pow(clicker.skills[skillNum], 1.5)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.miracleSpent > 5.0)
                        {
                            boughtSkill = true;
                            clicker.lightningboltSkill += 0.2 + System.Math.Pow(clicker.skills[skillNum], 0.5);
                            clicker.skillPoints -= skillpointCost;
                            clicker.miracleSpent += skillpointCost;
                            LightningBoltButton.SetActive(true);
                        }
                        break;
                    }
                case 10:
                    {
                        skillpointCost = (2.0 + System.Math.Pow(clicker.skills[skillNum], 1.8)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.miracleSpent > 5.0 && clicker.endurance < 50.0)
                        {
                            boughtSkill = true;
                            clicker.endurance += 0.75 + System.Math.Pow(clicker.skills[skillNum], 0.9);
                            clicker.skillPoints -= skillpointCost;
                            clicker.miracleSpent += skillpointCost;
                        }
                        break;
                    }
                case 11:
                    {
                        skillpointCost = (2.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.miracleSpent > 5.0)
                        {
                            boughtSkill = true;
                            clicker.inspire += 0.2 + System.Math.Pow(clicker.skills[skillNum], 0.2);
                            clicker.skillPoints -= skillpointCost;
                            clicker.miracleSpent += skillpointCost;
                        }
                        break;
                    }
                case 12:
                    {
                        skillpointCost = (4.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.miracleSpent > 50.0)
                        {
                            boughtSkill = true;
                            clicker.lightningarrow += 0.03 + System.Math.Pow(clicker.skills[skillNum], 0.06);
                            clicker.skillPoints -= skillpointCost;
                            clicker.miracleSpent += skillpointCost;
                        }
                        break;
                    }
                case 13:
                    {
                        skillpointCost = (4.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.miracleSpent > 50.0)
                        {
                            boughtSkill = true;
                            clicker.dextrous += 0.1 + System.Math.Pow(clicker.skills[skillNum], 0.1);
                            clicker.skillPoints -= skillpointCost;
                            clicker.miracleSpent += skillpointCost;
                        }
                        break;
                    }
                case 14:
                    {
                        skillpointCost = (4.0 + System.Math.Pow(clicker.skills[skillNum], 1.7)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.miracleSpent > 50.0)
                        {
                            boughtSkill = true;
                            clicker.warbanner += 0.15 + System.Math.Pow(clicker.skills[skillNum], 0.2);
                            clicker.skillPoints -= skillpointCost;
                            clicker.miracleSpent += skillpointCost;
                        }
                        break;
                    }
                case 15:
                    {
                        skillpointCost = (10.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.miracleSpent > 200.0)
                        {
                            boughtSkill = true;
                            clicker.miracleefficiency += 0.05 + System.Math.Pow(clicker.skills[skillNum], 0.05);
                            clicker.skillPoints -= skillpointCost;
                            clicker.miracleSpent += skillpointCost;
                        }
                        break;
                    }
                //Strength
                case 16:
                    {
                        skillpointCost = (1.0 + System.Math.Pow(clicker.skills[skillNum], 2.0)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost)
                        {
                            boughtSkill = true;
                            clicker.warrior += 0.4 + System.Math.Pow(clicker.skills[skillNum], 0.4);
                            clicker.skillPoints -= skillpointCost;
                            clicker.strengthSpent += skillpointCost;
                        }
                        break;
                    }
                case 17:
                    {
                        skillpointCost = (2.0 + System.Math.Pow(clicker.skills[skillNum], 1.5)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.strengthSpent > 5.0)
                        {
                            boughtSkill = true;
                            clicker.bleed += 0.1 + System.Math.Pow(clicker.skills[skillNum], 0.15);
                            clicker.skillPoints -= skillpointCost;
                            clicker.strengthSpent += skillpointCost;
                        }
                        break;
                    }
                case 18:
                    {
                        skillpointCost = (2.0 + System.Math.Pow(clicker.skills[skillNum], 1.8)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.strengthSpent > 5.0)
                        {
                            boughtSkill = true;
                            clicker.forcedamage += 0.22 + System.Math.Pow(clicker.skills[skillNum], 0.22);
                            if (clicker.forceduration < 0.7)
                            {
                                clicker.forceduration += 0.1;
                            }
                            clicker.skillPoints -= skillpointCost;
                            clicker.strengthSpent += skillpointCost;
                        }
                        break;
                    }
                case 19:
                    {
                        skillpointCost = (2.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.strengthSpent > 5.0)
                        {
                            boughtSkill = true;
                            clicker.twohanded += 0.1 + System.Math.Pow(clicker.skills[skillNum], 0.1);
                            clicker.skillPoints -= skillpointCost;
                            clicker.strengthSpent += skillpointCost;
                        }
                        break;
                    }
                case 20:
                    {
                        skillpointCost = (4.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.strengthSpent > 50.0)
                        {
                            boughtSkill = true;
                            clicker.armorpenetration += 0.25 + System.Math.Pow(clicker.skills[skillNum], 0.25);
                            clicker.skillPoints -= skillpointCost;
                            clicker.strengthSpent += skillpointCost;
                        }
                        break;
                    }
                case 21:
                    {
                        skillpointCost = (4.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.strengthSpent > 50.0 && clicker.heavybash < 29.0)
                        {
                            boughtSkill = true;
                            clicker.heavybash += 0.5 + System.Math.Pow(clicker.skills[skillNum], 0.5);
                            clicker.skillPoints -= skillpointCost;
                            clicker.strengthSpent += skillpointCost;
                        }
                        break;
                    }
                case 22:
                    {
                        skillpointCost = (4.0 + System.Math.Pow(clicker.skills[skillNum], 1.7)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.strengthSpent > 50.0)
                        {
                            boughtSkill = true;
                            clicker.flashcutschance += 0.06 + System.Math.Pow(clicker.skills[skillNum], 0.06);
                            clicker.flashcutsdamage += 0.03 + System.Math.Pow(clicker.skills[skillNum], 0.05);
                            clicker.skillPoints -= skillpointCost;
                            clicker.strengthSpent += skillpointCost;
                        }
                        break;
                    }
                case 23:
                    {
                        skillpointCost = (10.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.strengthSpent > 200.0)
                        {
                            boughtSkill = true;
                            clicker.strengthefficiency += 0.05 + System.Math.Pow(clicker.skills[skillNum], 0.05);
                            clicker.skillPoints -= skillpointCost;
                            clicker.strengthSpent += skillpointCost;
                        }
                        break;
                    }
                //Leadership
                case 24:
                    {
                        skillpointCost = (1.0 + System.Math.Pow(clicker.skills[skillNum], 2.0)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost)
                        {
                            boughtSkill = true;
                            clicker.leadership += 0.4 + System.Math.Pow(clicker.skills[skillNum], 0.4);
                            clicker.skillPoints -= skillpointCost;
                            clicker.leadershipSpent += skillpointCost;
                        }
                        break;
                    }
                case 25:
                    {
                        skillpointCost = (2.0 + System.Math.Pow(clicker.skills[skillNum], 1.5)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.leadershipSpent > 5.0 && clicker.hasten < 100.0)//0.01 is the max speed for a float timer.
                        {
                            boughtSkill = true;
                            clicker.hasten += 0.2 + System.Math.Pow(clicker.skills[skillNum], 0.2);
                            clicker.skillPoints -= skillpointCost;
                            clicker.leadershipSpent += skillpointCost;
                        }
                        break;
                    }
                case 26:
                    {
                        skillpointCost = (2.0 + System.Math.Pow(clicker.skills[skillNum], 1.8)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.leadershipSpent > 5.0)
                        {
                            boughtSkill = true;
                            clicker.escapeplan += 0.25 + System.Math.Pow(clicker.skills[skillNum], 0.25);
                            clicker.skillPoints -= skillpointCost;
                            clicker.leadershipSpent += skillpointCost;
                        }
                        break;
                    }
                case 27:
                    {
                        skillpointCost = (2.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.leadershipSpent > 5.0)
                        {
                            boughtSkill = true;
                            clicker.criticaltargeting += 0.1 + System.Math.Pow(clicker.skills[skillNum], 0.1);
                            clicker.skillPoints -= skillpointCost;
                            clicker.leadershipSpent += skillpointCost;
                        }
                        break;
                    }
                case 28:
                    {
                        skillpointCost = (4.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.leadershipSpent > 50.0)
                        {
                            boughtSkill = true;
                            clicker.mines += 0.3 + System.Math.Pow(clicker.skills[skillNum], 0.3);
                            clicker.skillPoints -= skillpointCost;
                            clicker.leadershipSpent += skillpointCost;
                        }
                        break;
                    }
                case 29:
                    {
                        skillpointCost = (4.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.leadershipSpent > 50.0 && clicker.betterequipment <= 50.0)//0.01 is the max speed for a float timer, and I highly doubt you'll be needing the difference between 0.02s and 0.01s.
                        {
                            boughtSkill = true;
                            clicker.betterequipment += 0.2 + System.Math.Pow(clicker.skills[skillNum], 0.2);
                            clicker.skillPoints -= skillpointCost;
                            clicker.leadershipSpent += skillpointCost;
                        }
                        break;
                    }
                case 30:
                    {
                        skillpointCost = (4.0 + System.Math.Pow(clicker.skills[skillNum], 1.7)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.leadershipSpent > 50.0)
                        {
                            boughtSkill = true;
                            clicker.invasion += 0.1 + System.Math.Pow(clicker.skills[skillNum], 0.2);
                            clicker.skillPoints -= skillpointCost;
                            clicker.leadershipSpent += skillpointCost;
                        }
                        break;
                    }
                case 31:
                    {
                        skillpointCost = (10.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.leadershipSpent > 200.0)
                        {
                            boughtSkill = true;
                            clicker.leadershipefficiency += 0.05 + System.Math.Pow(clicker.skills[skillNum], 0.05);
                            clicker.skillPoints -= skillpointCost;
                            clicker.leadershipSpent += skillpointCost;
                        }
                        break;
                    }
                //Leadership
                case 32:
                    {
                        skillpointCost = (1.0 + System.Math.Pow(clicker.skills[skillNum], 2.0)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost)
                        {
                            boughtSkill = true;
                            clicker.wisdom += 0.4 + System.Math.Pow(clicker.skills[skillNum], 0.4);
                            clicker.skillPoints -= skillpointCost;
                            clicker.cognitiveSpent += skillpointCost;
                        }
                        break;
                    }
                case 33:
                    {
                        skillpointCost = (2.0 + System.Math.Pow(clicker.skills[skillNum], 1.5)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.cognitiveSpent > 5.0 && clicker.discount < 5.0)
                        {
                            boughtSkill = true;
                            clicker.discount += 0.2 + System.Math.Pow(clicker.skills[skillNum], 0.2);
                            upgrademenu.UpdateAllUpgradesText();
                            clicker.skillPoints -= skillpointCost;
                            clicker.cognitiveSpent += skillpointCost;
                        }
                        break;
                    }
                case 34:
                    {
                        skillpointCost = (2.0 + System.Math.Pow(clicker.skills[skillNum], 1.8)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.cognitiveSpent > 5.0)
                        {
                            boughtSkill = true;
                            clicker.counter += 0.4 + System.Math.Pow(clicker.skills[skillNum], 0.4);
                            clicker.skillPoints -= skillpointCost;
                            clicker.cognitiveSpent += skillpointCost;
                        }
                        break;
                    }
                case 35:
                    {
                        skillpointCost = (2.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.cognitiveSpent > 5.0)
                        {
                            boughtSkill = true;
                            clicker.weakspot += 0.08 + System.Math.Pow(clicker.skills[skillNum], 0.08);
                            clicker.skillPoints -= skillpointCost;
                            clicker.cognitiveSpent += skillpointCost;
                        }
                        break;
                    }
                case 36:
                    {
                        skillpointCost = (4.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.cognitiveSpent > 50.0)
                        {
                            boughtSkill = true;
                            clicker.luck += 0.08 + System.Math.Pow(clicker.skills[skillNum], 0.08);
                            clicker.skillPoints -= skillpointCost;
                            clicker.cognitiveSpent += skillpointCost;
                        }
                        break;
                    }
                case 37:
                    {
                        skillpointCost = (4.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.cognitiveSpent > 50.0)
                        {
                            boughtSkill = true;
                            clicker.wisdomefficiency += 0.05 + System.Math.Pow(clicker.skills[skillNum], 0.05);
                            clicker.skillPoints -= skillpointCost;
                            clicker.cognitiveSpent += skillpointCost;
                        }
                        break;
                    }
                case 38:
                    {
                        skillpointCost = (4.0 + System.Math.Pow(clicker.skills[skillNum], 1.7)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.cognitiveSpent > 50.0)
                        {
                            boughtSkill = true;
                            clicker.skillefficiency += 0.07 + System.Math.Pow(clicker.skills[skillNum], 0.07);
                            clicker.skillPoints -= skillpointCost;
                            clicker.cognitiveSpent += skillpointCost;
                        }
                        break;
                    }
                case 39:
                    {
                        skillpointCost = (10.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier;
                        if (clicker.skillPoints >= skillpointCost && clicker.cognitiveSpent > 200.0)
                        {
                            boughtSkill = true;
                            clicker.efficiency += 0.05 + System.Math.Pow(clicker.skills[skillNum], 0.05);
                            clicker.skillPoints -= skillpointCost;
                            clicker.cognitiveSpent += skillpointCost;
                        }
                        break;
                    }
            }
            if(boughtSkill)
            {
                clicker.skills[skillNum]++;
                FindObjectOfType<SoundManager>().Play("buttonClick");
            }
            texts.UpdateSkillpoints();
        }
        OnSkillClicked(skillNum);
    }
    public void OnSkillClicked(int skillNum)
    {
        upgradebutton.SetActive(true);
        skillSelected = skillNum;

        switch (skillNum)
        {
            //Fire
            case 0:
                {
                    skillDescriptionText.text = "Cope | Increases all flame based damage. \n" + texts.GiveScientificForm(clicker.pyromancer, false, 0, "F2") + "x Fire Damage." + "\nNext upgrade costs " + texts.GiveScientificForm((1.0 + System.Math.Pow(clicker.skills[skillNum], 2.0)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    break;
                }
            case 1:
                {
                    skillDescriptionText.text = "Seethe | Increases crit chance for fire. \n" + texts.GiveScientificForm(clicker.wildflame * 100.0,false,0,"F2") + "% crit chance." + "\nNext upgrade costs " + texts.GiveScientificForm((2.0 + System.Math.Pow(clicker.skills[skillNum], 1.5)) * clicker.skillcostMultiplier, false,0,"F2") + " skill points.";
                    if(clicker.fireSpent < 5.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (5.0 - clicker.fireSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 2:
                {
                    skillDescriptionText.text = "Emotional Impact | Flame damage afflicts an aftertime damage effect. \n" + texts.GiveScientificForm(clicker.lingeringflame * 100.0,false,0,"F2") + "% aftertime." + "\nNext upgrade costs " + texts.GiveScientificForm((2.0 + System.Math.Pow(clicker.skills[skillNum], 1.8)) * clicker.skillcostMultiplier, false,0,"F2") + " skill points.";
                    if (clicker.fireSpent < 5.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (5.0 - clicker.fireSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 3:
                {
                    skillDescriptionText.text = "Voice Amplification | Adds a skill that constantly attacks while attack key is down. \n" + texts.GiveScientificForm(clicker.flamethrowerSkill,false,0,"F2") + "x damage." + "\nNext upgrade costs " + texts.GiveScientificForm((2.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier, false,0,"F2") + " skill points.";
                    if (clicker.fireSpent < 5.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (5.0 - clicker.fireSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 4:
                {
                    skillDescriptionText.text = "Trolling Frenzy | The less time left to kill, the more damage you deal. \n" + texts.GiveScientificForm(clicker.oldchaos,false,0,"F2") + "x Damage at 0 seconds left." + "\nNext upgrade costs " + texts.GiveScientificForm((4.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier, false,0,"F2") + " skill points.";
                    if (clicker.fireSpent < 50.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (50.0 - clicker.fireSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 5:
                {
                    skillDescriptionText.text = "Social Engineering | Any attack occasionally triggers a burst of fire damage. \n" + texts.GiveScientificForm(clicker.firecrackers,false,0,"F2") + "x Burst Damage." + "\nNext upgrade costs " + texts.GiveScientificForm((4.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier, false,0,"F2") + " skill points.";
                    if (clicker.fireSpent < 50.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (50.0 - clicker.fireSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 6:
                {
                    skillDescriptionText.text = "Memorable Words | Gives armor penetration to fire damage. \n" + texts.GiveScientificForm(clicker.controlledflame,false,0,"F2") + "x Less effective armor." + "\nNext upgrade costs " + texts.GiveScientificForm((4.0 + System.Math.Pow(clicker.skills[skillNum], 1.7)) * clicker.skillcostMultiplier, false,0,"F2") + " skill points.";
                    if (clicker.fireSpent < 50.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (50.0 - clicker.fireSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 7:
                {
                    skillDescriptionText.text = "Insult Efficiency | Multiplies the effect of other fire skills. \n" + texts.GiveScientificForm(clicker.fireefficiency,false,0,"F2") + "x Effect." + "\nNext upgrade costs " + texts.GiveScientificForm((10.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier, false,0,"F2") + " skill points.";
                    if (clicker.fireSpent < 200.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (200.0 - clicker.fireSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            //Miracles
            case 8:
                {
                    skillDescriptionText.text = "Botnet | Increases physical damage. \n" + texts.GiveScientificForm(clicker.clerical,false,0,"F2") + "x Physical damage." + "\nNext upgrade costs " + texts.GiveScientificForm((1.0 + System.Math.Pow(clicker.skills[skillNum], 2.0)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    break;
                }
            case 9:
                {
                    skillDescriptionText.text = "Media Attention | Deal an attack that has high skip and damage. \n" + texts.GiveScientificForm(clicker.lightningboltSkill,false,0,"F2") + "x damage." + "\nNext upgrade costs " + texts.GiveScientificForm((2.0 + System.Math.Pow(clicker.skills[skillNum], 1.5)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.miracleSpent < 5.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (5.0 - clicker.miracleSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 10:
                {
                    skillDescriptionText.text = "Attrition | Extends time for each kill. \n" + "+ " + texts.GiveScientificForm(clicker.endurance,false,0,"F2") + " seconds." + "\nNext upgrade costs " + texts.GiveScientificForm((2.0 + System.Math.Pow(clicker.skills[skillNum], 1.8)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.miracleSpent < 5.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (5.0 - clicker.miracleSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 11:
                {
                    skillDescriptionText.text = "Inspire | Increase ally damage. \n" + texts.GiveScientificForm(clicker.inspire,false,0,"F2") + "x Ally damage." + "\nNext upgrade costs " + texts.GiveScientificForm((2.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.miracleSpent < 5.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (5.0 - clicker.miracleSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 12:
                {
                    skillDescriptionText.text = "Media Feedback Loop | Every second, a portion of Media Attention's damage is done. \n" + texts.GiveScientificForm(clicker.lightningarrow,false,0,"F2") + "x damage." + "\nNext upgrade costs " + texts.GiveScientificForm((4.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.miracleSpent < 50.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (50.0 - clicker.miracleSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 13:
                {
                    skillDescriptionText.text = "Dextrous | Increases tap damage. \n" + texts.GiveScientificForm(clicker.dextrous,false,0,"F2") + "x Tap damage." + "\nNext upgrade costs " + texts.GiveScientificForm((4.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.miracleSpent < 50.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (50.0 - clicker.miracleSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 14:
                {
                    skillDescriptionText.text = "Encouragement Speech | Occasionally boosts allies damage. \n" + texts.GiveScientificForm(clicker.warbanner,false,0,"F2") + "x Ally damage." + "\nNext upgrade costs " + texts.GiveScientificForm((4.0 + System.Math.Pow(clicker.skills[skillNum], 1.7)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.miracleSpent < 50.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (50.0 - clicker.miracleSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 15:
                {
                    skillDescriptionText.text = "Hive Efficiency | Multiplies the effect of other miracle skills.  \n" + texts.GiveScientificForm(clicker.miracleefficiency,false,0,"F2") + "x Effect." + "\nNext upgrade costs " + texts.GiveScientificForm((10.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.miracleSpent < 200.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (200.0 - clicker.miracleSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            //Strength
            case 16:
                {
                    skillDescriptionText.text = "Dedication | Increases tap damage. \n" + texts.GiveScientificForm(clicker.warrior,false,0,"F2") + "x Tap damage." + "\nNext upgrade costs " + texts.GiveScientificForm((1.0 + System.Math.Pow(clicker.skills[skillNum], 2.0)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    break;
                }
            case 17:
                {
                    skillDescriptionText.text = "Bleed | Taps inflict a stacking damage over time. \n" + texts.GiveScientificForm(clicker.bleed,false,0,"F2") + "x Bleed damage." + "\nNext upgrade costs " + texts.GiveScientificForm((2.0 + System.Math.Pow(clicker.skills[skillNum], 1.5)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.strengthSpent < 5.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (5.0 - clicker.strengthSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 18:
                {
                    skillDescriptionText.text = "Unexpected Answer | Taps can occasionally stun. Stun increases damage and stops the timer. \n" + texts.GiveScientificForm(clicker.forceduration,false,0,"F2") + "s Stun duration. " + texts.GiveScientificForm(clicker.forcedamage,false,0,"F2") + "x Damage while stunned." + "\nNext upgrade costs " + texts.GiveScientificForm((2.0 + System.Math.Pow(clicker.skills[skillNum], 1.8)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.strengthSpent < 5.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (5.0 - clicker.strengthSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 19:
                {
                    skillDescriptionText.text = "Macro | Increases chance to tap multiple times. \n" + texts.GiveScientificForm(clicker.twohanded * 100.0, false, 0, "F2") + "% chance to hit multiple times." + "\nNext upgrade costs " + texts.GiveScientificForm((2.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.strengthSpent < 5.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (5.0 - clicker.strengthSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 20:
                {
                    skillDescriptionText.text = "Victim Information | Breaks down damage reductions. \n" + "-" + texts.GiveScientificForm(clicker.armorpenetration,false,0,"F2") + " armor on hit." + "\nNext upgrade costs " + texts.GiveScientificForm((4.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.strengthSpent < 50.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (50.0 - clicker.strengthSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 21:
                {
                    skillDescriptionText.text = "Spread of the Word | Tap kills randomly skip. \n" + texts.GiveScientificForm(clicker.heavybash,false,0,"F2") + " skip." + "\nNext upgrade costs " + texts.GiveScientificForm((4.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.strengthSpent < 50.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (50.0 - clicker.strengthSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 22:
                {
                    skillDescriptionText.text = "Trolling Machine | Increases chance to tap multiple times & successive combos of taps deal more damage. \n" + "+" + texts.GiveScientificForm(clicker.flashcutsdamage * 100.0,false,0,"F2") + "% damage per hit. \n" + "+" + texts.GiveScientificForm(clicker.flashcutschance * 100.0,false,0,"F2") + "% multihit." + "\nNext upgrade costs " + texts.GiveScientificForm((4.0 + System.Math.Pow(clicker.skills[skillNum], 1.7)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.strengthSpent < 50.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (50.0 - clicker.strengthSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 23:
                {
                    skillDescriptionText.text = "Brute Force Efficiency | Multiplies the effect of other strength skills.  \n" + texts.GiveScientificForm(clicker.strengthefficiency,false,0,"F2") + "x Effect." + "\nNext upgrade costs " + texts.GiveScientificForm((10.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.strengthSpent < 200.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (200.0 - clicker.strengthSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            //Leadership
            case 24:
                {
                    skillDescriptionText.text = "Leadership | Increases automatic damage. \n" + texts.GiveScientificForm(clicker.leadership,false,0,"F2") + "x Auto damage." + "\nNext upgrade costs " + texts.GiveScientificForm((1.0 + System.Math.Pow(clicker.skills[skillNum], 2.0)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    break;
                }
            case 25:
                {
                    skillDescriptionText.text = "Hasten | Decreases amount of time in between automatic damage. \n" + texts.GiveScientificForm(clicker.hasten,false,0,"F2") + "x Faster auto damage." + "\nNext upgrade costs " + texts.GiveScientificForm((2.0 + System.Math.Pow(clicker.skills[skillNum], 1.5)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.leadershipSpent < 5.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (5.0 - clicker.leadershipSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 26:
                {
                    skillDescriptionText.text = "Quick Wits | Increases damage the closer you are to time out. \n" + texts.GiveScientificForm(clicker.escapeplan,false,0,"F2") + "x Damage at 1 seconds remaining." + "\nNext upgrade costs " + texts.GiveScientificForm((2.0 + System.Math.Pow(clicker.skills[skillNum], 1.8)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.leadershipSpent < 5.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (5.0 - clicker.leadershipSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 27:
                {
                    skillDescriptionText.text = "Critical Targeting | Increases chance for automatic damage to crit. \n" + texts.GiveScientificForm(clicker.criticaltargeting * 100.0,false,0,"F2") + "% Crit chance." + "\nNext upgrade costs " + texts.GiveScientificForm((2.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.leadershipSpent < 5.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (5.0 - clicker.leadershipSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 28:
                {
                    skillDescriptionText.text = "Preparations | After certain intervals of time, enemies take a massive burst of damage. \n" + texts.GiveScientificForm(clicker.mines,false,0,"F2") + "x damage." + "\nNext upgrade costs " + texts.GiveScientificForm((4.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.leadershipSpent < 50.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (50.0 - clicker.leadershipSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 29:
                {
                    skillDescriptionText.text = "Better Equipment | Increases damage of automatic damage & shortens the respawn time. \n" + texts.GiveScientificForm(clicker.betterequipment,false,0,"F2") + "x Damage and respawn rate." + "\nNext upgrade costs " + texts.GiveScientificForm((4.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.leadershipSpent < 50.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (50.0 - clicker.leadershipSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 30:
                {
                    skillDescriptionText.text = "Invasion | Decreases enemy max health & you start off with a sudden burst of damage. \n" + texts.GiveScientificForm(clicker.invasion,false,0,"F2") + "x Less enemy health." + "\nNext upgrade costs " + texts.GiveScientificForm((4.0 + System.Math.Pow(clicker.skills[skillNum], 1.7)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.leadershipSpent < 50.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (50.0 - clicker.leadershipSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 31:
                {
                    skillDescriptionText.text = "Leadership Efficiency | Multiplies the effect of other leadership skills.  \n" + texts.GiveScientificForm(clicker.leadershipefficiency,false,0,"F2") + "x Effect." + "\nNext upgrade costs " + texts.GiveScientificForm((10.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.leadershipSpent < 200.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (200.0 - clicker.leadershipSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            //Cognitive
            case 32:
                {
                    skillDescriptionText.text = "Wisdom | Increases skill damage. \n" + texts.GiveScientificForm(clicker.wisdom,false,0,"F2") + "x Damage." + "\nNext upgrade costs " + texts.GiveScientificForm((1.0 + System.Math.Pow(clicker.skills[skillNum], 2.0)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    break;
                }
            case 33:
                {
                    skillDescriptionText.text = "Discount | Decreases cost of store upgrades. \n" + ((1.0 - (1.0 / clicker.discount)) * 100.0).ToString("F2") + "% cost decrease. Does not affect attention costs." + "\nNext upgrade costs " + texts.GiveScientificForm((2.0 + System.Math.Pow(clicker.skills[skillNum], 1.5)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.cognitiveSpent < 5.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (5.0 - clicker.cognitiveSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 34:
                {
                    skillDescriptionText.text = "Counter | Increases damage when less than 5 seconds remain. \n" + texts.GiveScientificForm(clicker.counter,false,0,"F2") + "x Damage." + "\nNext upgrade costs " + texts.GiveScientificForm((2.0 + System.Math.Pow(clicker.skills[skillNum], 1.8)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.cognitiveSpent < 5.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (5.0 - clicker.cognitiveSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 35:
                {
                    skillDescriptionText.text = "Weak Spot | Increases crit chance. \n" + texts.GiveScientificForm(clicker.weakspot * 100.0,false,0,"F2") + "% crit chance." + "\nNext upgrade costs " + texts.GiveScientificForm((2.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.cognitiveSpent < 5.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (5.0 - clicker.cognitiveSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 36:
                {
                    skillDescriptionText.text = "Luck | Increases chance of everything. \n" + texts.GiveScientificForm(clicker.luck,false,0,"F2") + "x Chance." + "\nNext upgrade costs " + texts.GiveScientificForm((4.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.cognitiveSpent < 50.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (50.0 - clicker.cognitiveSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 37:
                {
                    skillDescriptionText.text = "Wisdom Efficiency | Multiplies the effect of other wisdom skills. \n" + texts.GiveScientificForm(clicker.wisdomefficiency,false,0,"F2") + "x Effect." + "\nNext upgrade costs " + texts.GiveScientificForm((4.0 + System.Math.Pow(clicker.skills[skillNum], 2.8)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.cognitiveSpent < 50.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (50.0 - clicker.cognitiveSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 38:
                {
                    skillDescriptionText.text = "Skill Efficiency | Increases skill potentcy & cooldown reduction. \n" + texts.GiveScientificForm(clicker.skillefficiency,false,0,"F2") + "x Effect & cooldown." + "\nNext upgrade costs " + texts.GiveScientificForm((4.0 + System.Math.Pow(clicker.skills[skillNum], 1.7)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.cognitiveSpent < 50.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (50.0 - clicker.cognitiveSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            case 39:
                {
                    skillDescriptionText.text = "Efficiency | Multiplies the effect of other skills.  \n" + texts.GiveScientificForm(clicker.efficiency,false,0,"F2") + "x Effect." + "\nNext upgrade costs " + texts.GiveScientificForm((10.0 + System.Math.Pow(clicker.skills[skillNum], 2.5)) * clicker.skillcostMultiplier, false, 0, "F2") + " skill points.";
                    if (clicker.cognitiveSpent < 200.0)
                    {
                        skillDescriptionText.text += "\nRequires " + (200.0 - clicker.cognitiveSpent).ToString("F0") + " more skillpoints allocated to this tree.";
                    }
                    break;
                }
            default:
            {
                skillDescriptionText.text = "Click on a skill to see description & buy it.";
                break;
            }
        }
    }
}
