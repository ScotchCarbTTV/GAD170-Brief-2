using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatLog : MonoBehaviour
{
    //reference to the text component on the combat log
    [SerializeField] Text combatLogTxt;

    private void Awake()
    {
        combatLogTxt.text = null;
    }

    //method for updating the text on the combat log
    public void UpdateCombatLog(string newLog)
    {
        if(combatLogTxt.text == null)
        {
            combatLogTxt.text = newLog;
        }
        else
        {
            combatLogTxt.text += "\n" + newLog;
        }

        
    }

    public void WipeLog()
    {
        combatLogTxt.text = null;
    }

}
