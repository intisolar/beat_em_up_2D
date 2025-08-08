using UnityEngine;

namespace Handlers
{
    public class AttackHitBox : MonoBehaviour
    {
        [SerializeField] private byte _attackPower = 1;
        private GameObject _owner;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void SetAttackPower(byte attackPower)
        {
            _attackPower = attackPower;
        }

        public void SetOwner(GameObject owner)
        {
            _owner = owner;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Colisión detectada con: {other.gameObject.name}");

            if (other.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage(_attackPower, transform);
            }
        }
    }
}
