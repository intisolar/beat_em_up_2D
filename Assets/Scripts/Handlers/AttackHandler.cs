using System.Collections;
using UnityEngine;

namespace Handlers
{
    public class AttackHandler
    {
        private readonly AnimationHandler _animationHandler;
        private readonly GameObject _attackHitBox;

        public AttackHandler(AnimationHandler animationHandler, GameObject attackHitBox)
        {
            _animationHandler = animationHandler;
            _attackHitBox = attackHitBox;
        }

        public IEnumerator ExecuteAttack(AttackData attackData, int attackIndex, PlayerAnimationSFXController _sFXController)
        {
            if (!IsValidAttackIndex(attackData, attackIndex)) yield break;

            var selectedAttack = attackData.Attacks[attackIndex];
            _animationHandler.TriggerAttackAnimation();
            yield return new WaitForSeconds(selectedAttack.AttackDelay);
            CombatHandler.ExecuteMeleeAttack(
                _animationHandler.Animator.gameObject.GetComponent<MonoBehaviour>(),
                _attackHitBox,
                selectedAttack.AttackDuration,
                selectedAttack.AttackPower,
                _sFXController
            );
        }

        private bool IsValidAttackIndex(AttackData attackData, int attackIndex)
        {
            if (attackIndex < 0 || attackIndex >= attackData.Attacks.Count)
            {
                Debug.LogError("Índice de ataque fuera de rango.");
                return false;
            }
            return true;
        }
    }
}
