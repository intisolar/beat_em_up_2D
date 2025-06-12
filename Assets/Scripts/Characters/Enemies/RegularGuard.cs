using UnityEngine;

public class RegularGuard : EnemyCharacter
{
    protected override void Start()
    {
        InitStats();
    }

    /// <summary>
    /// It will give standard stats to a character but they will be ideally handled by the children
    /// </summary>
    protected override void InitStats()
    {
        Debug.Log("WARNING: CharacterBase.InitStats - it should not come here. Stats handled by children");
        MaxHealth = 100;
        _currentHealth = MaxHealth;
        _attackPower = 10;
        _defense = 5;
        _moveSpeed = 2f;
        _attackSpeed = 1f;
        _knockbackPower = 5f;
        _range = 0.5f;
    }

}
