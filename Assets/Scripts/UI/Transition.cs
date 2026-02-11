using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] private float _destructionTime = 2f;

    private void Start()
    {
        Destroy(gameObject, _destructionTime);
    }
}
