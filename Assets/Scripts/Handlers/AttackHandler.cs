using System.Collections;
using UnityEngine;

namespace Handlers
{
    public class AttackHandler
    {
        private readonly AnimationHandler _animationHandler;
        private readonly GameObject _attackHitBox;
        private readonly float _attackDelay;
        private readonly float _attackDuration;

        public AttackHandler(AnimationHandler animationHandler, GameObject attackHitBox, float attackDelay, float attackDuration)
        {
            _animationHandler = animationHandler;
            _attackHitBox = attackHitBox;
            _attackDelay = attackDelay;
            _attackDuration = attackDuration;
        }

        public IEnumerator ExecuteAttack(byte attackPower)
        {
            _animationHandler.TriggerAttackAnimation();
            yield return new WaitForSeconds(_attackDelay);
            CombatHandler.ExecuteMeleeAttack(_animationHandler.Animator.gameObject.GetComponent<MonoBehaviour>(), _attackHitBox, _attackDuration, attackPower);
        }
    }
}
