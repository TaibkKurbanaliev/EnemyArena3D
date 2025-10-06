using UnityEngine;

public class AttackState : MovementState
{
    public AttackState(IStateSwitcher stateSwitcher, Player player, StateMachineData stateMachineData) : base(stateSwitcher, player, stateMachineData)
    {
    }


    public override void Update()
    {
        base.Update();

        if (Player.Input.Player.Attack.WasPressedThisFrame())
            Player.Gun.Fire();
    }
}
