using UnityEngine;

public class EnemyMovementState : IState
{
    private Player _target;
    private Enemy _enemy;
    private IStateSwitcher _stateMachine;

    public EnemyMovementState(Player target, Enemy enemy, IStateSwitcher stateMachine)
    {
        _target = target;
        _enemy = enemy;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _enemy.Agent.speed = _enemy.MoveSpeed;
        _enemy.Agent.angularSpeed = _enemy.AngularSpeed;
        _enemy.Agent.destination = _target.transform.position;
        _enemy.Agent.stoppingDistance = _enemy.AttackRange;
        _enemy.View.PlayMovement(_enemy.MoveSpeed);

        _enemy.Health.HealthChanged += OnHealthChanged;
    }

    public void Exit()
    {
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
        if (_enemy.Agent.remainingDistance <= _enemy.AttackRange)
        {
            _stateMachine.SwitchState<EnemyAttackState>();
            return;
        }

        _enemy.Agent.destination = _target.transform.position;
    }

    private void OnHealthChanged(float value)
    {
        if (value <= 0)
            _stateMachine.SwitchState<EnemyDyingState>();
        else
            _stateMachine.SwitchState<EnemyReactionState>();
    }
}
