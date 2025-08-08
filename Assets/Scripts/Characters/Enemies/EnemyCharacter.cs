using UnityEngine;

/***
 * Represents the playable characters.
 * 
 * It manages generic behavior of enemy subclasses
 ***/
public class EnemyCharacter : CharacterBase
{
    [Header("Points")]
    [SerializeField] private int _scoreValue = 100;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Patrol(Vector3 direction, float moveSpeed, float duration)
    {
        Vector3 movement = direction.normalized * moveSpeed * Time.deltaTime;
        Rigidbody.MovePosition(Rigidbody.position + movement);
    }

    public bool DetectPlayer(EnemyAIController aiController, out Transform playerTransform)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, aiController.VisionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player") && hit.TryGetComponent<PlayerCharacter>(out var player))
            {
                playerTransform = player.transform;
                return true;
            }
        }

        playerTransform = null;
        return false;
    }

    public override void TakeDamage(byte amount, Transform attackerTransform)
    {
        base.TakeDamage(amount, attackerTransform);
    }

    protected override void Die()
    {
        UIManager.Instance.AddScore(_scoreValue);
        base.Die();
    }
}
