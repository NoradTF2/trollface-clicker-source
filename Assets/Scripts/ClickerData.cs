using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
[System.Serializable]
public class ClickerData
{
    //Purpose of Clicker : Make all calculations for damage, skills & etc.
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
    public float currentVolume;
    public int soundPreset;
    public ClickerData (Clicker player)
    {
        clickDamage = player.clickDamage;
        autoDPS = player.autoDPS;
        flameDPS = player.flameDPS;
        tapsWeaken = player.tapsWeaken;
        dpsWeaken = player.dpsWeaken;
        firstStrike = player.firstStrike;
        attackspeed = player.attackspeed;
        attackTimer = player.attackTimer;
        //Skips
        tapSkip = player.tapSkip;
        dpsSkip = player.dpsSkip;
        skillSkip = player.skillSkip;
        overallSkip = player.overallSkip;
        //Resources
        money = player.money;
        ash = player.ash;
        ashPerMinute = player.ashPerMinute;
        moneypersecond = player.moneypersecond;
        moneyMult = player.moneyMult;
        moneyperkill = player.moneyperkill;
        stealMoney = player.stealMoney;
        skillpointMultiplier = player.skillpointMultiplier;
        skillcostMultiplier = player.skillcostMultiplier;
        //Skills
        lightningboltSkill = player.lightningboltSkill;
        flamethrowerSkill = player.flamethrowerSkill;
        plunderSkill = player.plunderSkill;
        cooldownReduction = player.cooldownReduction;
        skillDamage = player.skillDamage;
        //Skill Tree
        skillPoints = player.skillPoints;
        //Fire
        pyromancer = player.pyromancer;
        wildflame = player.wildflame;
        lingeringflame = player.lingeringflame;
        oldchaos = player.oldchaos;
        firecrackers = player.firecrackers;
        controlledflame = player.controlledflame;
        fireefficiency = player.fireefficiency;
        fireSpent = player.fireSpent;
        //Miracles
        clerical = player.clerical;
        endurance = player.endurance;
        inspire = player.inspire;
        lightningarrow = player.lightningarrow;
        dextrous = player.dextrous;
        warbanner = player.warbanner;
        miracleefficiency = player.miracleefficiency;
        miracleSpent = player.miracleSpent;
        //Strength
        warrior = player.warrior;
        bleed = player.bleed;
        forceduration = player.forceduration;
        forcedamage = player.forcedamage;
        twohanded = player.twohanded;
        armorpenetration = player.armorpenetration;
        heavybash = player.heavybash;
        flashcutsdamage = player.flashcutsdamage;
        flashcutschance = player.flashcutschance;
        strengthefficiency = player.strengthefficiency;
        strengthSpent = player.strengthSpent;
        //Leadership
        leadership = player.leadership;
        hasten = player.hasten;
        escapeplan = player.escapeplan;
        criticaltargeting = player.criticaltargeting;
        mines = player.mines;
        betterequipment = player.betterequipment;
        invasion = player.invasion;
        leadershipefficiency = player.leadershipefficiency;
        leadershipSpent = player.leadershipSpent;
        //Cognitive
        wisdom = player.wisdom;
        discount = player.discount;
        counter = player.counter;
        weakspot = player.weakspot;
        luck = player.luck;
        wisdomefficiency = player.wisdomefficiency;
        skillefficiency = player.skillefficiency;
        efficiency = player.efficiency;
        cognitiveSpent = player.cognitiveSpent;
        //Bools
        artillery = player.artillery;
        //Integers
        stage = player.stage;
        higheststage = player.higheststage;
        highestcurrentstage = player.highestcurrentstage;
        prestiges = player.prestiges;
        upgrades = player.upgrades;
        skills = player.skills;
        //Preferences
        currentVolume = player.currentVolume;
        soundPreset = player.soundPreset;
    }
}
