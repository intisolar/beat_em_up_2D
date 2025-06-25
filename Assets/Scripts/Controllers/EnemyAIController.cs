using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public StateController StateController { get; private set; }

    [Header("Dependencies")]
    [SerializeField] private EnemyCharacter _owner;
    [SerializeField] private Rigidbody _rigidbody;

    private void Awake()
    {
        float offset = Random.Range(_owner.InitialDelayMin, _owner.InitialDelayMax);
        StateController = new StateController(new PatrolState(_owner, offset, _owner.InitialDirection));
    }

    private void Update()
    {
        StateController.UpdateState();
        StateController.DetectPlayer(_owner);
    }
}
