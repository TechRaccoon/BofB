using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character")]
public class CharacterTemplate : ScriptableObject
{
    //maybe make an enum for the class type?

    public string characterName;

    public Sprite portrait;

    public int level;

    public int baseHP;
    public int baseValor;
    public int baseAttack;
    public int baseDefense;

    public RuntimeAnimatorController animatorController;
}
