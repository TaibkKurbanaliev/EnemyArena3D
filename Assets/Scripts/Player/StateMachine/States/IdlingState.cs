using UnityEngine;

public class IdlingState : AttackState
{
    public IdlingState(IStateSwitcher stateSwitcher, Player player, StateMachineData stateMachineData) : base(stateSwitcher, player, stateMachineData)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Data.Speed = 0;
    }

    public override void Update()
    {
        base.Update();

        if (Data.Input != Vector2.zero && Data.IsSprinting)
            StateSwitcher.SwitchState<RunningState>();
        else if (Data.Input != Vector2.zero && !Data.IsSprinting)
            StateSwitcher.SwitchState<WalkingState>();
    }
}
