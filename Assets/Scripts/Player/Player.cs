using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;


public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private PlayerView _playerView;

    [field: SerializeField] public PlayerConfig Config { get; private set; }
    [field: SerializeField] public Gun Gun { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public CinemachineInputAxisController InputAxisController { get; private set; }

    public StateMachine StateMachine { get; private set; }
    public PlayerInput Input { get; private set; }
    public CharacterController Controller { get; private set; }

    [Inject]
    private void Constract(PlayerInput playerInput)
    {
        Input = playerInput;
        Controller = GetComponent<CharacterController>();
        Health.Init();
        StateMachine = new StateMachine();
        StateMachineData data = new StateMachineData();
        StateMachine.AddState(new IdlingState(StateMachine, this, data));
        StateMachine.AddState(new WalkingState(StateMachine, this, data));
        StateMachine.AddState(new RunningState(StateMachine, this, data));
        StateMachine.AddState(new DyingState(this));
        StateMachine.SwitchState<IdlingState>();
    }

    private void Update()
    {
        StateMachine.HandleInput();

        StateMachine.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }

    public void TakeDamage(float Damage)
    {
        Health.ReduceHealth(Damage);
    }
}
