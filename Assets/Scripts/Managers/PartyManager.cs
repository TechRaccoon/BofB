using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance { get; private set; }

    public List<CharacterInstance> party = new List<CharacterInstance>();

    //only for test purposes
    [SerializeField] public CharacterTemplate brass;
    [SerializeField] public CharacterTemplate jaro;

    void Awake() // Singleton declaration
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Makes it persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        //methods to try with battleManager
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
