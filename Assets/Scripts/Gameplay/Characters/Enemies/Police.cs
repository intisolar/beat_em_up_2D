using System.Collections;
using UnityEngine;

/***
 * Enemigo específico: Police
 * Hereda de EnemyCharacter
 * Contiene animaciones y efectos de sonido específicos.
 ***/
public class PoliceEnemy : EnemyCharacter
{
    [SerializeField] private new PlayerAnimationSFXController _sFXController;

    [Header("Detection")]
    [SerializeField] private float _minPlayerDistance = 0.5f;
    [SerializeField] private float _distanceUpdateDelay = 0.75f;
    private float _lastDistanceUpdateTime = -Mathf.Infinity;
    private float _cachedPlayerDistance = 0f;
    private Transform _cachedDistanceTarget = null;

    [Header("Attack")]
    [SerializeField] private float _attackDelay = 1.0f;
    private float _lastAttackTime = -Mathf.Infinity;

    protected override IEnumerator ExecuteAttack()
    {
        if (Time.time - _lastAttackTime < _attackDelay)
            yield break;

        _lastAttackTime = Time.time;

        if (_animator != null)
            _animator.SetTrigger("Police_Attack");

        if (_sFXController != null)
            _sFXController.PlayAttack();

        if (_attackHitBox != null)
            _attackHitBox.SetActive(true);

        yield return new WaitForSeconds(_attackDuration);

        if (_attackHitBox != null)
            _attackHitBox.SetActive(false);

        if (_animator != null)
            _animator.SetTrigger("Police_Idle");
    }

    public override void TakeDamage(byte amount, Transform attackerTransform)
    {
        PlayerAnimationSFXController attackerSFX = attackerTransform.GetComponentInParent<PlayerAnimationSFXController>();
        if (attackerSFX != null)
            attackerSFX.PlayHit();

        base.TakeDamage(amount, attackerTransform);

        if (_sFXController != null)
            _sFXController.PlayHurt();
    }

    protected override void Die()
    {
        if (_sFXController != null)
            _sFXController.PlayDeath();

        base.Die();
    }

    public override bool DetectPlayer(EnemyAIController aiController, out Transform playerTransform)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, aiController.VisionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player") && hit.TryGetComponent<PlayerCharacter>(out var player))
            {
                // Actualizamos la distancia solo si pasó el delay o cambió el objetivo
                if (Time.time - _lastDistanceUpdateTime > _distanceUpdateDelay || _cachedDistanceTarget != player.transform)
                {
                    _cachedPlayerDistance = Vector3.Distance(transform.position, player.transform.position);
                    _lastDistanceUpdateTime = Time.time;
                    _cachedDistanceTarget = player.transform;
                }

                float distance = _cachedPlayerDistance;
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
}
