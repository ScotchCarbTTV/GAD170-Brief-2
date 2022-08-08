using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script for comparing the types of two combatants in the combat system when a combatant attacks and determining if they should double, halve or do nothing to their combat power
 * 
 * 
 * 
 */


public class TypeComparison : MonoBehaviour
{
    private int atkType;
    private int defType;

    private float multiplier;

   //method for determining the final multiplier of combat damage
   public float GetMultiplier(int atk, int def)
    {
        
        if(atk == 0) //blue
        {
            if (def == 1 || def == 4)//red or white
            {
                multiplier = 2;
            }
            else if(def == 3) //black
            {
                multiplier = 0.5f;
            }
            else
            {
                multiplier = 1;
            }
        }
        else if(atk == 1) //red
        {
            if(def == 2 || def == 4) //green or red
            {
                multiplier = 2;
            }
            else if(def == 3) //black
            {
                multiplier = 0.5f;
            }
            else
            {
                multiplier = 1;
            }
        }
        else if(atk == 2) //green
        {
            if(def == 0 || def == 3) //blue or black
            {
                multiplier = 2;
            }
            else if(def == 1) //red
            {
                multiplier = 0.5f;
            }
            else
            {
                multiplier = 1;
            }
        }
        else if(atk == 3) // black
        {
            if(def == 0 || def == 1) //blue or red
            {
                multiplier = 2;
            }
            else if (def == 4) //white
            {
                multiplier = 0.5f;
            }
            else
            {
                multiplier = 1;
            }
        }
        else if(atk == 4) //white
        {
            if(def == 2 || def == 3) //green or black
            {
                multiplier = 2;
            }
            else if (def == 0) //blue
            {
                multiplier = 0.5f;
            }
            else
            {
                multiplier = 1;
            }
        }
        return multiplier;
    }

}
