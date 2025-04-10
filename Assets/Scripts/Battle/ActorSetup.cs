using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSetUp : MonoBehaviour
{
    // Reference to the data (assigned at spawn)
    public IBattleActor Data { get; private set; }

    // Link this prefab to an existing IBattleActor (from PartyManager)
    public void Initialize(IBattleActor data)
    {
        this.Data = data;
        UpdateVisuals(); // Refresh UI/animations
    }

    void UpdateVisuals()
    {
        // Set animator controller
        GetComponent<Animator>().runtimeAnimatorController = Data.Animator;
        GetComponent<Animator>().Play("IDLE_SE");
    }

}
