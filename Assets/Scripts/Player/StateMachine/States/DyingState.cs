using UnityEngine;

public class DyingState : IState
{
    Player _player;

    public DyingState(Player player)
    {
        _player = player;
    }

    public void Enter()
    {
        _player.Input.Player.Disable();
        _player.InputAxisController.enabled = false;
        
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
    }

    public void HandleInput()
    {
    }

    public void Update()
    {
    }
}
