using UnityEngine;

public class RegularGuard : EnemyCharacter
{
    protected override void Start()
    {

    }

    /// <summary>
    /// It will give standard stats to a RegularGuard
    /// </summary>
    protected override void InitStats()
    {
        Debug.Log(gameObject.name + " RegularGuard.InitStats");

        if (_maxHealth <= 0)
        {
            _maxHealth = 100;
            Debug.Log(name + " set maxHealth to default 100");
        }
        if (_currentHealth <= 0) _currentHealth = _maxHealth;
        if (_attackPower <= 0) _attackPower = 10;
        if (_defense <= 0) _defense = 5;
        if (_moveSpeed <= 0f) _moveSpeed = 0.5f;
        if (_attackSpeed <= 0f) _attackSpeed = 1f;
        if (_knockbackPower <= 0f) _knockbackPower = 5f;
        if (_range <= 0f) _range = 0.5f;
    }
}
