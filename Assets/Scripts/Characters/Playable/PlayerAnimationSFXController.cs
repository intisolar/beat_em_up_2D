using UnityEngine;

public class PlayerAnimationSFXController : MonoBehaviour
{

    [SerializeField]
    private AK.Wwise.Event step;

    [SerializeField]
    private AK.Wwise.Event hit;

    [SerializeField]
    private AK.Wwise.Event meleeAttack;

    [SerializeField]
    private AK.Wwise.Event hurt;

    [SerializeField]
    private AK.Wwise.Event death;

    public void PlayFootSteps()
    {
        step.Post(gameObject);
    }

    public void PlayHit()
    {
        hit.Post(gameObject);
    }

    public void PlayAttack()
    { 
        meleeAttack.Post(gameObject); 
    }

    public void PlayHurt()
    {
        hurt.Post(gameObject);
    }
    public void PlayDeath()
    {
        death.Post(gameObject);
    }


}
