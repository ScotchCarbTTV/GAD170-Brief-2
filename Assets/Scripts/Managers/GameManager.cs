using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FiniteStateMachine;

public class GameManager : MonoBehaviour
{

    //header for all the lists
    [Header("Lists of Prefabs and Rosters")]

    //list of all prefab characters 'prefabCharacters'
    [SerializeField] private List<Combatant> prefabCharacters = new List<Combatant>();

    //list of character class 'playerRoster'
    [SerializeField] private List<Combatant> playerRoster = new List<Combatant>();
    //list of character class 'botRoster'
    [SerializeField] private List<Combatant> botRoster = new List<Combatant>();

    //dunno if I'll need these???
    //list of combatants from player roster who are alive
    [SerializeField] private List<Combatant> alivePlayerRoster = new List<Combatant>();

    //list of combatants from bot roster who are alive
    [SerializeField] private List<Combatant> aliveBotRoster = new List<Combatant>();


    [SerializeField] private List<FighterSelectPortrait> fighterPortraits;
    [SerializeField] private Sprite deadSprite;

    [Header("UI Elements to Activate/Deactivate")]
    [SerializeField] private GameObject chooseFighterUI;
    [SerializeField] private GameObject combatUI;

    [Header("Current Player and Bot Deployed Combatants")]
    [SerializeField] private Combatant playerCombatant;
    [SerializeField] private Combatant botCombatant;

    [Header("References to the player and bot sprite positions")]
    [SerializeField] private Transform playerFighterPos;
    [SerializeField] private Transform botFighterPos;

    [Header("Combat UI elements")]
    [SerializeField] private Text playerCombatantName;
    [SerializeField] private Text botCombatantName;

    [SerializeField] private Image playerHealthBar;
    [SerializeField] private Image botHealthBar;

    [Header("Combat Engine Variables")]
    [SerializeField] private int playerPri;
    [SerializeField] private int botPri;

    [SerializeField] private Combatant alphaCombatant;
    [SerializeField] private Combatant betaCombatant;

    [SerializeField] private TypeComparison typeComparison;

    public StateMachine StateMachine { get; private set; }

    private GameObject playerRosterParent;
    private GameObject botRosterParent;


    private void Awake()
    {
        //Debug.Log("Set up StateMachine OK");
        StateMachine = new StateMachine();

        //make sure UI elements which aren't needed are set to inactive
        chooseFighterUI.SetActive(false);
        combatUI.SetActive(false);

        playerRosterParent = GameObject.FindGameObjectWithTag("PlayerRoster");
        botRosterParent = GameObject.FindGameObjectWithTag("BotRoster");
    }

    private void Start()
    {
        //Debug.Log("Set the State OK");
        StateMachine.SetState(new SetUpTeams(this));
    }

    private void Update()
    {
        StateMachine.OnUpdate();
    }

    #region StateMachine setting methods

    public void SetSetSetupTeams()
    {
        StateMachine.SetState(new SetUpTeams(this));
    }

    public void SetChooseFighter()
    {
        StateMachine.SetState(new ChooseFighter(this));
    }

    public void SetCombatStart()
    {
        StateMachine.SetState(new CombatStart(this));
    }

    public void SetCombatPriority()
    {
        StateMachine.SetState(new CombatPriority(this));
    }

    public void SetAlphaCombat()
    {
        StateMachine.SetState(new CombatAlpha(this));
    }

    public void SetBetaCombat()
    {
        StateMachine.SetState(new CombatBeta(this));
    }

    public void SetDeclareWinner()
    {
        StateMachine.SetState(new DeclareWinner(this));
    }

    public void SetGameOver()
    {
        StateMachine.SetState(new GameOver(this));
    }

    #endregion

    public void UpdatePortraits()
    {
        for (int port = 0; port < playerRoster.Count; port++)
        {
            int charClass = (int)playerRoster[port].GetCharClass();
            if (playerRoster[port].GetHPNormalized() == 0)
            {
                fighterPortraits[port].SetPortraitDead(deadSprite);
                fighterPortraits[port].SetFighterID(port);
            }
            else
            {
                fighterPortraits[port].SetPortrait(playerRoster[port]);
                fighterPortraits[port].SetFighterID(port);
            }
            //call the 'update portrait' func on the portrait objects, passing it the class value
            
            
        }
    }

