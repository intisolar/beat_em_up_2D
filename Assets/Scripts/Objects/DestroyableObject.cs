using UnityEngine;

public abstract class DestroyableObject : MonoBehaviour, IDamageable
{
    [SerializeField] protected int _life = 100;

    public void TakeDamage(byte amount, Transform attackerTransform)
    {
        _life = UpdateHealth(_life, amount);

        if (_life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int UpdateHealth(int currentHealth, int damageTaken)
    {
        return currentHealth - damageTaken;
    }
}
