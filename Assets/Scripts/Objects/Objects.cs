using Unity.VisualScripting;
using UnityEngine;

public abstract class Objects : MonoBehaviour
{
    [SerializeField] protected int _life = 100;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            _life--;

            if (_life <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
