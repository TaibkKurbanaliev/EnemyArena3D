using UnityEngine;

public class SlowZombie : Enemy
{

    public override void InitStates()
    {
        StateMachine.AddState(new EnemyMovementState(Target, this, StateMachine));
        StateMachine.AddState(new EnemyAttackState(Target, this, StateMachine));
        StateMachine.AddState(new EnemyDyingState(this, StateMachine));
        StateMachine.AddState(new EnemyReactionState(this, StateMachine));
        StateMachine.SwitchState<EnemyMovementState>();
    }
}
