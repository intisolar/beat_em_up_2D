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
        InitializeComponents();

        float offset = Random.Range(initialDelayMin, initialDelayMax);
        StateController = new StateController(new PatrolState(_owner, offset, initialDirection));
    }

    private void Update()
    {
        StateController.UpdateState();
        StateController.DetectPlayer(this, _owner);
    }

    private void InitializeComponents()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
            Debug.LogWarning("Rigidbody no está asignado en el inspector. Se ha asignado automáticamente.");
        }

        if (_owner == null)
        {
            _owner = GetComponent<EnemyCharacter>();
            Debug.LogWarning("EnemyCharacter no está asignado en el inspector. Se ha asignado automáticamente.");
        }
    }
}
