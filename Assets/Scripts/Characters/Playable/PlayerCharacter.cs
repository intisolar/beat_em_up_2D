using UnityEngine;
using UnityEngine.InputSystem;
using Handlers;

public class PlayerCharacter : CharacterBase
{
    [Header("Movement Limits")]
    [SerializeField] private float _maxPosition = 0.5f;
    [SerializeField] private float _minPosition = -1f;

    [Header("Attack")]
    [SerializeField] private AttackData _attackData;
    [SerializeField] private GameObject _attackHitBox;

    private AttackHandler _attackHandler;

    [Header("Dependencies")]
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Animator _animator;

    private bool _canMoveY = false;
    private Vector3 _lastPosition;

    private AnimationHandler _animationHandler;
    private MovementHandler _movementHandler; 

    protected void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        if (_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
            Debug.LogWarning("PlayerInput no está asignado en el inspector. Se ha asignado automáticamente.");
        }

        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
            Debug.LogWarning("Animator no está asignado en el inspector. Se ha asignado automáticamente.");
        }

        _animationHandler = new AnimationHandler(_animator);
        _movementHandler = new MovementHandler(Rigidbody, MoveSpeed, _minPosition, _maxPosition);
        _attackHandler = new AttackHandler(_animationHandler, _attackHitBox);
    }

    private void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {
        Move();

        if (!_canMoveY)
        {
            _movementHandler.HandleIdleState(Rigidbody.position);
        }

        _lastPosition = Rigidbody.position;
    }

    private void Move()
    {
        Vector2 input = _playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 currentPosition = Rigidbody.position;

        _canMoveY = Mathf.Abs(currentPosition.z - _lastPosition.z) > Mathf.Epsilon;

        Vector3 velocity = _movementHandler.CalculateVelocity(input, currentPosition, _lastPosition, _canMoveY);
        Rigidbody.linearVelocity = velocity;

        FlipCharacter(input.x);

        bool isWalking = Mathf.Abs(input.x) > Mathf.Epsilon || Mathf.Abs(input.y) > Mathf.Epsilon;
        _animationHandler.UpdateWalkAnimation(isWalking);
    }

    private void FlipCharacter(float horizontalInput)
    {
        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Attack()
    {
        if (_playerInput.actions["Attack"].triggered)
        {
            StartCoroutine(_attackHandler.ExecuteAttack(_attackData, 0));
        }
    }

    public override void TakeDamage(byte amount, Transform attackerTransform)
    {
        base.TakeDamage(amount, attackerTransform);

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateHealthBar(CurrentHealth, MaxHealth);
        }

        Debug.Log($"{gameObject.name} ha recibido {amount} de daño. Salud actual: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TriggerGameOver();
            }
        }
    }
}
