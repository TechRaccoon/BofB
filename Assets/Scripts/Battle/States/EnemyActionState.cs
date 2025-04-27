using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionState : BattleState
{
    public IBattleActor enemy;

    public EnemyActionState(EnemyInstance actor) {
        enemy = actor;
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }
}
