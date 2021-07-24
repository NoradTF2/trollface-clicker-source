using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using UnityEngine.Audio;
[System.Serializable]
public class Clicker : MonoBehaviour
{
    //Purpose of Clicker : Store data.
    public EnemyBehavior victim;
    public ClickerTexts texts;
    public UpgradeMenu upgrademenu;
    public SettingsMenu settings;
    public SkillTree skilltree;
    //Damages
    public double clickDamage = 1.0;
    public double autoDPS = 1.0;
    public double flameDPS = 0.0;
    public double tapsWeaken = 0.0;
    public double dpsWeaken = 0.0;
    public double firstStrike = 1.0;
    public float attackspeed = 1.0f;
    public float attackTimer = 1.0f;
    //Skips
    public double tapSkip = 1.0;
    public double dpsSkip = 1.0;
    public double skillSkip = 1.0;
    public double overallSkip = 0.0;
    //Resources
    public double money = 1500.0;
    public double ash = 0.0;
    public double ashPerMinute = 0.0;
    public double moneypersecond = 1.0;
    public double moneyMult = 1.0;
    public double moneyperkill = 0.0;
    public double stealMoney = 0.0;
    public double skillpointMultiplier = 1.0;
    public double skillcostMultiplier = 1.0;
    //Skills
    public double lightningboltSkill = 0.0;
    public double lightningboltCooldown = 20.0;
    public double flamethrowerSkill = 0.0;
    public double flamethrowerCooldown = 20.0;
    public double plunderSkill = 0.0;
    public double plunderCooldown = 20.0;
    public double cooldownReduction = 1.0;
    public double skillDamage = 1.0;
    //Skill Tree
    public double skillPoints = 0.0;
    //Fire
    public double pyromancer = 1.0;
    public double wildflame = 0.0;
    public double lingeringflame = 0.0;
    public double oldchaos = 1.0;
    public double firecrackers = 0.0;
    public double controlledflame = 1.0;
    public double fireefficiency = 1.0;
    public double fireSpent = 0.0;
    //Miracles
    public double clerical = 1.0;
    public double endurance = 0.0;
    public double inspire = 1.0;
    public double lightningarrow = 0.0;
    public double dextrous = 1.0;
    public double warbanner = 1.0;
    public double warbannerTime = 0.0;
    public double miracleefficiency = 1.0;
    public double miracleSpent = 0.0;
    //Strength
    public double warrior = 1.0;
    public double bleed = 0.0;
    public double forceduration = 0.0;
    public double forcedamage = 1.0;
    public double twohanded = 0.0;
    public double armorpenetration = 0.0;
    public double heavybash = 0.0;
    public double flashcutsdamage = 0.0;
    public double flashcutschance = 0.0;
    public double strengthefficiency = 1.0;
    public double strengthSpent = 0.0;
    //Leadership
    public double leadership = 1.0;
    public double hasten = 1.0;
    public double escapeplan = 1.0;
    public double criticaltargeting = 0.0;
    public double mines = 0.0;
    public double betterequipment = 1.0;
    public double invasion = 1.0;
    public double leadershipefficiency = 1.0;
    public double leadershipSpent = 0.0;
    //Cognitive
    public double wisdom = 1.0;
    public double discount = 1.0;
    public double counter = 1.0;
    public double weakspot = 0.0;
    public double luck = 1.0;
    public double wisdomefficiency = 1.0;
    public double skillefficiency = 1.0;
    public double efficiency = 1.0;
    public double cognitiveSpent = 0.0;
    //Bools
    public bool artillery = false;
    public bool isAlive = true;
    public bool flamethrowerActive = false;
    public bool plunderActive = false;
    //Integers
    public int stage = 1;
    public int higheststage = 1;
    public int highestcurrentstage = 1;
    public int prestiges = 0;
    public int ArtilleryCountdown = 15;
    public int[] upgrades;
    public int[] skills;
    //Preferences
    public float currentVolume = 0.25f;
    public int soundPreset;
    //Functions
    public void SavePlayer()
    {
        Save.SaveData(this);
    }
    public void LoadPlayer()
    {
        string path = Path.Combine(Application.persistentDataPath, "savedata.dat");
        if (File.Exists(path))
        {
            ClickerData data = Save.LoadPlayer();
            //Damages
            clickDamage = data.clickDamage;
            autoDPS = data.autoDPS;
            flameDPS = data.flameDPS;
            tapsWeaken = data.tapsWeaken;
            dpsWeaken = data.dpsWeaken;
            firstStrike = data.firstStrike;
            attackspeed = data.attackspeed;
            attackTimer = data.attackTimer;
            //Skips
            tapSkip = data.tapSkip;
            dpsSkip = data.dpsSkip;
            skillSkip = data.skillSkip;
            overallSkip = data.overallSkip;
            //Resources
            money = data.money;
            ash = data.ash;
            ashPerMinute = data.ashPerMinute;
            moneypersecond = data.moneypersecond;
            moneyMult = data.moneyMult;
            moneyperkill = data.moneyperkill;
            stealMoney = data.stealMoney;
            skillpointMultiplier = data.skillpointMultiplier;
            skillcostMultiplier = data.skillcostMultiplier;
            //Skills
            lightningboltSkill = data.lightningboltSkill;
            flamethrowerSkill = data.flamethrowerSkill;
            plunderSkill = data.plunderSkill;
            cooldownReduction = data.cooldownReduction;
            skillDamage = data.skillDamage;
            //Skill Tree
            skillPoints = data.skillPoints;
            //Fire
            pyromancer = data.pyromancer;
            wildflame = data.wildflame;
            lingeringflame = data.lingeringflame;
            oldchaos = data.oldchaos;
            firecrackers = data.firecrackers;
            controlledflame = data.controlledflame;
            fireefficiency = data.fireefficiency;
            fireSpent = data.fireSpent;
            //Miracles
            clerical = data.clerical;
            endurance = data.endurance;
            inspire = data.inspire;
            lightningarrow = data.lightningarrow;
            dextrous = data.dextrous;
            warbanner = data.warbanner;
            miracleefficiency = data.miracleefficiency;
            miracleSpent = data.miracleSpent;
            //Strength
            warrior = data.warrior;
            bleed = data.bleed;
            forceduration = data.forceduration;
            forcedamage = data.forcedamage;
            twohanded = data.twohanded;
            armorpenetration = data.armorpenetration;
            heavybash = data.heavybash;
            flashcutsdamage = data.flashcutsdamage;
            flashcutschance = data.flashcutschance;
            strengthefficiency = data.strengthefficiency;
            strengthSpent = data.strengthSpent;
            //Leadership
            leadership = data.leadership;
            hasten = data.hasten;
            escapeplan = data.escapeplan;
            criticaltargeting = data.criticaltargeting;
            mines = data.mines;
            betterequipment = data.betterequipment;
            invasion = data.invasion;
            leadershipefficiency = data.leadershipefficiency;
            leadershipSpent = data.leadershipSpent;
            //Cognitive
            wisdom = data.wisdom;
            discount = data.discount;
            counter = data.counter;
            weakspot = data.weakspot;
            luck = data.luck;
            wisdomefficiency = data.wisdomefficiency;
            skillefficiency = data.skillefficiency;
            efficiency = data.efficiency;
            cognitiveSpent = data.cognitiveSpent;
            //Bools
            artillery = data.artillery;
            //Integers
            stage = data.stage;
            higheststage = data.higheststage;
            highestcurrentstage = data.highestcurrentstage;
            prestiges = data.prestiges;
            upgrades = data.upgrades;
            skills = data.skills;
            //Preferences
            currentVolume = data.currentVolume;
            soundPreset = data.soundPreset;
            //less goooo..
            victim.SetEnemyHealth();
            texts.UpdateStage();
            texts.UpdateMoney();
            texts.UpdateAsh();
            texts.UpdateSkillpoints();
            skilltree.UpdatePrestigeValues();
            upgrademenu.UpdateAllUpgradesText();
        }
        settings.SetVolume(currentVolume);
        settings.volumeSlider.value = currentVolume;
        settings.soundDropdown.value = soundPreset;
    }
}
