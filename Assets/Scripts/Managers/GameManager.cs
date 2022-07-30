using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FiniteStateMachine;

public class GameManager : MonoBehaviour
{
    //list of all prefab characters 'prefabCharacters'
    [SerializeField] private List<Combatant> prefabCharacters = new List<Combatant>();

    //list of character class 'playerRoster'
    [SerializeField] private List<Combatant> playerRoster = new List<Combatant>();
    //list of character class 'botRoster'
    [SerializeField] private List<Combatant> botRoster = new List<Combatant>();

    [SerializeField] private List<FighterSelectPortrait> fighterPortraits;

    [SerializeField] private GameObject chooseFighterUI;

    public StateMachine StateMachine { get; private set; }

    

    private void Awake()
    {
        //Debug.Log("Set up StateMachine OK");
        StateMachine = new StateMachine();

        //make sure UI elements which aren't needed are set to inactive
        chooseFighterUI.SetActive(false);
    }

    private void Start()
    {
        //Debug.Log("Set the State OK");
        StateMachine.SetState(new SetUpTeams(this));
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

    #endregion

    public void UpdatePortraits()
    {
        for(int port = 0; port < playerRoster.Count; port++)
        {
            //call the 'update portrait' func on the portrait objects, passing it the class value
            fighterPortraits[port].SetPortrait(playerRoster[port]);
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
            for(int playerSelect = 0; playerSelect < 8; playerSelect++)
            {
                //Debug.Log("In loop #" + playerSelect + "of selecting random characters for player");
                //grab a random prefab
                int randomChoice = Random.Range(0, instance.prefabCharacters.Count);
                
                    //add the prefab to the player's roster
                    instance.playerRoster.Add(instance.prefabCharacters[randomChoice]);
                //randomly assign TYPE to each of the player's characters
                //randomly assign ATKTYPE based on the TYPE chosen                
            }

            //Randomly select from the prefabs to populate a list of 8 slots for AI's party (botRoster)
            for (int botSelect = 0; botSelect < 8; botSelect++)
            {
                //Debug.Log("In loop #" + botSelect + "of selecting random characters for player");
                //grab a random prefab
                int botChoice = Random.Range(0, instance.prefabCharacters.Count);

                //add the prefab to the player's roster
                instance.botRoster.Add(instance.prefabCharacters[botChoice]);
                //randomly assign TYPE to each of the player's characters
                //randomly assign ATKTYPE based on the TYPE chosen                
            }
            //go to the 'PlayerChooseFighter' state
            instance.SetChooseFighter();
        }

        public override void OnExit()
        {
            
                      
        }
    }

    public class ChooseFighter : GameManagerState
    {
        public ChooseFighter(GameManager _instance) : base(_instance) { }

        //OnEnter:
        public override void OnEnter()
        {
            //switch on UI elements for character selection
            instance.chooseFighterUI.SetActive(true);

            //Populate scene with prefab visual representations of the characters in the Player's roster
            instance.UpdatePortraits();

            //iterate through each character and check if their HP is currently greater than zero
            //if its 0 or less then switch over to a 'dead' version of that character portrait & set the prefab character select UI object to its 'dead' state

            //Call the 'botChooseFighter' method
            //Randomly select from the AI's roster the opponent they will be using
        }







        //OnUpdate:
        //update UI according to where player moves mouse/controller inputs
        //if player uses 'select' input when a valid option is highlighted, add the relevant object to the 'selected' preview slot
        //if that slot is already populated then remove the object currently occupying it and reactivate it in the relevant available character slot
        //if player highlights the 'selected' character and hits 'cancel' input, remove that character and reactivate them in the available character slot
        //if the player highlights 'confirm' and hits the 'select' input then go to the 'Combat' state

        //OnExit:
        //Deactivate UI elements for character selection
        //Deactivate prefabs visuals for the characters in Player's roster

    }

    //Combat states

    public class CombatStart : GameManagerState
    {
        public CombatStart(GameManager _instance) : base(_instance) { }

        //OnEnter
        //activate the UI elements for combat scene
        //populate the scene with sprites & stats from the player's selected fighter and AI's selected fighter
        //update UI with stats from the selected fighters
        //go to the CombatPriority state
        
    }

    public class CombatPriority : GameManagerState
    {
        public CombatPriority(GameManager _instance) : base(_instance) { }

        //OnEnter
        //call the 'calculate priority' on Player's fighter and store value 
        //call the 'calculate priority' on bot's figher and store value
        //compare the two values and assign whoever has the higher value as 'Alpha' 
            //if it's a true tie then just assign player's fighter as 'Alpha'
            //assign the lower value as 'Beta'

        //go to CombatAlpha state
    }

    public class CombatAlpha : GameManagerState
    {
        public CombatAlpha(GameManager _instance) : base(_instance) { }

        //OnEnter:
        //call the 'calculate damage' on Alpha and store as tempDam
        //compare Alpha's ATKTYPE to Beta's TYPE
            //adjust tempDam accordingly and assign as tempDam2
        //reduce tempDam by Beta's DEF and assign as finalDam
            //clamp it to a minimum of 1
        //subtract finalDam from Beta's HP
        //check if Beta's HP is lower or equal to zero (have a bool method 'check4Winner' which checks the HP of Alpha & Beta and returns one or the other as 'winner' if they're at zero, otherwise returns false)
            //if true then go to 'DeclareWinner' state
            //otherwise go to 'CombatBeta' state

    }

    public class CombatBeta : GameManagerState
    {
        public CombatBeta(GameManager _instance) : base(_instance) { }

        //OnEnter:
        //call the 'calculate damage' on Beta and store as tempDam
        //compare Beta's ATKTYPE to Alpha's TYPE
        //adjust tempDam accordingly and assign as tempDam2
        //reduce tempDam by Alpha's DEF and assign as finalDam
        //clamp it to a minimum of 1
        //subtract finalDam from Beta's HP
        //check if Alpha's HP is lower or equal to zero (have a bool method 'check4Winner' which checks the HP of Alpha & Beta and returns one or the other as 'winner' if they're at zero, otherwise returns false)
        //if true then go to 'DeclareWinner' state
        //otherwise go to 'CombatPriority' state
    }

    public class DeclareWinner : GameManagerState
    {
        public DeclareWinner(GameManager _instance): base(_instance) { }

        //OnEnter
        //check if the player has any remaining combatants
            //if not, then declare the bot as winner
            //if true, check if the bot has any remaining combatants
                //if not, then declare player as winner
        //if both have combatants left then go to the ChooseFighter state
    }
 

}