    public void ConfirmFighterSelect(FighterSelectPortrait fSelect)
    {
        if (playerRoster[fSelect.GetFighterID()].GetHPNormalized() != 0)
        {

            playerCombatant = playerRoster[fSelect.GetFighterID()];

            /* deprecated; objects are now spawned within the SetUpTeams state 
            //populate the scene with sprites & stats from the player's selected fighter and AI's selected fighter
            Instantiate(playerCombatant, playerFighterPos.position, Quaternion.identity);
            Instantiate(botCombatant, botFighterPos.position, Quaternion.identity);
            */

            //enable and reposition the player and bot fighter objects
            playerCombatant.transform.position = playerFighterPos.position;
            botCombatant.transform.position = botFighterPos.position;

            UpdateCombatUI();
            SetCombatStart();
        }
        else
        {
            //Debug.Log("That combatant is dead! Choose another!");
        }

    }

    public void AssignBotFighter()
    {
        //Debug.Log("Assigning Bot Fighter");
        
                //check if the randomly chosen fighter isn't dead, otherwise repeat
                bool findingBot = false;
        if (aliveBotRoster.Count != 0)
        {
            while (findingBot == false)
            {
                //Debug.Log("Finding Bot Fighter...");
                int ran = Random.Range(0, botRoster.Count);
                if (botRoster[ran].GetHPNormalized() != 0)
                {
                    botCombatant = botRoster[ran];
                    findingBot = true;
                }
                else
                {
                    //Debug.Log("Chose a dead fighter, trying again...");
                }


            }
        }
        else
        {
            //Debug.Log("All the bots are dead and you aren't supposed to be here.");
        }
            
        
    }

    public void UpdateCombatUI()
    {
        //update the player and bot names on the UI 
        playerCombatantName.text = playerCombatant.GetName();
        botCombatantName.text = botCombatant.GetName();

        //update UI with stats from the selected fighters
        playerHealthBar.fillAmount = playerCombatant.GetHPNormalized();
        botHealthBar.fillAmount = botCombatant.GetHPNormalized();
    }

    public void CalculatePriority()
    {

        //call the 'calculate priority' on Player's fighter and store value 
        playerPri = playerCombatant.RollPriority();
        //Debug.Log("Player Fighter PRI is " + playerPri);
        //call the 'calculate priority' on bot's figher and store value

        botPri = botCombatant.RollPriority();
        //Debug.Log("Bot Fighter PRI is " + botPri);



        //compare the two values and assign whoever has the higher value as 'Alpha'
        //if it's a true tie then just assign player's fighter as 'Alpha'
        //assign the lower value as 'Beta'
        if (playerPri >= botPri)
        {
            alphaCombatant = playerCombatant;
            betaCombatant = botCombatant;
        }
        else
        {
            alphaCombatant = botCombatant;
            betaCombatant = playerCombatant;
        }
    }

