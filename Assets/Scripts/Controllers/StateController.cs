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

    public void OnStateTick() => _current?.OnTick();
    public void OnFixedUpdateTick() => _current?.OnFixedUpdateTick();

    public void DetectPlayer(EnemyCharacter enemy)
    {
        Debug.Log(enemy.name + " Detecting player ");
        Collider2D target = Physics2D.OverlapCircle(enemy.transform.position,
                                         enemy.VisionRadius, LayerMask.GetMask("Player"));

        if (target && target.TryGetComponent<PlayerCharacter>(out var playerTarget))
        {
            Debug.Log(enemy.name + " Player detected");
            ChangeState(new ChaseState(enemy, playerTarget.transform));
        }
        else
        {
            Debug.Log(enemy.name + " Player NOT detected. ");
            if (GetCurrentState() is not PatrolState)
            {
                ChangeState(StartPatrolling(enemy, 0f));
            }
        }
    }


    public PatrolState StartPatrolling(EnemyCharacter enemy, float offset)
    {
        return new PatrolState(enemy, offset, enemy.InitialDirection);
    }
}
