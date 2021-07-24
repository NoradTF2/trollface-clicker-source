using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemyBehavior : MonoBehaviour
     , IPointerClickHandler
{
    public Functions functions;
    public ClickerTexts texts;
    public SkillTree skilltree;
    public SoundManager soundmanager;
    public Clicker clicker;
    public ParticleSystem particle;
    public ParticleSystem majorParticle;
    public Animator hurtAnimator;
    public Text DamageText;
    public GameObject renderCanvas;
    public double currentHealth = 0;
    public double damagetakenMult = 1.0;
    public float timeRemaining = 20f;
    public int stageCap = 100000000;
    public double bossStage = 10.0;
    public bool isFirstDPSStrike = true;
    public float isStunned = 0.0f;
    private bool mines1 = false;
    private bool mines2 = false;
    private bool mines3 = false;
    public int armorStage = 1000;
    public Text timerText;
    public bool isOnPreviousStage = false;
    public double damageBatchNumber = 0.0;
    public float textDestructionTimer = 0.0f;
    public float CPSlimit = 30f;
    float timeout = 0;
    private bool isTextCurrentlyOut = false;
    public Text currentDamageNumber;
    public void GoToPreviousStage(bool truefalse)
    {
        if (clicker.stage > 1)
        {
            if (truefalse)
            {
                isOnPreviousStage = true;
            }
            else
            {
                isOnPreviousStage = false;
            }
            SetEnemyHealth();
            texts.UpdateStage();
            texts.UpdateMoney();
        }
    }
    public void GoToStage(string stageString)
    {
        int stageNum;
        int.TryParse(stageString, out stageNum);
        if(stageNum > clicker.highestcurrentstage)
        {
            stageNum = clicker.highestcurrentstage;
        }

        if (stageNum >= 1 && stageNum <= clicker.highestcurrentstage)
        {
            clicker.stage = stageNum;
            SetEnemyHealth();
            texts.UpdateStage();
            texts.UpdateMoney();
        }
    }
    void OnEnable()
    {
        SetEnemyHealth();
    }
    void Update()
    {
        if (timeRemaining > 0)
        {
            if (clicker.isAlive)
            {
                if (isStunned <= 0.0)
                {
                    timeRemaining -= Time.deltaTime;
                }
                else
                {
                    isStunned -= Time.deltaTime;
                }
                if (timeRemaining <= 15.0 && clicker.mines > 0.0)
                {
                    if (mines1 == false)
                    {
                        DealDamage(25.0 * clicker.autoDPS * clicker.mines * clicker.leadershipefficiency, clicker.dpsSkip, true, "ally");
                        mines1 = true;
                    }
                    else if (timeRemaining <= 10.0 && mines2 == false)
                    {
                        DealDamage(25.0 * clicker.autoDPS * clicker.mines * clicker.leadershipefficiency, clicker.dpsSkip, true, "ally");
                        mines2 = true;
                    }
                    else if (timeRemaining <= 5.0 && mines3 == false)
                    {
                        DealDamage(25.0 * clicker.autoDPS * clicker.mines * clicker.leadershipefficiency, clicker.dpsSkip, true, "ally");
                        mines3 = true;
                    }
                }
                if (clicker.warbannerTime > 0.0)
                {
                    clicker.warbannerTime -= Time.deltaTime;
                }
                timerText.text = "Time Left : " + timeRemaining.ToString("F2");

                if (clicker.attackTimer > 0.0f)
                {
                    clicker.attackTimer -= Time.deltaTime;
                }
                else
                {
                    if (Random.value <= 0.1 * clicker.luck && clicker.warbanner > 1.0)
                    {
                        clicker.warbannerTime = 7.0;
                    }
                    functions.DPS();
                    functions.Flames();
                    clicker.ArtilleryCountdown--;
                    clicker.attackspeed = 1.0f / (float)(clicker.hasten * clicker.leadershipefficiency * clicker.efficiency);
                    clicker.attackTimer = clicker.attackspeed;
                    if (clicker.ArtilleryCountdown <= 0)
                    {
                        functions.ArtilleryCannon();
                    }
                }
            }
            textDestructionTimer -= Time.deltaTime;

            if (isTextCurrentlyOut)
            {
                Color c = currentDamageNumber.color;
                c.a = textDestructionTimer * 3.33f;
                currentDamageNumber.color = c;
                if (textDestructionTimer <= 0.0f)
                {
                    Destroy(currentDamageNumber.gameObject);
                    isTextCurrentlyOut = false;
                    damageBatchNumber = 0.0;
                }
            }
        }
        else
        {
            SetEnemyHealth();
        }
        if (timeout > 0)
            timeout -= Time.deltaTime;
    }
    public void SetEnemyHealth()
    {
        double HPMultiplier = Mathf.Pow(Mathf.Round(clicker.stage), 2.5f + (clicker.stage/1000.0f)) * 15.0f;

        if(clicker.stage > 100)
        {
            HPMultiplier *= (clicker.stage / 100.0);
        }
        currentHealth = HPMultiplier;
        timeRemaining = 20f + (float)clicker.endurance;
        if (clicker.stage % bossStage == 0 && clicker.stage != 0)
        {
            currentHealth = HPMultiplier * (30.0 * (1.0 + (clicker.stage / 15.0)));
            timeRemaining = 100f + (float)(clicker.endurance);
        }
        texts.UpdateEnemyHealth();
        damagetakenMult = 1.0;
        isFirstDPSStrike = true;
        clicker.isAlive = true;
        gameObject.GetComponent<Image>().enabled = true;
        mines1 = false;
        mines2 = false;
        mines3 = false;
        currentHealth /= clicker.invasion * clicker.efficiency * clicker.leadershipefficiency;
        if (clicker.invasion > 1.0)
        {
            DealDamage(3.0 * clicker.autoDPS * clicker.efficiency * clicker.leadershipefficiency, clicker.dpsSkip, true, "ally");
        }
    }
    public void OnPointerClick(PointerEventData eventData) // 3
    {
        if (timeout <= 0)
        {
            ClickDamage();
            timeout = 1f / CPSlimit;//CPS cap.
        }
    }
    public void ClickDamage()
    {
        if (clicker.isAlive)
        {
            DealDamage(clicker.clickDamage, (clicker.tapSkip + clicker.overallSkip), true, "physical");
            damagetakenMult += clicker.tapsWeaken;
            if (clicker.stealMoney != 0.0)
            {
                functions.giveMoney(clicker.stealMoney * 2.0, true);
            }
            hurtAnimation();
        }
    }
    public void hurtAnimation()
    {
        hurtAnimator.SetTrigger("whenHurt");
        particle.Stop();
        particle.Play();
    }
    public void majorHurtAnimation()
    {
        hurtAnimator.SetTrigger("whenHurt");
        majorParticle.Stop();
        majorParticle.Play();
    }
    public void DealDamage(double damage, double stageSkip = 1, bool text = false, string damageType = "generic")
    {
        if (clicker.isAlive)
        {
            damage *= damagetakenMult;
            double critChance = clicker.weakspot;
            double armor = 1.0;
            if (clicker.stage >= armorStage)
            {
                double hundredStagesAhead = System.Math.Floor(((double)(clicker.stage - armorStage)) / 100.0);
                armor += (hundredStagesAhead);
            }
            if (damageType == "fire")
            {
                damage *= clicker.pyromancer * clicker.fireefficiency * clicker.efficiency;
                critChance += clicker.wildflame * clicker.fireefficiency * clicker.efficiency;
                if (clicker.oldchaos > 1.0)
                {
                    damage *= (clicker.oldchaos * clicker.fireefficiency * clicker.efficiency) / (timeRemaining + 1.0f);
                }
                if (clicker.firecrackers > 0.0)
                {
                    if (Random.value < 0.05 * clicker.fireefficiency * clicker.efficiency * clicker.luck)
                    {
                        damage *= 20.0 * clicker.firecrackers * clicker.efficiency * clicker.fireefficiency;
                    }
                }
                if (clicker.lingeringflame > 0.0)
                {
                    StartCoroutine(DOT(1.0f, 5, damage * clicker.lingeringflame * clicker.efficiency * clicker.fireefficiency, stageSkip, false, "generic"));//Use generic to stop an infinite loop of DOT.
                }
                //Remember to add controlled flames when armor is added.
                armor /= (clicker.controlledflame * clicker.fireefficiency * clicker.efficiency);
            }
            else if (damageType == "ally")
            {
                damage *= clicker.clerical * clicker.miracleefficiency * clicker.efficiency;
                damage *= clicker.inspire * clicker.miracleefficiency * clicker.efficiency;
                damage *= clicker.leadership * clicker.leadershipefficiency * clicker.efficiency;
                damage *= clicker.betterequipment * clicker.leadershipefficiency * clicker.efficiency;
                critChance += clicker.criticaltargeting * clicker.leadershipefficiency * clicker.efficiency;
                if (clicker.escapeplan > 1.0)
                {
                    damage *= (clicker.escapeplan * clicker.leadershipefficiency * clicker.efficiency) / (timeRemaining + 1.0f);
                }
                if (clicker.warbannerTime > 0.0)
                {
                    damage *= clicker.warbanner * clicker.miracleefficiency * clicker.efficiency;
                }
            }
            else if (damageType == "physical")
            {
                damage *= clicker.clerical * clicker.miracleefficiency * clicker.efficiency;
                damage *= clicker.dextrous * clicker.miracleefficiency * clicker.efficiency;
                damage *= clicker.warrior * clicker.strengthefficiency * clicker.efficiency;

                if (Random.value <= 0.03 * clicker.luck && clicker.forceduration > 0.0)
                {
                    isStunned = (float)clicker.forceduration;
                }
                if(isStunned > 0.0 & clicker.forcedamage > 1.0)
                {
                    damage *= clicker.forcedamage * clicker.strengthefficiency * clicker.efficiency;
                }
                //Add armor reduction here.
                if(Random.value < 0.05 * clicker.luck & clicker.heavybash > 0.0)
                {
                    stageSkip += clicker.heavybash * clicker.efficiency;
                }
                if (Random.value < clicker.twohanded * clicker.luck)
                {
                    int times = 1 + (int)System.Math.Floor(clicker.twohanded * clicker.luck * clicker.efficiency);
                    StartCoroutine(DOT(0.5f, times, damage * clicker.strengthefficiency, stageSkip, true, "generic"));
                }
                if (Random.value < clicker.flashcutschance * clicker.luck)
                {
                    int times = 1 + (int)System.Math.Floor(clicker.flashcutschance * clicker.luck * clicker.efficiency);
                    StartCoroutine(DOT(0.02f, times, damage * clicker.flashcutsdamage * clicker.strengthefficiency, stageSkip, true, "generic"));
                }
                if (clicker.bleed > 0.0)
                {
                    StartCoroutine(DOT(0.5f, 15, damage * clicker.bleed * clicker.strengthefficiency * clicker.efficiency * 0.05, stageSkip, false, "generic"));//Use generic to stop an infinite loop of DOT.
                }
            }
            if (timeRemaining < 5.0 & clicker.counter > 1.0)
            {
                damage *= clicker.counter * clicker.efficiency;
            }
            critChance *= clicker.luck * clicker.efficiency;
            if (Random.value < critChance)
            {
                damage *= 3.0;
                if (critChance > 1.0)//Add scaling after 100% crit chance.
                {
                    damage *= (1.0 + (3.0 * (critChance - 1.0)));
                }
            }
            if(armor > 1.0)
            {
                damage /= armor;
            }
            currentHealth -= damage;
            if (text)
            {
                damageBatchNumber += damage;
                textDestructionTimer = 0.3f;
                if (!isTextCurrentlyOut)
                {
                    Text tempTextBox = Instantiate(DamageText, gameObject.transform) as Text;
                    tempTextBox.transform.SetParent(renderCanvas.transform, false);
                    tempTextBox.text = texts.GiveScientificForm(damageBatchNumber, true, 2);
                    isTextCurrentlyOut = true;
                    currentDamageNumber = tempTextBox;
                }
                else
                {
                    currentDamageNumber.text = texts.GiveScientificForm(damageBatchNumber, true, 2);
                }
            }
            switch (clicker.soundPreset)
            {
                case 0:
                    {
                        FindObjectOfType<SoundManager>().Play("normalattack");
                        break;
                    }
                case 1:
                    {
                        FindObjectOfType<SoundManager>().Play("2000attack");
                        break;
                    }
                case 2:
                    {
                        FindObjectOfType<SoundManager>().Play("ironyattack");
                        break;
                    }
            }
            texts.UpdateEnemyHealth();
            if (currentHealth <= 0.0)//On Kill
            {
                float respawnTime = 1.0f / (float)(clicker.betterequipment * clicker.efficiency * clicker.leadershipefficiency);
                clicker.isAlive = false;
                gameObject.GetComponent<Image>().enabled = false;
                StartCoroutine(SendToStage(respawnTime, stageSkip));
                switch (clicker.soundPreset)
                {
                    case 0:
                        {
                            FindObjectOfType<SoundManager>().Play("normalvictimDeath");
                            break;
                        }
                    case 1:
                        {
                            FindObjectOfType<SoundManager>().Play("2000victimDeath");
                            break;
                        }
                    case 2:
                        {
                            FindObjectOfType<SoundManager>().Play("ironyvictimDeath");
                            break;
                        }
                }
            }
        }
    }
    IEnumerator DOT(float delayTime, int times, double damage, double stageSkip,bool text, string damagetype)
    {
        yield return new WaitForSeconds(delayTime);
        if (clicker.isAlive && times > 0)
        {
            DealDamage(damage, stageSkip, text, damagetype);
            times--;
            StartCoroutine(DOT(delayTime, times, damage, stageSkip, text, damagetype));
        }
    }
    IEnumerator SendToStage(float delayTime, double stages)
    {
        yield return new WaitForSeconds(delayTime);
        if (stageCap > clicker.stage)
        {
            if (!isOnPreviousStage)
            {
                clicker.stage += 1;
            }
            functions.giveMoney(clicker.moneyperkill * clicker.stage * 2.5, true);
            texts.UpdateStage();
            texts.UpdateMoney();
            skilltree.UpdatePrestigeValues();
            for (int i = 1; i < stages && ((clicker.stage) % bossStage) != 0; i++)
            {
                if (!isOnPreviousStage)
                {
                    clicker.stage += 1;
                }
                functions.giveMoney(clicker.moneyperkill * clicker.stage * 2.5, true);
                texts.UpdateStage();
                texts.UpdateMoney();
                skilltree.UpdatePrestigeValues();
            }

            if (clicker.stage > clicker.higheststage)
            {
                clicker.higheststage = clicker.stage;
            }
            if (clicker.stage > clicker.highestcurrentstage)
            {
                clicker.highestcurrentstage = clicker.stage;
            }
        }
        SetEnemyHealth();
    }
}
