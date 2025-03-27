using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script in charge of moving the player to a new area/platform once it has
// reached the area limit by triggering the box collider
public class Jumper : MonoBehaviour
{
    // Conection to the adjacent area 
    [SerializeField] public Vector3 spawnPoint;

    //Static Event to trigger fade out camera effect 
    public static event System.Action OnPlayerTransition;

    private void OnTriggerEnter(Collider other)
    {
        // Confirm the object is the Player 
        if(other.CompareTag("Player"))
        {
            StartCoroutine(FadeFirst(other));
        }
    }

    // Coroutine to time the fade in/out of the transition 
    IEnumerator FadeFirst(Collider other) {

        // trigger the fade out Camera
        OnPlayerTransition?.Invoke();
        yield return new WaitForSeconds(0.3f);

        // Disable player movement 
        other.GetComponent<PlayerMovement>().enabled = false;

        //Change Player Position to new area
        other.transform.position = spawnPoint;
        yield return new WaitForSeconds(0.3f);

        // Disable player movement 
        other.GetComponent<PlayerMovement>().enabled = true;

        // Trigger fade in once in the new area
        OnPlayerTransition?.Invoke();
    }

}
