using UnityEngine;

public class FastZombie : Enemy
{

    public override void InitStates()
    {
        StateMachine.AddState(new EnemyMovementState(Target, this, StateMachine));
        StateMachine.AddState(new EnemyAttackState(Target, this, StateMachine));
        StateMachine.AddState(new EnemyDyingState(this, StateMachine));
        StateMachine.SwitchState<EnemyMovementState>();
    }
}
