using UnityEngine;

//It makes it possible to take damage
public interface IDamageable 
{
    void TakeDamage(int amount, Transform transform);
    int UpdateHealth(int currentHealth, int damageTaken);
}
