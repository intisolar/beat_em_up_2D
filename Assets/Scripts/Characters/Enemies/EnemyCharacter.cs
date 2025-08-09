using System.Collections;
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

    [Header("Attack")]
    [SerializeField] private GameObject _attackHitBox;
    [SerializeField] private float _attackDuration = 0.5f;

    [Header("Detection")]
    [SerializeField] private float _minPlayerDistance = 0.5f;

    [Header("Visual")]
    [SerializeField] private Transform _visualRoot; // opcional: asigna el objeto visual (sprite/modelo)
    public Transform VisualRoot => _visualRoot != null ? _visualRoot : transform;

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
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance >= _minPlayerDistance)
                {
                    playerTransform = player.transform;
                    return true;
                }
            }
        }

        playerTransform = null;
        return false;
    }

    public void Attack()
    {
        StartCoroutine(ExecuteAttack());
    }

    public IEnumerator ExecuteAttack()
    {
        _attackHitBox.SetActive(true);
        yield return new WaitForSeconds(_attackDuration);
        _attackHitBox.SetActive(false);
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
