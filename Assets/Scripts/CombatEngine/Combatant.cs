using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    //publically gettable privately settable stats

    public int HP { get; private set; }
    public int ATK { get; private set; }
    public int ATKT { get; private set; }
    public int DEF { get; private set; }
    public int TYPE { get; private set; }
    public int DMG { get; private set; }
    public int PRI { get; private set; }

    //enum for determining the character's class
    public enum CharClass { Knight, Wizard, Ranger, Barbarian, Robot, Skeleton, Scientist, Rogue };

    public CharClass charClass;

    //enum for determining colour/element identity
    public enum ColourID {  Blue, Red, Green, Black, White };
    public ColourID colourID { get; private set; } 

    public enum AtkColourID { Blue, Red, Green, Black, White };
    public AtkColourID atkColourID { get; private set; }

    //string for modifying the name
    private string newName;

    private void Start()
    {
        SetStats(charClass);
        SetColourID();
    }

    //method for setting stats based on class
    public void SetStats(CharClass _class)
    {
        switch (_class)
        {
            case CharClass.Knight:
                HP = 10;
                ATK = 10;
                DEF = 15;
                PRI = 5;
                break;
            case CharClass.Wizard:
                HP = 10;
                ATK = 15;
                DEF = 5;
                PRI = 10;
                break;
            case CharClass.Ranger:
                HP = 5;
                ATK = 10;
                DEF = 10;
                PRI = 5;
                break;
            case CharClass.Barbarian:
                HP = 15;
                ATK = 5;
                DEF = 10;
                PRI = 10;
                break;
            case CharClass.Robot:
                HP = 5;
                ATK = 10;
                DEF = 15;
                PRI = 10;
                break;
            case CharClass.Skeleton:
                HP = 5;
                ATK = 10;
                DEF = 10;
                PRI = 5;
                break;
            case CharClass.Scientist:
                HP = 5;
                ATK = 10;
                DEF = 10;
                PRI = 5;
                break;
            case CharClass.Rogue:
                HP = 5;
                ATK = 10;
                DEF = 10;
                PRI = 5;
                break;
                
        }
        Debug.Log("Stats are:\n HP: " + HP + "\n ATK: " + ATK + "\n DEF " + DEF + "\n PRI " + PRI);
    }

    public void SetColourID()
    {
        int ranNum = Random.Range(0, 4);
        colourID = (ColourID)ranNum;
        int ranAtk = Random.Range(0, 1);
        switch (ranNum)
        {
            case 0:
                
                if(ranAtk == 0)
                {
                    atkColourID = 0;
                }
                else
                {
                    atkColourID = (AtkColourID)3;
                }                
                break;
            case 1:
                
                if (ranAtk == 0)
                {
                    atkColourID = 0;
                }
                else
                {
                    atkColourID = (AtkColourID)1;
                }
                break;
            case 2:
                if (ranAtk == 0)
                {
                    atkColourID = (AtkColourID)2;
                }
                else
                {
                    atkColourID = (AtkColourID)4;
                }
                break;
            case 3:
                if (ranAtk == 0)
                {
                    atkColourID = (AtkColourID)3;
                }
                else
                {
                    atkColourID = (AtkColourID)2;
                }
                break;
            case 4:
                if (ranAtk == 0)
                {
                    atkColourID = (AtkColourID)4;
                }
                else
                {
                    atkColourID = (AtkColourID)1;
                }
                break;
        }
        SetName(colourID);
        Debug.Log("TYPE is " + colourID + " and ATKT is " + atkColourID + "\nName is: " + name);

    }

    public void SetName(ColourID colourID)
    {
        newName = colourID + " " + name;
        name = newName;
    }
    
}
