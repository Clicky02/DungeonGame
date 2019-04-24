using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMenuScript : MonoBehaviour
{
    public Text end, str, intel, dex, lck, playerName, lvl, className, health, mana, attack, defense, accuracy, crit, critDMG, evade, statPoints;
    public List<Button> ups = new List<Button>();
    public List<Button> downs = new List<Button>();
    public int assignedStatPoints = 0;

    public int aEND, aSTR, aINT, aDEX, aLCK, aACC = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameData d = GameData.data;

        statPoints.text = d.unassignedStatPoints.ToString();

        end.text = d.endurance.ToString();
        str.text = d.strength.ToString();
        intel.text = d.intelligence.ToString();
        dex.text = d.dexterity.ToString();
        lck.text = d.luck.ToString();

        GameObject.Find(d.specialization + " Label").GetComponent<Text>().fontStyle = FontStyle.Italic;

        playerName.text = d.playerName;

        className.text = d.classType;
        lvl.text = "Level " + d.level.ToString();

        health.text = Mathf.FloorToInt(d.GetHealth()).ToString();
        mana.text = Mathf.FloorToInt(d.GetMana()).ToString();

        attack.text = d.GetAttack().ToString();
        defense.text = d.GetDefense().ToString();
        accuracy.text = (d.GetAccuracy()*100).ToString() + " %";
        crit.text = (d.GetCritChance()*100).ToString() + " %";
        critDMG.text = (d.GetCritMultiplier()*100).ToString() + " %";
        evade.text = (d.GetEvadeChance()*100).ToString() + " %";


        UpdateUpButtons();
        ResetDownButtons();

    }

    public void StatUp(int stat)
    {

        if (GameData.data.unassignedStatPoints - assignedStatPoints > 0)
        {
            switch (stat)
            {
                case 1:
                    aEND += 1;
                    end.text = (GameData.data.endurance + aEND).ToString();
                    end.color = Color.green;
                    assignedStatPoints += 1;
                    statPoints.text = (GameData.data.unassignedStatPoints - assignedStatPoints).ToString();
                    downs[stat-1].interactable = true;
                    UpdateUpButtons();
                    break;
                case 2:
                    aSTR += 1;
                    str.text = (GameData.data.strength + aSTR).ToString();
                    str.color = Color.green;
                    assignedStatPoints += 1;
                    statPoints.text = (GameData.data.unassignedStatPoints - assignedStatPoints).ToString();
                    downs[stat - 1].interactable = true;
                    UpdateUpButtons();
                    break;
                case 3:
                    aINT += 1;
                    intel.text = (GameData.data.intelligence + aINT).ToString();
                    intel.color = Color.green;
                    assignedStatPoints += 1;
                    statPoints.text = (GameData.data.unassignedStatPoints - assignedStatPoints).ToString();
                    downs[stat - 1].interactable = true;
                    UpdateUpButtons();
                    break;
                case 4:
                    aDEX += 1;
                    dex.text = (GameData.data.dexterity + aDEX).ToString();
                    dex.color = Color.green;
                    assignedStatPoints += 1;
                    statPoints.text = (GameData.data.unassignedStatPoints - assignedStatPoints).ToString();
                    downs[stat - 1].interactable = true;
                    UpdateUpButtons();
                    break;
                case 5:
                    aLCK += 1;
                    lck.text = (GameData.data.luck + aLCK).ToString();
                    lck.color = Color.green;
                    assignedStatPoints += 1;
                    statPoints.text = (GameData.data.unassignedStatPoints - assignedStatPoints).ToString();
                    downs[stat - 1].interactable = true;
                    UpdateUpButtons();
                    break;
            }

            GameData d = GameData.data;

            health.text = Mathf.FloorToInt(d.GetHealth(d.endurance + aEND)).ToString();
            mana.text = Mathf.FloorToInt(d.GetMana(d.intelligence + aINT)).ToString();

            attack.text = d.GetAttack(d.strength+aSTR,d.dexterity+aDEX).ToString();
            defense.text = d.GetDefense(d.strength + aSTR,d.endurance + aEND).ToString();
            accuracy.text = (d.GetAccuracy(d.dexterity + aDEX, d.intelligence + aINT) * 100).ToString() + " %";
            crit.text = (d.GetCritChance(d.dexterity + aDEX, d.strength + aSTR, d.intelligence + aINT) * 100).ToString() + " %";
            critDMG.text = (d.GetCritMultiplier(d.dexterity + aDEX, d.strength + aSTR) * 100).ToString() + " %";
            evade.text = (d.GetEvadeChance(d.dexterity + aDEX, d.endurance + aEND) * 100).ToString() + " %";
        }
    }

    public void StatDown(int stat)
    {
        if (assignedStatPoints > 0)
        {
            switch (stat)
            {
                case 1:
                    if (aEND > 0)
                    {
                        aEND -= 1;
                        end.text = (GameData.data.endurance + aEND).ToString();
                        if (aEND == 0)
                        {
                            end.color = new Color((51f / 255f), (51f / 255f), (51f / 255f));
                            downs[stat - 1].interactable = false;
                        }
                        assignedStatPoints -= 1;
                        statPoints.text = (GameData.data.unassignedStatPoints - assignedStatPoints).ToString();
                        UpdateUpButtons();
                    }
                    break;
                case 2:
                    if (aSTR > 0)
                    {
                        aSTR -= 1;
                        str.text = (GameData.data.endurance + aSTR).ToString();
                        if (aSTR == 0)
                        {
                            str.color = new Color((51f / 255f), (51f / 255f), (51f / 255f));
                            downs[stat - 1].interactable = false;
                        }
                        assignedStatPoints -= 1;
                        statPoints.text = (GameData.data.unassignedStatPoints - assignedStatPoints).ToString();
                        UpdateUpButtons();
                    }
                    break;
                case 3:
                    if (aINT > 0)
                    {
                        aINT -= 1;
                        intel.text = (GameData.data.intelligence + aINT).ToString();
                        if (aINT == 0)
                        {
                            intel.color = new Color((51f / 255f), (51f / 255f), (51f / 255f));
                            downs[stat - 1].interactable = false;
                        }
                        assignedStatPoints -= 1;
                        statPoints.text = (GameData.data.unassignedStatPoints - assignedStatPoints).ToString();
                        UpdateUpButtons();
                    }
                    break;
                case 4:
                    if (aDEX > 0)
                    {
                        aDEX -= 1;
                        dex.text = (GameData.data.dexterity + aDEX).ToString();
                        if (aDEX == 0)
                        {
                            dex.color = new Color((51f / 255f), (51f / 255f), (51f / 255f));
                            downs[stat - 1].interactable = false;
                        }
                        assignedStatPoints -= 1;
                        statPoints.text = (GameData.data.unassignedStatPoints - assignedStatPoints).ToString();
                        UpdateUpButtons();
                    }
                    break;
                case 5:
                    if (aLCK > 0)
                    {
                        aLCK -= 1;
                        lck.text = (GameData.data.luck + aLCK).ToString();
                        if (aLCK == 0)
                        {
                            lck.color = new Color((51f / 255f), (51f / 255f), (51f / 255f));
                            downs[stat - 1].interactable = false;
                        }
                        assignedStatPoints -= 1;
                        statPoints.text = (GameData.data.unassignedStatPoints - assignedStatPoints).ToString();
                        UpdateUpButtons();
                    }
                    break;
            }
            GameData d = GameData.data;

            health.text = Mathf.FloorToInt(d.GetHealth(d.endurance + aEND)).ToString();
            mana.text = Mathf.FloorToInt(d.GetMana(d.intelligence + aINT)).ToString();

            attack.text = d.GetAttack(d.strength + aSTR, d.dexterity + aDEX).ToString();
            defense.text = d.GetDefense(d.strength + aSTR, d.endurance + aEND).ToString();
            accuracy.text = (d.GetAccuracy(d.dexterity + aDEX, d.intelligence + aINT) * 100).ToString() + " %";
            crit.text = (d.GetCritChance(d.dexterity + aDEX, d.strength + aSTR, d.intelligence + aINT) * 100).ToString() + " %";
            critDMG.text = (d.GetCritMultiplier(d.dexterity + aDEX, d.strength + aSTR) * 100).ToString() + " %";
            evade.text = (d.GetEvadeChance(d.dexterity + aDEX, d.endurance + aEND) * 100).ToString() + " %";
        }
    }


    public void AddStatPoint()
    {
        GameData.data.unassignedStatPoints++;
        statPoints.text = (GameData.data.unassignedStatPoints - assignedStatPoints).ToString();
        UpdateUpButtons();
    }

    public void RemoveStatPoint()
    {
        if (GameData.data.unassignedStatPoints - assignedStatPoints > 0)
            GameData.data.unassignedStatPoints--;
        statPoints.text = (GameData.data.unassignedStatPoints - assignedStatPoints).ToString();
        UpdateUpButtons();
    }

    private void UpdateUpButtons()
    {
        foreach (Button b in ups)
        {
            if (GameData.data.unassignedStatPoints - assignedStatPoints > 0)
                b.interactable = true;
            else
                b.interactable = false;
        }
    }
    private void ResetDownButtons()
    {
        foreach (Button b in downs)
        {
            b.interactable = false;
        }
    }

    public void Apply()
    {
        GameData d = GameData.data;

        d.unassignedStatPoints -= aEND;
        d.endurance += aEND;
        aEND = 0;
        end.text = (GameData.data.endurance + aEND).ToString();
        end.color = new Color((51f / 255f), (51f / 255f), (51f / 255f));

        d.unassignedStatPoints -= aSTR;
        d.strength += aSTR;
        aSTR = 0;
        str.text = (GameData.data.strength + aSTR).ToString();
        str.color = new Color((51f / 255f), (51f / 255f), (51f / 255f));

        d.unassignedStatPoints -= aINT;
        d.intelligence += aINT;
        aINT = 0;
        intel.text = (GameData.data.intelligence + aINT).ToString();
        intel.color = new Color((51f / 255f), (51f / 255f), (51f / 255f));

        d.unassignedStatPoints -= aDEX;
        d.dexterity += aDEX;
        aDEX = 0;
        dex.text = (GameData.data.dexterity + aDEX).ToString();
        dex.color = new Color((51f / 255f), (51f / 255f), (51f / 255f));

        d.unassignedStatPoints -= aLCK;
        d.luck += aLCK;
        aLCK = 0;
        lck.text = (GameData.data.luck + aLCK).ToString();
        lck.color = new Color((51f / 255f), (51f / 255f), (51f / 255f));

        assignedStatPoints = 0;

        health.text = Mathf.FloorToInt(d.GetHealth()).ToString();
        mana.text = Mathf.FloorToInt(d.GetMana()).ToString();

        UpdateUpButtons();
        ResetDownButtons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }



}
