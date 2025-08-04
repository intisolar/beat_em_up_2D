using UnityEngine;

/*
 *  Description: An abstract class that defines the base structure for both player and enemy characters in the game.
 *  
 *  Responsibilities:
 *  Defines common attributes like health, energy, and other stats.
 *  Abstract methods for movement and interactions.
 */
public abstract class CharacterBase : MonoBehaviour, IDamageable
{
    #region Variables
    [Header("Health Stats")]
    [SerializeField] private int _maxHealth = 10;
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    [Header("Movement Stats")]
    [SerializeField] private float _moveSpeed = 1f;
    public float MoveSpeed { get; private set; }
    #endregion

    #region Unity Methods
    protected virtual void Awake()
    {
        MaxHealth = _maxHealth;
        MoveSpeed = _moveSpeed;
        CurrentHealth = MaxHealth;
    }
    #endregion

    #region Status Life
    public void TakeDamage(byte amount, Transform attackerTransform)
    {
        CurrentHealth = UpdateHealth(CurrentHealth, amount);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public int UpdateHealth(int currentHealth, int damageTaken)
    {
        return currentHealth - damageTaken;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    #endregion
}
