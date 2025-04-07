using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    
    public static EnemyManager Instance { get; private set; }

    // List of the current enemies for battle (upon encounter)
    public List<CharacterInstance> party = new List<CharacterInstance>();


    
    void Awake() // Singleton declaration
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        DeleteParty();
    }

    private void Start()
    {
        // Enemy listener subscription
        //EnemyAI.OnBattleStart += AddCharacterToParty;
    }

    // Adds a characters to the party
    public void AddCharacterToParty(CharacterTemplate template, int units)
    {
        for (int i = 0; i < units; i++) {
            CharacterInstance newChar = new CharacterInstance(template);
            party.Add(newChar);
        }
        
    }

    // Clears the enemy party for the next encounter
    public void DeleteParty()
    {
        party.Clear();
    }


}
