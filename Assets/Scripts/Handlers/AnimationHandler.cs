using UnityEngine;

namespace Handlers
{
    public class AnimationHandler
    {
        private readonly Animator _animator;

        public AnimationHandler(Animator animator)
        {
            _animator = animator;
        }

        public void UpdateWalkAnimation(bool isWalking)
        {
            _animator.SetBool("isWalk", isWalking);
        }

        public void TriggerAttackAnimation()
        {
            _animator.SetTrigger("isAttack");
        }

        public Animator Animator => _animator;
    }
}
