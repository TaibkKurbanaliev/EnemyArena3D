using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EnemyAttackState : IState
{
    private Player _target;
    private Enemy _enemy;
    private IStateSwitcher _stateMachine;

    private Coroutine _attackRoutine;

    public EnemyAttackState(Player target, Enemy enemy, IStateSwitcher stateMachine)
    {
        _target = target;
        _enemy = enemy;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _enemy.View.PlayAttack();
        _enemy.AttackEnded += OnAttack;
        _enemy.Health.HealthChanged += OnHealthChanged;
    }

    public void Exit()
    {
        _enemy.Health.HealthChanged -= OnHealthChanged;
        _enemy.AttackEnded -= OnAttack;
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

    public void OnAttack()
    {
        if (Vector3.Distance(_enemy.transform.position, _target.transform.position) > _enemy.AttackRange + _enemy.Agent.radius)
        {
            _stateMachine.SwitchState<EnemyMovementState>();
        }
        else
        {
            _enemy.Attack();
        }
            
    }

    private void OnHealthChanged(float value)
    {
        if (value <= 0)
            _stateMachine.SwitchState<EnemyDyingState>();
        else
            _stateMachine.SwitchState<EnemyReactionState>();
    }
}
