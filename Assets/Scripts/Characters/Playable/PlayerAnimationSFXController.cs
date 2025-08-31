using UnityEngine;

public class PlayerAnimationSFXController : MonoBehaviour
{

    [SerializeField]
    private AK.Wwise.Event step;

    public void PlayFootSteps()
    {
            step.Post(gameObject);
    }

}
