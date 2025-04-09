using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleActor
{
    string CharacterName { get; }
    int MaxHP { get; set; }
    int MaxValor { get; set; }
    int Attack { get; }
    int Defense { get; }
    RuntimeAnimatorController Animator { get; }
    bool IsDefeated { get; set; }
}
