using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : IStateSwitcher
{
    private List<IState> _states = new();
    
    protected IState CurrentState { get; private set; }

    public void AddState(IState state)
    {
        _states.Add(state);
    }

    public void SwitchState<T>() where T : IState
    {
        var newState = _states.FirstOrDefault(state => state is T);

        if (newState == null)
            return;

        if (CurrentState != null) 
            CurrentState.Exit();

        CurrentState = newState;
        CurrentState.Enter();
    }

    public void HandleInput() => CurrentState.HandleInput();
    public void Update() => CurrentState.Update();
    public void FixedUpdate() => CurrentState.FixedUpdate();
}
