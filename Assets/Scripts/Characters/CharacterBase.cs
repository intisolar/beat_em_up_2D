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
    [Header("Basic Stats")]
    [SerializeField] protected int _maxHealth = 100;
    [SerializeField] protected int _currentHealth;
    [SerializeField] protected int _attackPower = 10;
    [SerializeField] protected int _defense = 5;
    [SerializeField] protected float _moveSpeed = 5f;
    [SerializeField] protected float _attackSpeed = 1f;
    [SerializeField] protected float _knockbackPower = 5f;

    protected virtual void Start()
    {
        InitStats();
    }

    public virtual void TakeDamage(int amount)
    {
        int damageTaken = Mathf.Max(amount - _defense, 0);
        _currentHealth -= damageTaken;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public abstract void PerformAttack();

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " died.");
        Destroy(gameObject);
    }
    /// <summary>
    /// It will give standard stats to a character but they will be ideally handled by the children
    /// </summary>
    protected virtual void InitStats()
    {
        Debug.Log("WARNING: CharacterBase.InitStats - it should not come here. Stats handled by children");
        _maxHealth = 100;
        _currentHealth = _maxHealth;
        _attackPower = 10;
        _defense = 5;
        _moveSpeed = 5f;
        _attackSpeed = 1f;
        _knockbackPower = 5f;

    }
    /// <summary>
    /// public method to update a character's health
    /// </summary>
    /// <param name="currentHealth"></param>
    /// <param name="damageTaken"></param>
    /// <returns></returns>
    public int UpdateHealth(int currentHealth, int damageTaken)
    {
        return currentHealth -= damageTaken;
    }
}
