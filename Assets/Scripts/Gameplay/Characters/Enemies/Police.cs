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
    [SerializeField] private float _policeMinPlayerDistance = 0.5f;

    protected override IEnumerator ExecuteAttack()
    {
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
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance >= _policeMinPlayerDistance)
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
