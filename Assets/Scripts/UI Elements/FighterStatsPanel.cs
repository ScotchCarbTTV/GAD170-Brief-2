using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FighterStatsPanel : MonoBehaviour
{
    [SerializeField] private GameManager gManager;

    [SerializeField] private TMP_Text fighterName;
    [SerializeField] private TMP_Text currentHP;
    [SerializeField] private TMP_Text atkPower;
    [SerializeField] private TMP_Text defValue;
    [SerializeField] private TMP_Text priValue;
    [SerializeField] private TMP_Text atkType;

    [SerializeField] private TMP_Text nextBotName;

    public delegate void ShowStats(int fightID);
    public static ShowStats ShowFighterNameEvent;
    public static ShowStats ShowCurrentHPEvent;
    public static ShowStats ShowAtkPowerEvent;
    public static ShowStats ShowDefValueEvent;
    public static ShowStats ShowPriValueEvent;
    public static ShowStats ShowAtkTypeEvent;

    public delegate void ShowNextBotFighter(Combatant botFighter);
    public static ShowNextBotFighter ShowNextBotEvent;

    private void Awake()
    {
        ShowFighterNameEvent += ShowFighterName;
        ShowCurrentHPEvent += ShowCurrentHP;
        ShowAtkPowerEvent += ShowAtkPower;
        ShowDefValueEvent += ShowDefValue;
        ShowPriValueEvent += ShowPriValue;
        ShowAtkTypeEvent += ShowAtkType;
        ShowNextBotEvent += ShowNextBot;
    }

    private void ShowNextBot(Combatant botFighter)
    {
        nextBotName.text = "Your Opponent Is About To Send Out: " + botFighter.GetName();
    }
    
    private void ShowFighterName(int fightID)
    {
        fighterName.text = gManager.playerRoster[fightID].GetName();
    }

    private void ShowCurrentHP(int fightID)
    {
        currentHP.text = "Current HP: " + gManager.playerRoster[fightID].HP;
    }

    private void ShowAtkPower(int fightID)
    {
        atkPower.text = "ATK Power: " + gManager.playerRoster[fightID].ATK;
    }

    private void ShowDefValue(int fightID)
    {
        defValue.text = "DEF Value: " + gManager.playerRoster[fightID].DEF;
    }

    private void ShowPriValue(int fightID)
    {
        priValue.text = "PRI Value: " + gManager.playerRoster[fightID].PRI;
    }

    private void ShowAtkType(int fightID)
    {
        atkType.text = "ATK Type: " + gManager.playerRoster[fightID].atkColourID;
    }
}
