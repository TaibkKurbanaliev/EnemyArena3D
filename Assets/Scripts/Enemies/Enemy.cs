using System;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour, IDamagable
{
    public event Action AttackEnded;
    public event Action ReactionEnded;
    public event Action DyingEnded;

    public StateMachine StateMachine { get; private set; }

    [field: SerializeField] public Animator Animator {  get; private set; }

    public EnemyView View { get; private set; }
    public NavMeshAgent Agent {  get; private set; }
    public Player Target { get; private set; }

    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float AttackRate { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float AngularSpeed { get; private set; }

    private int _maxHealth;

    private void Awake()
    {
        StateMachine = new StateMachine();
        Agent = GetComponent<NavMeshAgent>();
        View = new EnemyView(Animator);
        Target = FindFirstObjectByType<Player>();
        Health.Init();
        InitStates();
    }

    private void OnEnable()
    {
        Health.Init();
        Agent.enabled = true;
        StateMachine.SwitchState<EnemyMovementState>();
    }

    public abstract void InitStates();
    public virtual void Attack()
    {
        Target.TakeDamage(Damage);
    }

    public virtual void TakeDamage(float Damage)
    {
        Health.ReduceHealth(Damage);
    }

    private void Update() => StateMachine.Update();

    public void AniamationAttackEnded() => AttackEnded?.Invoke();

    public void AnimationReactionEnded() => ReactionEnded?.Invoke();
    public void AnimationDyingEnded() => DyingEnded?.Invoke();
}
