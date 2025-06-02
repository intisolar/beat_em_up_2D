using UnityEditor.Experimental.GraphView;
using UnityEngine;

/***
 * Manages combat interactions for both the player and enemies.
 * 
 * Responsibilities:
 * utility to solve combat dinamics
 ***/
public static class CombatHandler 
{
    public static void ResolveAttack(CharacterBase attacker,
                                 float range,
                                 LayerMask targets)
    {
        Collider2D targetHit = Physics2D.OverlapCircle(attacker.transform.position,
                                                 range, targets);
        Debug.Log(attacker.GetType());
        
        if (targetHit && targetHit.TryGetComponent<IDamageable>(out var victim))
        {

          //  victim.TakeDamage(attacker.Strength, attacker.transform);
        }
    }

   static void Main (string[] args)
    {
        Debug.Log("Hola");
    }
}
