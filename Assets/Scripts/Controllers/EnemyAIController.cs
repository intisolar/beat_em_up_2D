using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public StateController StateController { get; private set; }

    [Header("Patrol and Detect")]
    [SerializeField] private float initialDelayMin = 0f;
    [SerializeField] private float initialDelayMax = 3f;
    [SerializeField] private float visionRadius = 1f;
    [SerializeField] private Vector2 initialDirection;

    public float VisionRadius => visionRadius;
    public float InitialDelayMin => initialDelayMin;
    public float InitialDelayMax => initialDelayMax;
    public Vector2 InitialDirection => initialDirection;

    [Header("Dependencies")]
    [SerializeField] private EnemyCharacter _owner;
    [SerializeField] private Rigidbody _rigidbody;

    private void Awake()
    {
        float offset = Random.Range(initialDelayMin, initialDelayMax);
        StateController = new StateController(new PatrolState(_owner, offset, initialDirection));
    }

    private void Update()
    {
        StateController.UpdateState();
        StateController.DetectPlayer(this, _owner);
    }
}
