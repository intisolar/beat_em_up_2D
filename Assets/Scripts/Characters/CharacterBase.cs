using System.Collections;
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
    [Header("Health")]
    [SerializeField] private int _maxLife = 10;
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 1f;
    public float MoveSpeed { get; private set; }
    [SerializeField] private Rigidbody _rigidbody;
    public Rigidbody Rigidbody { get; private set; }

    [Header("Damage Feedback")]
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private float damageDuration = 0.5f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    #endregion
    protected PlayerAnimationSFXController _sFXController;
    #region Unity Methods
    protected virtual void Awake()
    {
        MaxHealth = _maxLife;
        CurrentHealth = MaxHealth;
        MoveSpeed = _moveSpeed;
        Rigidbody = _rigidbody;

        InitializeComponents();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer no está asignado o no existe en el objeto.");
        }
    }
    #endregion

    protected virtual void InitializeComponents()
    {
        if (Rigidbody == null)
        {
            Rigidbody = GetComponent<Rigidbody>();
            Debug.LogWarning("Rigidbody no está asignado en el inspector. Se ha asignado automáticamente.");
        }
        if (_sFXController == null)
        {
            _sFXController = GetComponent<PlayerAnimationSFXController>();
        }
    }

    #region Status Life
    public virtual void TakeDamage(byte amount, Transform attackerTransform)
    {
        CurrentHealth = UpdateHealth(CurrentHealth, amount);

        if (CurrentHealth <= 0)
        {
            Die();
        }

        if (spriteRenderer != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashDamageColor());
        }
    }

    public int UpdateHealth(int currentHealth, int damageTaken)
    {
        return Mathf.Clamp(currentHealth - damageTaken, 0, MaxHealth);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    #endregion

    private IEnumerator FlashDamageColor()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(damageDuration);
        spriteRenderer.color = originalColor;
    }
}
