using System.Collections;
using UnityEngine;

/***
 * Base para todos los enemigos.
 * Contiene comportamiento genérico.
 ***/
public abstract class EnemyCharacter : CharacterBase
{
    [Header("Points")]
    [SerializeField] private int _scoreValue = 100;

    [Header("Attack")]
    [SerializeField] protected GameObject _attackHitBox;
    [SerializeField] protected float _attackDuration = 0.5f;
    [SerializeField] protected float _attackCooldown = 1f;
    private float _lastAttackTime = 0f;

    [Header("Visual")]
    [SerializeField] private Transform _visualRoot;
    public Transform VisualRoot => _visualRoot != null ? _visualRoot : transform;
    [SerializeField] protected Animator _animator;

    protected override void Awake()
    {
        base.Awake();
    }

    public virtual void Patrol(Vector3 direction, float moveSpeed, float duration)
    {
        Vector3 movement = direction.normalized * moveSpeed * Time.deltaTime;
        Rigidbody.MovePosition(Rigidbody.position + movement);
    }

    public virtual void Attack()
    {
        StartCoroutine(ExecuteAttack());
    }

    protected virtual IEnumerator ExecuteAttack()
    {
        if (_attackHitBox != null)
            _attackHitBox.SetActive(true);

        yield return new WaitForSeconds(_attackDuration);

        if (_attackHitBox != null)
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

    public bool CanAttack()
    {
        if (Time.time >= _lastAttackTime + _attackCooldown)
        {
            _lastAttackTime = Time.time;
            return true;
        }
        return false;
    }

    public virtual bool DetectPlayer(EnemyAIController aiController, out Transform playerTransform)
    {
        playerTransform = null;
        return false;
    }
}
