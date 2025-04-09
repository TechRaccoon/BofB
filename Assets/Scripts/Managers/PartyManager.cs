using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance { get; private set; }

    // All available characters in party
    public List<CharacterInstance> party = new List<CharacterInstance>();

    // Battle Party 

    //only for test purposes remove after
    [SerializeField] public CharacterTemplate brass;
    [SerializeField] public CharacterTemplate jaro;

    void Awake() // Singleton declaration
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start() {
        //methods to try with battleManager - delete later
        AddCharacterToParty(brass);
        AddCharacterToParty(jaro);
    }
    


    // Adds a character to the party
    public void AddCharacterToParty(CharacterTemplate template)
    {
        CharacterInstance newChar = new CharacterInstance(template);
        party.Add(newChar);
    }

}
