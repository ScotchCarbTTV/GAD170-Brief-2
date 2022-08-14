using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatLog : MonoBehaviour
{
    //reference to the text component on the combat log
    [SerializeField] Text combatLogTxt;

    
    //method for updating the text on the combat log
    public void UpdateCombatLog(string newLog)
    {
        combatLogTxt.text = newLog;
    }


}
