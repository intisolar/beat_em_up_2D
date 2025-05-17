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
    }

    public override void TakeDamage(int amount)
    {
    }
}
