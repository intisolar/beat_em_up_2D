using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    [SerializeField] private byte _attackPower = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage(_attackPower, transform);
        }
    }
}
