using UnityEngine;

public class StateController
{
    private IState _current;

    public StateController(IState initialState)
    {
        _current = initialState;
    }

    public IState GetCurrentState() { return _current; }

    /// <summary>It switches to the new state and executes OnExit / OnEnter accordingly.</summary>
    public void ChangeState(IState next) 
    {
        if (_current != next)
        {
            if (next != null)
            {
                _current?.OnExit();
                _current = next;
                _current.OnEnter();
            } else
            {
                throw new System.Exception("StateController.ChangeState() state cannot be null. Keep on current state: " 
                    + _current );
            }
        }
    }

    /// <summary>It calls OnState() of the active state; must be invoked once per frame.</summary>
    public void UpdateState() => _current?.OnState();
}
