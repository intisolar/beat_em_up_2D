using UnityEngine;

public class StateController
{
    private IState _currentState;
    private bool _isRotated = false;
    private float _previousX;

    public StateController(IState initialState)
    {
        _currentState = initialState;
        _currentState.OnEnter();
        _previousX = 0f;
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

    public void DetectPlayer(EnemyAIController aiController, EnemyCharacter enemy)
    {
        Collider[] hits = Physics.OverlapSphere(enemy.transform.position, aiController.VisionRadius, LayerMask.GetMask("Player"));

        if (hits.Length > 0 && hits[0].TryGetComponent<PlayerCharacter>(out var player))
        {
            Debug.Log($"Player detected by {enemy.name} at position {player.transform.position}");
            ChangeState(new ChaseState(enemy, player.transform));
        }
        else if (_currentState is not PatrolState)
        {
            ChangeState(new PatrolState(enemy, 0f, aiController.InitialDirection));
        }

        if (enemy.transform.position.x < _previousX && !_isRotated)
        {
            enemy.transform.Rotate(0, -180, 0);
            _isRotated = true;
        }
        else if (enemy.transform.position.x > _previousX && _isRotated)
        {
            enemy.transform.Rotate(0, 180, 0);
            _isRotated = false;
        }

        _previousX = enemy.transform.position.x;
    }
}
