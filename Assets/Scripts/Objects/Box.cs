using UnityEngine;

public class Box : DestroyableObject  
{
    private void Start()
    {
        _animationPrefix = "Crate";
    }

    public override void TakeDamage(byte amount, Transform attackerTransform)
    {
        base.TakeDamage(amount, attackerTransform);
        UpdateAnimationFrame();
    }
}
