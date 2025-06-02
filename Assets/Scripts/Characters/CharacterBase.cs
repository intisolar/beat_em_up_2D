using UnityEngine;


/*
 *  Description: An abstract class that defines the base structure for both player and enemy characters in the game.
 *  
 *  Responsibilities:
 *  Defines common attributes like health, energy, and other stats.
 *  Abstract methods for movement and interactions.
 */
public abstract class CharacterBase : MonoBehaviour, IAggressive, IDamageable
{
    public abstract bool PerformAttack();
    public abstract void TakeDamage(int amount);

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
