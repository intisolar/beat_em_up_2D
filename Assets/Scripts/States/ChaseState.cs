using UnityEngine;

public class ChaseState : IState
{

    private EnemyCharacter _owner;
    private Transform _playerTransform;

    public ChaseState(EnemyCharacter owner, Transform playerTransform)
    {
        _owner = owner;
        _playerTransform = playerTransform;
    }

    public void OnEnter()
    {
        Debug.Log(" In ChaseState");
    }

    public void OnExit()
    {
    }

    public void OnState()
    {

        Vector2 playerPosition = _playerTransform.position;
        Vector2 direction = (playerPosition - _owner.Rigidbody.position).normalized;
        Vector2 newPosition = _owner.Rigidbody.position + direction * _owner.MoveSpeed * Time.deltaTime;
        _owner.Rigidbody.MovePosition(newPosition);
    }

    public void OnTick()
    {
    }
}
