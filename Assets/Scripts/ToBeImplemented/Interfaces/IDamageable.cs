using UnityEngine;

public interface IDamageable 
{
    void TakeDamage(byte damageAmount, Transform attackerTransform);
    int UpdateHealth(int currentHealth, int damageTaken);
}
