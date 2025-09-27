using UnityEngine;

namespace Handlers
{
    public class AttackPlayer : AttackHitBox
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy") && other.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage(_attackPower, transform);
            }
        }
    }
}