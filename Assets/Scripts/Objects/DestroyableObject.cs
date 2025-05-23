using Unity.VisualScripting;
using UnityEngine;

public abstract class DestroyableObject : MonoBehaviour, IDamageable
{
    [SerializeField] protected int _life = 100;

    public void TakeDamage(int amount)
    {
        //reduceLife
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            //call TakeDamage here passing the damage dealt
            _life--;

            if (_life <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
