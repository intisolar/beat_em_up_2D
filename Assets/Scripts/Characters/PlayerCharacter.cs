using UnityEngine;

/***
 * Represents the playable characters.
 * 
 * It manages generic behavior of player subclasses
 ***/
public abstract class PlayerCharacter : CharacterBase
{
    public override bool PerformAttack()
    {
        return true;
    }

    /// <summary>
    /// It will give standard stats to all playerCharacters
    /// </summary>
    protected override void InitStats()
    {
        Debug.Log("PlayerCharacter.InitStats");
        _maxHealth = 100;
        _currentHealth = MaxHealth;
        _attackPower = 10;
        _defense = 5;
        _moveSpeed = 5f;
        _attackSpeed = 1f;
        _knockbackPower = 5f;
    }
}
