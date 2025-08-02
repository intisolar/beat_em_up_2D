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
    [SerializeField] private int _maxHealth = 10;
    public int MaxHealth { get; private set; }

    [SerializeField] private int _currentHealth;
    public int CurrentHealth { get; private set; }

    [SerializeField] private float _attackPower = 10;
    public float AttackPower { get; private set; }

    [SerializeField] private float _defense = 5;
    public float Defense { get; private set; }

    [SerializeField] private float _moveSpeed = 1f;
    public float MoveSpeed { get; private set; }

    [SerializeField] private float _attackSpeed = 1f;
    public float AttackSpeed { get; private set; }

    [SerializeField] private float _knockbackPower = 5f;
    public float KnockbackPower { get; private set; }

    [Header("Attack Data")]
    [SerializeField] private LayerMask _targetLayers;
    public LayerMask TargetLayers { get; private set; }

    [SerializeField] private Transform _weapon;
    public Transform Weapon { get; private set; }

    [SerializeField] private float _range = 0.5f;
    public float Range { get; private set; }
    #endregion

    #region Unity Methods
    protected virtual void Awake()
    {
        MaxHealth = _maxHealth;
        CurrentHealth = _currentHealth;
        AttackPower = _attackPower;
        Defense = _defense;
        MoveSpeed = _moveSpeed;
        AttackSpeed = _attackSpeed;
        KnockbackPower = _knockbackPower;
        TargetLayers = _targetLayers;
        Weapon = _weapon;
        Range = _range;
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

    #region Movements
    public abstract bool PerformAttack();
    #endregion
}
