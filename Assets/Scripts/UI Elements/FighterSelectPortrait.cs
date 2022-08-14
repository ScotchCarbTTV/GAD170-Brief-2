using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterSelectPortrait : MonoBehaviour
{
    //list of the prefab sprites
    [SerializeField] List<Sprite> prefabPortraits = new List<Sprite>();
    //reference to the image component on the object attached to this
    private Image portrait;

    private int fighterID;

    private void Awake()
    {
        TryGetComponent<Image>(out portrait);
    }

    public void SetFighterID(int ID)
    {
        fighterID = ID;
    }

    public int GetFighterID()
    {
        return fighterID;
    }

    //method for receiving the class ID of the character 'in' this slot and updating the UI accordingly
    public void SetPortrait(Combatant combatant)
    {
        int portraitID = ((int)combatant.charClass);
        Debug.Log(portraitID);
        portrait.sprite = prefabPortraits[portraitID];
    }

    public void SetPortraitDead(Sprite sprite)
    {
        portrait.sprite = sprite;
    }
}
