using UnityEngine;

/***
 * Represents the playable characters.
 * 
 * It manages generic behavior of player subclasses
 ***/
public abstract class PlayerCharacter : CharacterBase
{
    public int _playerLife = 100;
    public override void PerformAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int damageAmount)
    {
        _playerLife -= damageAmount;
        if (_playerLife <= 0) 
        {
            Destroy(this.gameObject);
        }
    }
}
