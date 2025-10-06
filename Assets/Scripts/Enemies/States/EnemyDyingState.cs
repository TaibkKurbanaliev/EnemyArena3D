using System;
using UnityEngine;

public class EnemyDyingState : IState
{
    private Enemy _enemy;
    private IStateSwitcher _stateMachine;
    public EnemyDyingState(Enemy enemy, IStateSwitcher stateMachine)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _enemy.Agent.enabled = false;
        _enemy.View.PlayDying();
        _enemy.DyingEnded += Die;
    }

    public void Exit()
    {
        _enemy.DyingEnded += Die;
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

    private void Die()
    {
        _enemy.gameObject.SetActive(false);
    }
}
