using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    //publically gettable privately settable stats

    public float HP;
    public int ATK { get; private set; }
    public int ATKT { get; private set; }
    public int DEF { get; private set; }
    public int TYPE { get; private set; }
    public int DMG { get; private set; }
    public int PRI { get; private set; }

    private float maxHP;

    private int totalPri;
    private int totalAtk;
    private int floatDEF;


    //enum for determining the character's class
    public enum CharClass { Knight, Wizard, Ranger, Barbarian, Robot, Skeleton, Scientist, Rogue };

    public CharClass charClass;

    //enum for determining colour/element identity
    public enum ColourID {  Blue, Red, Green, Black, White };
    public ColourID colourID;

    public enum AtkColourID { Blue, Red, Green, Black, White };
    public AtkColourID atkColourID;

    private bool isBot; 
    //reference to the sprite renderer on this game object
    private SpriteRenderer spriteRenderer;

    [SerializeField] private List<Sprite> playerSprites = new List<Sprite>(); 
    [SerializeField] private List<Sprite> botSprites = new List<Sprite>();
    private void Awake()
    {
        TryGetComponent<SpriteRenderer>(out spriteRenderer);
    }

    private void Start()
    {
        SetStats(charClass);
        SetColourID();
        SetSprite(charClass);
        SetName(charClass);
    }

    public CharClass GetCharClass()
    {
        return charClass;
    }

    public string GetName()
    {
        return name;
    }

    public float GetHPNormalized()
    {
        float hpPercent = (HP / maxHP);
        return hpPercent;
    }

    public float GetDEFAsFloat()
    {
        floatDEF = DEF;
        return floatDEF;
    }

    #region Combat Methods
    public int RollPriority()
    {
        totalPri = PRI + Random.Range(1, 6);
        return totalPri;
    }

    public int RollAtk()
    {
        totalAtk = ATK + Random.Range(1, 12);
        return totalAtk;
    }

    public int atkID()
    {
        return (int)atkColourID;
    }

    public int defID()
    {
        return (int)colourID;
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if(HP < 0)
        {
            HP = 0;
        }
    }
    #endregion

    //method for setting stats based on class
    public void SetStats(CharClass _class)
    {
        switch (_class)
        {
            case CharClass.Knight:
                HP = 100;
                ATK = 10;
                DEF = 15;
                PRI = 5;
                break;
            case CharClass.Wizard:
                HP = 100;
                ATK = 15;
                DEF = 5;
                PRI = 10;
                break;
            case CharClass.Ranger:
                HP = 50;
                ATK = 10;
                DEF = 10;
                PRI = 5;
                break;
            case CharClass.Barbarian:
                HP = 150;
                ATK = 5;
                DEF = 10;
                PRI = 10;
                break;
            case CharClass.Robot:
                HP = 50;
                ATK = 10;
                DEF = 15;
                PRI = 10;
                break;
            case CharClass.Skeleton:
                HP = 50;
                ATK = 10;
                DEF = 10;
                PRI = 5;
                break;
            case CharClass.Scientist:
                HP = 50;
                ATK = 10;
                DEF = 10;
                PRI = 5;
                break;
            case CharClass.Rogue:
                HP = 50;
                ATK = 10;
                DEF = 10;
                PRI = 5;
                break;
                
        }
        maxHP = HP;
        //Debug.Log("Stats are:\n HP: " + HP + "\n ATK: " + ATK + "\n DEF " + DEF + "\n PRI " + PRI);
    }

    public void SetName(CharClass _class)
    {
        name = colourID + " " + _class;
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

    }

    public void SetSprite(CharClass _class)
    {
        if (isBot == false) 
        {
            //Debug.Log("I am " + name + "and my class is " + _class);
            spriteRenderer.sprite = playerSprites[(int)_class];
        }
        else
        {
            //Debug.Log("I am " + name + "and my class is " + _class);
            spriteRenderer.sprite = botSprites[(int)_class];
        }
    }

    public void SetBot(bool setBot)
    {
        isBot = setBot;
    }

}
