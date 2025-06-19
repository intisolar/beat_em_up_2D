using UnityEngine;

/// <summary>
/// Description: Base class for items that the player can pick up.
/// </summary>
public class ItemPickUp : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
