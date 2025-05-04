using UnityEngine;

/***
 * Represents the playable characters.
 * 
 * It manages generic behavior of enemy subclasses
 ***/
public abstract class EnemyCharacter : CharacterBase
{
    public override void PerformAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int amount)
    {
        throw new System.NotImplementedException();
    }
}
