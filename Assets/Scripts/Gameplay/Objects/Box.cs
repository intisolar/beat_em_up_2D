using UnityEngine;

public class Crate : DestroyableObject  
{
    [Header("Drop")]
    [SerializeField] [Range(0, 100)] private float _dropChance = 25f;
    [SerializeField] private GameObject _itemPrefab;

    private void Start()
    {
        _animationPrefix = "Crate";
    }

    public override void TakeDamage(byte amount, Transform attackerTransform)
    {
        base.TakeDamage(amount, attackerTransform);
        UpdateAnimationFrame();

        if (_life <= 0)
        {
            TryDropItem();
        }
    }

    private void TryDropItem()
    {
        if (_itemPrefab == null) return;

        float randomValue = Random.Range(0f, 100f);
        if (randomValue <= _dropChance)
        {
            Instantiate(_itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
