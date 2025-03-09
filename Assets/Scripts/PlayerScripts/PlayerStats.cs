using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update
    private int health;
    public int Health
    {
        get => health;
        set
        {
            int oldHealth = health; //old value
            health = value;         //new value
            //OnHealthChanged(oldHealth);
        }
    }

    void Start()
    {
        //anim = FindObjectOfType<PlayerAnim>();
        //state = GetComponent<PlayerState>();
        //Health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
