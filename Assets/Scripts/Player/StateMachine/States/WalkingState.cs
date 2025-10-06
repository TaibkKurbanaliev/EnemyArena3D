using UnityEngine;

public class WalkingState : AttackState
{
    private readonly MovementConfig _config;

    public WalkingState(IStateSwitcher stateSwitcher, Player player, StateMachineData stateMachineData) : base(stateSwitcher, player, stateMachineData)
    {
        _config = player.Config.WalkingConfig;
    }

    public override void Enter()
    {
        base.Enter();
        Data.Speed = _config.Speed;
        Data.Acceleration = _config.Acceleration;
        Data.Drag = _config.Drag;
    }

    public override void Update()
    {
        base.Update();

        if (Data.Input == Vector2.zero)
            StateSwitcher.SwitchState<IdlingState>();
        else if (Data.IsSprinting)
            StateSwitcher.SwitchState<RunningState>();
    }
}
