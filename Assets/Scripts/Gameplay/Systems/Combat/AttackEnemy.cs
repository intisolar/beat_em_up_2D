using UnityEngine;

namespace Handlers
{
    public class AttackEnemy : AttackHitBox
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && other.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage(_attackPower, transform);
            }
        }
    }
}