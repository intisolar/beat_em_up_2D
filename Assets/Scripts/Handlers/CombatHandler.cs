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
        public static void ExecuteMeleeAttack(MonoBehaviour coroutineRunner, GameObject hitboxObject, 
            float duration, byte attackPower, PlayerAnimationSFXController _sFXController)
        {
            coroutineRunner.StartCoroutine(AttackCoroutine(hitboxObject, duration, attackPower, _sFXController));
        }

        private static IEnumerator AttackCoroutine(GameObject hitboxObject, float duration, byte attackPower,
            PlayerAnimationSFXController _sFXController)
        {
            hitboxObject.SetActive(true);
            var hitBoxComponent = hitboxObject.GetComponent<AttackHitBox>();

            if (hitBoxComponent != null)
            {
                hitBoxComponent.SetAttackPower(attackPower);
               // _sFXController.PlayHit();
            }
            
            yield return new WaitForSeconds(duration);
            hitboxObject.SetActive(false);
        }
    }
}
