using UnityEngine;

/*
 *  Description: An abstract class that defines the base structure for both player and enemy characters in the game.
 *  
 *  Responsibilities:
 *  Defines common attributes like health, energy, and other stats.
 *  Abstract methods for movement and interactions.
 */
public abstract class CharacterBase : MonoBehaviour, IAggressive, IDamageable
{
    #region Variables
    [Header("Stats")]
    [field: SerializeField] public int MaxHealth { get; private set; } = 10;
    [field: SerializeField] public int CurrentHealth { get; set; }
    [field: SerializeField] public float AttackPower { get; private set; } = 10;
    [field: SerializeField] public float Defense { get; private set; } = 5;
    [field: SerializeField] public float MoveSpeed { get; private set; } = 1f;
    [field: SerializeField] public float AttackSpeed { get; private set; } = 1f;
    [field: SerializeField] public float KnockbackPower { get; private set; } = 5f;

    [Header("Attack Data")]
    [field: SerializeField] public LayerMask TargetLayers { get; private set; }
    [field: SerializeField] public Transform Weapon { get; private set; }
    [field: SerializeField] public float Range { get; private set; } = 0.5f;
    #endregion

    #region Unity Methods
    protected virtual void Awake()
    {
        
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
        Debug.Log($"{gameObject.name} died.");
        Destroy(gameObject);
    }
    #endregion

    public abstract bool PerformAttack();

    #region Properties
    #endregion
}
