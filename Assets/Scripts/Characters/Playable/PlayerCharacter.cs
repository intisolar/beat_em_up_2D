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
}
