using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public StateController StateController { get; private set; }

    [Header("Patrol and Detect")]
    [SerializeField] private float _initialDelayMin = 0f;
    [SerializeField] private float _initialDelayMax = 3f;
    [SerializeField] private float _visionRadius = 1f;
    [SerializeField] private Vector2 _initialDirection;

    public float VisionRadius => _visionRadius;
    public float InitialDelayMin => _initialDelayMin;
    public float InitialDelayMax => _initialDelayMax;
    public Vector2 InitialDirection => _initialDirection;

    [Header("Dependencies")]
    [SerializeField] private EnemyCharacter _owner;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        InitializeComponents();

        float offset = Random.Range(_initialDelayMin, _initialDelayMax);
        StateController = new StateController(new PatrolState(_owner, offset, _initialDirection));
    }

    private void Update()
    {
        StateController.UpdateState();
        StateController.DetectPlayer(this, _owner);

        if (_owner.DetectPlayer(this, out Transform playerTransform))
        {
            if (_owner.CanAttack())
            {
                _owner.Attack();
                if (_animator != null)
                {
                    _animator.SetTrigger("isAttack");
                    StartCoroutine(ResetToWalk());
                }
            }
        }
    }

    private System.Collections.IEnumerator ResetToWalk()
    {
        yield return new WaitForSeconds(0.5f);
        if (_animator != null)
        {
            _animator.SetTrigger("isWalk");
        }
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

        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
            Debug.LogWarning("Animator no está asignado en el inspector. Se ha asignado automáticamente.");
        }
    }
}
