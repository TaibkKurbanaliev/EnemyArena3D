using System;
using UnityEngine;

public abstract class MovementState : IState
{
    protected readonly IStateSwitcher StateSwitcher;
    protected readonly StateMachineData Data;
    protected readonly Player Player;

    private Camera _camera;

    public MovementState(IStateSwitcher stateSwitcher, Player player, StateMachineData stateMachineData)
    {
        StateSwitcher = stateSwitcher;
        Player = player;
        Data = stateMachineData;
        Data.CameraRotationSpeed = 10f;
        _camera = Camera.main;
    }

    public virtual void Enter()
    {
        Player.Health.Died += OnPlayerDied;
    }

    public virtual void Exit()
    {
        Player.Health.Died -= OnPlayerDied;
    }

    public void FixedUpdate()
    {
        Rotate();
        Move();
    }

    public void HandleInput()
    {
        Data.Input = Player.Input.Player.Move.ReadValue<Vector2>();
        Data.Velocity = Data.Input * Data.Speed;
        Data.IsSprinting = Player.Input.Player.Sprint.IsPressed();
    }

    public virtual void Update()
    {
    }

    private void Move()
    {
        var inputDir = new Vector3(Data.Input.x, 0f, Data.Input.y);

        var playerForwardXZ = new Vector3(Player.transform.forward.x, 0f, Player.transform.forward.z).normalized;
        var playerRightXZ = new Vector3(Player.transform.right.x, 0f, Player.transform.right.z).normalized;

        var moveDir = playerForwardXZ * Data.Input.y + playerRightXZ * Data.Input.x;
        var newVelocity = moveDir * Data.Acceleration * Time.fixedDeltaTime;
        newVelocity.y = Physics.gravity.y;

        Player.Controller.Move(newVelocity);
    }

    private void Rotate()
    {
        var targetRotation = Quaternion.Euler(0f, _camera.transform.eulerAngles.y, 0f);
        Player.transform.rotation = targetRotation;
    }

    private void OnPlayerDied()
    {
        StateSwitcher.SwitchState<DyingState>();
    }
}
