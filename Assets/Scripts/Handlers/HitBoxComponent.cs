using UnityEngine;

namespace Handlers
{
    public class HitBoxComponent : MonoBehaviour
    {
        private byte _attackPower;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void SetAttackPower(byte attackPower)
        {
            _attackPower = attackPower;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage(_attackPower, transform);
            }
        }
    }
}