    public bool CheckWinner(float defenderHP)
    {
        //check if the combatant's HP is lower or equal to zero 
        if (defenderHP == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //setting up the state machine
    public abstract class GameManagerState : IState
    {
        public GameManager instance;

        public GameManagerState(GameManager _instance)
        {
            instance = _instance;
        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {

        }

        public virtual void OnUpdate()
        {

        }
    }

    //state for being on the main menu
    public class MainMenu : GameManagerState
    {
        public MainMenu(GameManager _instance) : base(_instance) { }
    }


    //state for setting up teams when game scene is launched
    public class SetUpTeams : GameManagerState
    {
        public SetUpTeams(GameManager _instance) : base(_instance) { }

        //OnEnter:
        public override void OnEnter()
        {
            //Debug.Log("entered SetUpTeams OK");
            //Randomly select from the prefabs to populate a list of 8 slots for player's party (playerRoster)
            for (int playerSelect = 0; playerSelect < 8; playerSelect++)
            {


                //Debug.Log("In loop #" + playerSelect + "of selecting random characters for player");
                //grab a random prefab
                int randomChoice = Random.Range(0, instance.prefabCharacters.Count);

                //instantiate the prefab 
                GameObject spawnCombatant = Instantiate(instance.prefabCharacters[randomChoice].gameObject, new Vector3(2000, 2000, 0), Quaternion.identity, instance.playerRosterParent.transform);
                Combatant tempCombatant;
                spawnCombatant.TryGetComponent<Combatant>(out tempCombatant);
                //add the prefab to the player's roster
                instance.playerRoster.Add(tempCombatant);

                instance.playerRoster[playerSelect].SetBot(false);
                //instance.playerRoster[playerSelect].SetColourID();                

                //randomly assign TYPE to each of the player's characters
                //randomly assign ATKTYPE based on the TYPE chosen                
            }

            //Randomly select from the prefabs to populate a list of 8 slots for AI's party (botRoster)
            for (int botSelect = 0; botSelect < 8; botSelect++)
            {
                //Debug.Log("In loop #" + botSelect + "of selecting random characters for player");
                //grab a random prefab
                int botChoice = Random.Range(0, instance.prefabCharacters.Count);

                GameObject spawnBot = Instantiate(instance.prefabCharacters[botChoice].gameObject, new Vector3(2100, 2100, 0), Quaternion.identity, instance.botRosterParent.transform);
                Combatant tempBotCombatant;
                spawnBot.TryGetComponent<Combatant>(out tempBotCombatant);
                //add the prefab to the player's roster
                instance.botRoster.Add(tempBotCombatant);


                instance.botRoster[botSelect].SetBot(true);
                //instance.botRoster[botSelect].SetColourID();                               
            }
            //go to the 'PlayerChooseFighter' state
            instance.SetChooseFighter();
        }

        public override void OnExit()
        {
            //take all the combatants from the player roster and bot roster and add them to the alive roster
            foreach (Combatant cbt in instance.playerRoster)
            {
                instance.alivePlayerRoster.Add(cbt);
            }
            foreach (Combatant bot in instance.botRoster)
            {
                instance.aliveBotRoster.Add(bot);
            }

        }
    }

    public class ChooseFighter : GameManagerState
    {
        public ChooseFighter(GameManager _instance) : base(_instance) { }

        //OnEnter:
        public override void OnEnter()
        {
            instance.AssignBotFighter();
            //switch on UI elements for character selection
            instance.chooseFighterUI.SetActive(true);
            instance.combatUI.SetActive(false);

            //Populate scene with prefab visual representations of the characters in the Player's roster
            instance.UpdatePortraits();
        }







        //OnUpdate:
        //update UI according to where player moves mouse/controller inputs
        //(now being handled by the Unity UI system by making the icons buttons as well)

        //OnExit:
        public override void OnExit()
        {
            //Call the 'botChooseFighter' method
            //Randomly select from the AI's roster the opponent they will be using


            //Deactivate UI elements for character selection
            //Deactivate prefabs visuals for the characters in Player's roster
            instance.chooseFighterUI.SetActive(false);
        }


    }

    //Combat states

    public class CombatStart : GameManagerState
    {
        public CombatStart(GameManager _instance) : base(_instance) { }

        //OnEnter
        //activate the UI elements for combat scene
        public override void OnEnter()
        {
            instance.combatUI.SetActive(true);

            //go to the CombatPriority state
            instance.SetCombatPriority();
        }

    }

    public class CombatPriority : GameManagerState
    {
        public CombatPriority(GameManager _instance) : base(_instance) { }

        //OnEnter
        public override void OnEnter()
        {
            //Debug.Log("New round starting! Rolling for initiative...");
            instance.CalculatePriority();

            //Debug.Log(instance.alphaCombatant + " won the roll!");
        }

        public override void OnUpdate()
        {
            if (Input.GetButtonDown("Submit"))
            {
                //go to CombatAlpha state
                instance.SetAlphaCombat();
            }
        }
    }

    public class CombatAlpha : GameManagerState
    {
        public CombatAlpha(GameManager _instance) : base(_instance) { }

        float damMulti;
        float tempDam;

        //OnEnter:
        public override void OnEnter()
        {
            //call the 'calculate damage' on Alpha and store as tempDam
            tempDam = instance.alphaCombatant.RollAtk();
            //compare Alpha's ATKTYPE to Beta's TYPE to get damMulti
            damMulti = instance.typeComparison.GetMultiplier(instance.alphaCombatant.atkID(), instance.betaCombatant.defID());

            //adjust tempDam accordingly
            tempDam = tempDam * damMulti;

            //reduce tempDam by Beta's DEF and assign as finalDam
            tempDam -= instance.betaCombatant.GetDEFAsFloat();

            //clamp it to a minimum of 1
            tempDam = Mathf.Clamp(tempDam, 1, 100);

            //subtract finalDam from Beta's HP
            instance.betaCombatant.TakeDamage(tempDam);

            //Debug.Log(instance.alphaCombatant + " did " + tempDam + " to " + instance.betaCombatant);

            instance.UpdateCombatUI();

            //check if Beta's HP is lower or equal to zero
            if (instance.CheckWinner(instance.betaCombatant.HP) == true)
            {
                //Debug.Log(instance.alphaCombatant + " won!");
                //if true then go to 'DeclareWinner' state
                instance.SetDeclareWinner();

            }
            else
            {
                //otherwise go to 'CombatBeta' state
                //Debug.Log("Moving to " + instance.betaCombatant + "'s turn");
                instance.SetBetaCombat();

            }
        }

    }

    public class CombatBeta : GameManagerState
    {
        public CombatBeta(GameManager _instance) : base(_instance) { }

        float tempDam;
        float damMulti;

        //OnEnter:
        public override void OnEnter()
        {
            //call the 'calculate damage' on Beta and store as tempDam
            tempDam = instance.betaCombatant.RollAtk();
            //compare Beta's ATKTYPE to Alpha's TYPE to get damMulti
            damMulti = instance.typeComparison.GetMultiplier(instance.betaCombatant.atkID(), instance.alphaCombatant.defID());

            //adjust tempDam accordingly
            tempDam = tempDam * damMulti;

            //reduce tempDam by Alpha's DEF and assign as finalDam
            tempDam -= instance.alphaCombatant.GetDEFAsFloat();

            //clamp it to a minimum of 1
            tempDam = Mathf.Clamp(tempDam, 1, 100);

            //subtract finalDam from Beta's HP
            instance.alphaCombatant.TakeDamage(tempDam);

            //Debug.Log(instance.betaCombatant + " did " + tempDam + " to " + instance.alphaCombatant);

            instance.UpdateCombatUI();

            //check if Beta's HP is lower or equal to zero
            if (instance.CheckWinner(instance.alphaCombatant.HP) == true)
            {
                //if true then go to 'DeclareWinner' state
                instance.SetDeclareWinner();
            }
            else
            {
                //otherwise go to 'CombatPriority' state
                instance.SetCombatPriority();

            }

        }

    }

    public class DeclareWinner : GameManagerState
    {
        public DeclareWinner(GameManager _instance) : base(_instance) { }

        private bool gameOver;

        //OnEnter
        public override void OnEnter()
        {
            gameOver = false;
            //Debug.Log("Congrats you somehow got here without a crash!");
            //iterate through each character and check if their HP is currently greater than zero
            //if its 0 or less then switch over to a 'dead' version of that character portrait & set the prefab character select UI object to its 'dead' state
            for (int alive = 0; alive < instance.playerRoster.Count; alive++)
            {
                if (instance.playerRoster[alive].GetHPNormalized() == 0)
                {
                    if (instance.alivePlayerRoster.Contains(instance.playerRoster[alive]))
                    {
                        instance.alivePlayerRoster.Remove(instance.playerRoster[alive]);
                    }
                }
            }
            for (int botlive = 0; botlive < instance.botRoster.Count; botlive++)
            {
                if (instance.botRoster[botlive].GetHPNormalized() == 0)
                {
                    if (instance.aliveBotRoster.Contains(instance.botRoster[botlive]))
                    {
                        instance.aliveBotRoster.Remove(instance.botRoster[botlive]);
                    }
                }
            }
            //Debug.Log("Bot has " + instance.aliveBotRoster.Count + " fighters left!");


            

            if (instance.aliveBotRoster.Count == 0)
            {
                //if true, check if the bot has any remaining combatants
                //if not, then declare player as winner
                //Debug.Log("The Player Wins!");
                gameOver = true;
            }
            else if (instance.alivePlayerRoster.Count == 0)
            {
                //check if the player has any remaining combatants
                //if not, then declare the bot as winner
               // Debug.Log("The Bot Wins!");
                Time.timeScale = 0;
                gameOver = true;
            }
            else
            {

                if (instance.playerCombatant.GetHPNormalized() == 0)
                {
                   // Debug.Log(instance.botCombatant.GetName() + " wins the round!");
                }
                else
                {
                   // Debug.Log(instance.playerCombatant.GetName() + " wins the round!");
                }
            }
        }

        public override void OnUpdate()
        {
            //if both have combatants left then go to the ChooseFighter state
            if (Input.GetButtonDown("Submit"))
            {
                if (gameOver == false)
                {
                    instance.playerCombatant.transform.position = new Vector3(2100, 2100, 0);
                    instance.botCombatant.transform.position = new Vector3(2100, 2100, 0);
                    instance.SetChooseFighter();
                }
                else
                {
                    instance.SetGameOver();
                }
            }
        }
    }

    public class GameOver : GameManagerState
    {
        public GameOver(GameManager _instance) : base(_instance) { }

        public override void OnEnter()
        {
            instance.combatUI.SetActive(false);
           // Debug.Log("The game has finished.");
        }
    }

    }
