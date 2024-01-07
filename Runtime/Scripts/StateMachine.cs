using UnityEngine;

/// <summary>
/// override equals() when implemented
/// </summary>
public interface IState
{
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}

/// <summary>
/// a state when enter, run Burst(), then exit() immediately
/// </summary>
public interface IBurstState : IState
{
    public abstract void Burst();
}

/// <summary>
/// basic function for a state machine
/// </summary>
public abstract class StateMachine : MonoBehaviour
{
    protected IState _currentState;

    public virtual void ChangeState(IState newState)
    {
        if(_currentState == newState)
            return;

        _currentState?.Exit();
        _currentState = newState;
 
        if(_currentState is IBurstState burstState)
        {
            burstState.Burst();
            burstState.Exit();            
        }
        else
        {
            _currentState.Enter();
        }
    }

    public virtual void Update()
    {
        _currentState?.Execute();
    }
}
