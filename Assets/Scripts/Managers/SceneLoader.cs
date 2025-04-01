using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
    public enum Scene { Overworld1, CombatScene }

    
    public void Start() {
        //subscribe to battle scene trasition
        EnemyAI.OnBattleTransition += LoadBattle;
        Debug.Log("succesfully subscrived to battle event trigger");
    }

    public static void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }

    public static void LoadBattle()
    {
    SceneManager.LoadScene("CombatScene");
    }
}
