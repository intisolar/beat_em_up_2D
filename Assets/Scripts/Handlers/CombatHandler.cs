using System.Collections;
using UnityEngine;

namespace Handlers
{
    /***
     * Manages combat interactions for both the player and enemies.
     * 
     * Responsibilities:
     * utility to solve combat dinamics
     ***/
    public static class CombatHandler
    {
        public static void ExecuteMeleeAttack(MonoBehaviour coroutineRunner, GameObject hitboxObject, float duration, byte attackPower)
        {
            coroutineRunner.StartCoroutine(AttackCoroutine(hitboxObject, duration, attackPower));
        }

        private static IEnumerator AttackCoroutine(GameObject hitboxObject, float duration, byte attackPower)
        {
            hitboxObject.SetActive(true);
            var hitBoxComponent = hitboxObject.GetComponent<HitBoxComponent>();
            if (hitBoxComponent != null)
            {
                hitBoxComponent.SetAttackPower(attackPower);
            }
            yield return new WaitForSeconds(duration);
            hitboxObject.SetActive(false);
        }
    }
}
