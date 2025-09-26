using UnityEngine;

public class Crate : DestroyableObject  
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
