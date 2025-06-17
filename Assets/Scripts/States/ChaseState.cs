using UnityEngine;

public class ChaseState : IState
{

    private EnemyCharacter enemy;
    private Transform _playerTransform;

    public ChaseState(EnemyCharacter owner, Transform playerTransform)
    {
        enemy = owner;
        _playerTransform = playerTransform;
    }

    public void OnEnter()
    {
        Debug.Log(enemy.name + " In ChaseState");
    }

    public void OnExit()
    {
    }

    public void OnState()
    {
    }

    public void OnTick()
    {
    }

    public void OnFixedUpdateTick()
    {
        Vector2 playerPosition = _playerTransform.position;
        Vector2 direction = (playerPosition - enemy.Rigidbody.position).normalized;

        if (enemy.MoveSpeed == -1f)
        {
            throw new System.Exception(" Careful, _moveSpeed is null in" + enemy.name);
        }
        Vector2 newPosition = enemy.Rigidbody.position + direction * enemy.MoveSpeed * Time.fixedDeltaTime;
        enemy.Rigidbody.MovePosition(newPosition);
    }

}
