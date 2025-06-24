using System.Collections;
using UnityEngine;

/***
 * Manages combat interactions for both the player and enemies.
 * 
 * Responsibilities:
 * utility to solve combat dinamics
 ***/
public static class CombatHandler
{
    public static void ExecuteMeleeAttack(MonoBehaviour coroutineRunner, GameObject hitboxObject, float duration)
    {
        coroutineRunner.StartCoroutine(AttackCoroutine(hitboxObject, duration));
    }

    private static IEnumerator AttackCoroutine(GameObject hitboxObject, float duration)
    {
        hitboxObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        hitboxObject.SetActive(false);
    }

    // [Reemplazado por ExecuteMeleeAttack basado en Collider 3D]
    #region [LEGACY] OverlapCircle-Based Combat
    /*public static void ResolveRangeAttack(CharacterBase attacker, float range, LayerMask targets)
    {
        Collider2D targetHit = Physics2D.OverlapCircle(attacker.transform.position, range, targets);
        //Physics2D.OverlapCapsule
        Debug.Log(attacker.GetType());
        
        if (targetHit && targetHit.TryGetComponent<IDamageable>(out var victim))
        {
          victim.TakeDamage(attacker.AttackPower, attacker.transform);
        }
    }

    public static void ResolveMeleeAttack(CharacterBase attacker, float range, LayerMask targets)
    {
        Collider2D targetHit = Physics2D.OverlapCircle(attacker.Weapon.position, range, targets);
        //Physics2D.OverlapCapsule

        Debug.Log(attacker.GetType());

        if (targetHit && targetHit.TryGetComponent<IDamageable>(out var victim))
        {
            victim.TakeDamage(attacker.AttackPower, attacker.transform);
        }
    }*/
    #endregion
}
