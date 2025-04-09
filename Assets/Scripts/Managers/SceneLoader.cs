using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
    //public enum Scene { Overworld1, CombatScene }

    public static SceneLoader instance;

    //Static Event to trigger fade out camera effect 
    public static event System.Action OnPlayerTransition;

    public void Start() {
        if (instance == null) {
            
            instance = this;
        }

        //subscribe to battle scene trasition
        EnemyAI.OnBattleTransition += LoadBattle;
        Debug.Log("succesfully subscrived to battle event trigger");
    }

    public static void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }

    public static void LoadBattle()
    {
        instance.StartCoroutine(instance.FadeBeforeBattle());
        
    }

    IEnumerator FadeBeforeBattle() {
        OnPlayerTransition?.Invoke();
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("CombatScene");
    }

}
