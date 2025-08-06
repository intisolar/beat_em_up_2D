using UnityEngine;

/***
 * Represents the playable characters.
 * 
 * It manages generic behavior of enemy subclasses
 ***/
public abstract class EnemyCharacter : CharacterBase
{
    [SerializeField] private int scoreValue = 100;
    public Rigidbody Rigidbody { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Rigidbody = GetComponent<Rigidbody>();
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
}
