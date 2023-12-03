using UnityEngine;

public interface IState
{
    public abstract void Enter();
    public abstract bool Tick();
    public abstract void Execute();
    public virtual void Burst(){}
    public abstract void Exit();
}


public abstract class StateMachine : MonoBehaviour
{
    protected IState _currentState;

    public virtual void ChangeState(IState newState)
    {
        if(_currentState == newState)
            return;

        _currentState?.Exit();

        _currentState = newState;
 
        _currentState.Enter();
    }

    public virtual void Update()
    {
        _currentState?.Execute();
    }
}
