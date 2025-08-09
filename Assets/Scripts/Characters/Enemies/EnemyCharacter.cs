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

    [Header("Attack Control")]
    [SerializeField] private float _attackCooldown = 1f;
    private float _lastAttackTime = 0f;

    [Header("Detection")]
    [SerializeField] private float _minPlayerDistance = 0.5f;

    [Header("Visual")]
    [SerializeField] private Transform _visualRoot;
    public Transform VisualRoot
    {
        get
        {
            if (_visualRoot != null)
            {
                return _visualRoot;
            }
            return transform;
        }
    }

    [Header("Animation")]
    [SerializeField] private Animator _animator;

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
        _animator.SetTrigger("Police_Attack");

        _attackHitBox.SetActive(true);
        yield return new WaitForSeconds(_attackDuration);
        _attackHitBox.SetActive(false);

        _animator.SetTrigger("Police_Idle");
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

    public bool CanAttack()
    {
        if (Time.time >= _lastAttackTime + _attackCooldown)
        {
            _lastAttackTime = Time.time;
            return true;
        }
        return false;
    }
}
