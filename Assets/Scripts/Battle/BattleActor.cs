using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActor : MonoBehaviour
{
    //Unit name
    public string UnitName;

    //Health
    public int maxHealth;
    public int currentHealth;

    //Mana
    public int maxVP;
    public int currentVP;

    // Character Move list 
    public List<MoveBase> AvailableMoves;

}
