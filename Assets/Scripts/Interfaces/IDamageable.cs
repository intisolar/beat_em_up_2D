using UnityEngine;

/// <summary>
/// It makes it possible to take damage. Applicable to objects and characters
/// </summary>
public interface IDamageable 
{
    void TakeDamage(int amount);
    int UpdateHealth(int currentHealth, int damageTaken);
}
