using UnityEngine;

public class PatrolState : IState
{
    private enum SubState { WaitingStart, Moving, Idle }

    private readonly EnemyCharacter _enemyCharacter;
    private Vector3 _movementDirection;
    private float _stateTimer;
    private SubState _currentSubState;

    private readonly float _initialDelayDuration;
    private readonly float _idleWaitDuration = 3f;
    private readonly float _movementDuration = 3f;

    public PatrolState(EnemyCharacter enemyCharacter, float initialDelayDuration, Vector3 initialMovementDirection)
    {
        _enemyCharacter = enemyCharacter;
        _initialDelayDuration = initialDelayDuration;
        _movementDirection = initialMovementDirection;

        if (_movementDirection == Vector3.zero)
        {
            _movementDirection = Vector3.left;
        }
    }

    public void OnEnter()
    {
        _stateTimer = 0f;

        if (_initialDelayDuration > 0f)
        {
            _currentSubState = SubState.WaitingStart;
        }
        else
        {
            _currentSubState = SubState.Moving;
        }
    }

    public void OnState()
    {
        _stateTimer += Time.deltaTime;

        if (_currentSubState == SubState.WaitingStart && _stateTimer >= _initialDelayDuration)
        {
            StartMoving();
        }
        else if (_currentSubState == SubState.Moving)
        {
            Move();
            if (_stateTimer >= _movementDuration)
            {
                StartIdling();
            }
        }
        else if (_currentSubState == SubState.Idle && _stateTimer >= _idleWaitDuration)
        {
            ChangeDirectionAndMove();
        }
    }

    public void OnExit() { }

    private void StartMoving()
    {
        _stateTimer = 0f;
        _currentSubState = SubState.Moving;
    }

    private void StartIdling()
    {
        _stateTimer = 0f;
        _currentSubState = SubState.Idle;
    }

    private void ChangeDirectionAndMove()
    {
        _movementDirection = -_movementDirection;
        _stateTimer = 0f;
        _currentSubState = SubState.Moving;
    }

    private void Move()
    {
        Vector3 movement = _movementDirection.normalized * _enemyCharacter.MoveSpeed * Time.deltaTime;
        _enemyCharacter.Rigidbody.MovePosition(_enemyCharacter.Rigidbody.position + movement);
    }
}
