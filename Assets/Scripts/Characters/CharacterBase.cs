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
    [Header("Stats")]
    [SerializeField] protected int _maxHealth = -1;
    [SerializeField] protected int _currentHealth = -1;
    [SerializeField] protected int _attackPower = -1;
    [SerializeField] protected int _defense = -1;
    [SerializeField] protected float _moveSpeed = -1f;
    [SerializeField] protected float _attackSpeed = -1f;
    [SerializeField] protected float _knockbackPower = -1f;

    [Header("Attack Data")]
    [SerializeField] protected LayerMask _targetLayers;
    [SerializeField] protected Transform _weapon;
    [SerializeField] protected float _range = -1f;

    protected virtual void Start()
    {

    }

    protected virtual void Awake()
    {
        InitStats();
    }

    public abstract bool PerformAttack();
    
    public virtual void TakeDamage(int amount, Transform attackerTransform)
    {
        int damageTaken = Mathf.Max(amount - _defense, 0);
        UpdateHealth(_currentHealth, damageTaken);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        Destroy(gameObject);
    }

    /// <summary>
    /// It will give standard stats to a character but they will be ideally handled by the children
    /// </summary>
    protected virtual void InitStats()
    {
        Debug.Log("WARNING: CharacterBase.InitStats - it should not come here. Stats handled by children");
        if (_maxHealth <= 0) _maxHealth = 100;
        if (_currentHealth <= 0) _currentHealth = _maxHealth;
        if (_attackPower <= 0) _attackPower = 10;
        if (_defense <= 0) _defense = 5;
        if (_moveSpeed <= 0f) _moveSpeed = 5f;
        if (_attackSpeed <= 0f) _attackSpeed = 1f;
        if (_knockbackPower <= 0f) _knockbackPower = 5f;
        if (_range <= 0f) _range = 0.5f;
    }
    
    /// <summary>
    /// public method to update a character's health
    /// </summary>
    /// <param name="currentHealth"></param>
    /// <param name="damageTaken"></param>
    /// <returns></returns>
    /// 
    public int UpdateHealth(int currentHealth, int damageTaken)
    {
        return currentHealth -= damageTaken;
    }

    //Getters && Setters
    public int MaxHealth { get => _maxHealth; protected set => _maxHealth = value; }
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public int AttackPower { get => _attackPower; private set => _attackPower = value; }
    public int Defense { get => _defense; private set => _defense = value; }
    public float MoveSpeed { get => _moveSpeed; private set => _moveSpeed = value; }
    public float AttackSpeed { get => _attackSpeed; private set => _attackSpeed = value; }
    public float KnockbackPower { get => _knockbackPower; private set => _knockbackPower = value; }
    public Transform Weapon { get => _weapon; set => _weapon = value; }
}
