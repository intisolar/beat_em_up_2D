using UnityEngine;

public class Can : MonoBehaviour
{
    [SerializeField] private byte _healthIncrease = 2;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<PlayerCharacter>(out var player))
        {
            player.IncreaseHealth(_healthIncrease);
            Destroy(gameObject);
        }
    }
}
