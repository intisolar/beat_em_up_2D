using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Basic Stats")]
    public int maxHealth = 100;
    public int currentHealth;
    public int attackPower = 10;
    public int defense = 5;
    public float moveSpeed = 5f;
    public float attackSpeed = 1f;
    public float knockbackPower = 5f;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        int damageTaken = Mathf.Max(damage - defense, 0);
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " murió.");
        Destroy(gameObject); // Esto destruye el objeto en la escena
    }
}





