using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;

public class GameManager : MonoBehaviour
{
    //list of all prefab characters 'prefabCharacters'

    //list of character class 'playerRoster'
    //list of character class 'botRoster'

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

    //state for setting up teams when game scene is launched
    public class SetUpTeams : GameManagerState
    {
        public SetUpTeams(GameManager _instance) : base(_instance) { }

        //OnEnter:
        //Randomly select from the prefabs to populate a list of 8 slots for player's party (playerRoster)
        //maximum of 3 of any class type
        //randomly assign TYPE to each of the player's characters
        //randomly assign ATKTYPE based on the TYPE chosen

        //Randomly select from the prefabs to populate a list of 8 slots for AI's party (botRoster)
        //same rules as player's party

        //go to the 'PlayerChooseFighter' state

        //OnUpdate:
        //check if player opens pause or other menu options

        //OnExit:
    }

    public class ChooseFighter : GameManagerState
    {
        public ChooseFighter(GameManager _instance) : base(_instance) { }

        //OnEnter:
        //Populate scene with prefab visual representations of the characters in the Player's roster

        //iterate through each character and check if their HP is currently greater than zero
        //if its 0 or less then switch over to a 'dead' version of that character portrait & set the prefab character select UI object to its 'dead' state
        //switch on UI elements for character selection
        //Call the 'botChooseFighter' method
            //Randomly select from the AI's roster the opponent they will be using

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
        //check if Beta's HP is lower or equal to zero

    }
}
