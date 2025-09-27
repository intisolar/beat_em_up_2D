using UnityEngine;

public class ChaseState : IState
{
    private readonly EnemyCharacter _enemy;
    private readonly Transform _playerTransform;

    public ChaseState(EnemyCharacter enemy, Transform playerTransform)
    {
        _enemy = enemy;
        _playerTransform = playerTransform;
    }

    public void OnEnter() { }

    public void OnState()
    {
        if (_playerTransform == null || _enemy == null || _enemy.Rigidbody == null)
        {
            Debug.LogWarning("ChaseState: Missing references or Rigidbody is null.");
            return;
        }

        if (_enemy.MoveSpeed <= 0f)
        {
            Debug.LogWarning($"{_enemy.name} has invalid MoveSpeed.");
            return;
        }

        MoveTowardsTarget();
    }

    public void OnExit() { }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (_playerTransform.position - _enemy.Rigidbody.position).normalized;
        Vector3 movement = direction * _enemy.MoveSpeed * Time.deltaTime;

        _enemy.Rigidbody.MovePosition(_enemy.Rigidbody.position + movement);
    }
}
