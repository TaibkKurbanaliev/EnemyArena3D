using System;
using UnityEngine;

public class EnemyReactionState : IState
{
    private Enemy _enemy;
    private IStateSwitcher _stateMachine;

    public EnemyReactionState(Enemy enemy, IStateSwitcher stateMachine)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _enemy.View.PlayReaction();
        _enemy.ReactionEnded += OnReactionEnd;
        _enemy.Health.Died += OnEnemyDied;
        _enemy.Health.HealthChanged += OnHealthChanged;
    }

    public void Exit()
    {
        _enemy.ReactionEnded -= OnReactionEnd;
        _enemy.Health.Died -= OnEnemyDied;
        _enemy.Health.HealthChanged -= OnHealthChanged;
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

    private void OnEnemyDied()
    {
        _stateMachine.SwitchState<EnemyDyingState>();
    }

    private void OnReactionEnd()
    {
        _stateMachine.SwitchState<EnemyMovementState>();
    }

    private void OnHealthChanged(float obj)
    {
        _enemy.View.PlayReaction();
    }
}
