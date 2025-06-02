using UnityEngine;

//It makes it possible to take damage
public interface IDamageable 
{
    void TakeDamage(int amount);
    int UpdateHealth(int currentHealth, int damageTaken);
}
