using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartyManager : MonoBehaviour
{
    
    public static EnemyPartyManager Instance { get; private set; }

    // List of the current enemies for battle (upon encounter)
    public List<EnemyInstance> party = new List<EnemyInstance>();

    
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

    private void Start()
    {
        // Enemy listener subscription
        EnemyAI.OnBattleStart += AddCharacterToParty;
    }

    // Adds a characters to the party
    // needs to be improved in the futute to include diferent types of enemies
    public void AddCharacterToParty(EnemyInstance template, int units)
    {
        for (int i = 0; i < units; i++) {
            EnemyInstance newChar = template;
            party.Add(newChar);
        }
    }

    // Clears the enemy party for the next encounter
    // Called by an event by the battle manager when the battle finishes
    public void DeleteParty()
    {
        party.Clear();
    }




}
