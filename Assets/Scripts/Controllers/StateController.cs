using UnityEngine;

public class StateController
{
    private IState _currentState;

    public StateController(IState initialState)
    {
        _currentState = initialState;
        _currentState.OnEnter();
    }

    public void ChangeState(IState newState)
    {
        if (_currentState == newState || newState == null)
        {
            return;
        }
            
        _currentState.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }

    public void UpdateState()
    {
        if (_currentState != null)
        {
            _currentState.OnState();
        }
    }

    public void DetectPlayer(EnemyCharacter enemy)
    {
        Collider[] hits = Physics.OverlapSphere(enemy.transform.position, enemy.VisionRadius, LayerMask.GetMask("Player"));

        if (hits.Length > 0 && hits[0].TryGetComponent<PlayerCharacter>(out var player))
        {
            ChangeState(new ChaseState(enemy, player.transform));
        }
        else if (_currentState is not PatrolState)
        {
            ChangeState(new PatrolState(enemy, 0f, enemy.InitialDirection));
        }
    }
}
