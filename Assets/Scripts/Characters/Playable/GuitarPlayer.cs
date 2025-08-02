using UnityEngine;
using UnityEngine.InputSystem;
using Handlers;

public class GuitarPlayer : PlayerCharacter
{
    [Header("Movement Limits")]
    [SerializeField] private float _maxPosition = 0.5f;
    [SerializeField] private float _minPosition = -1f;

    [Header("Attack")]
    [SerializeField] private float _attackDuration = 0.25f;
    [SerializeField] private float _attackDelay = 0.1f;
    [SerializeField] private GameObject _attackHitBox;

    [Header("Dependencies")]
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;

    private bool _canMoveY = false;
    private Vector3 _lastPosition;

    private MovementHandler _movementHandler;
    private AnimationHandler _animationHandler;
    private AttackHandler _attackHandler;

    protected void Start()
    {
        _movementHandler = new MovementHandler(_rigidbody, MoveSpeed, _minPosition, _maxPosition);
        _animationHandler = new AnimationHandler(_animator);
        _attackHandler = new AttackHandler(_animationHandler, _attackHitBox, _attackDelay, _attackDuration);
    }

    private void Update()
    {
        if (_playerInput.actions["Attack"].triggered)
        {
            StartCoroutine(_attackHandler.ExecuteAttack(1));
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (!_canMoveY)
        {
            _movementHandler.HandleIdleState(_rigidbody.position);
        }

        _lastPosition = _rigidbody.position;
    }

    private void Move()
    {
        Vector2 input = _playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 currentPosition = _rigidbody.position;

        _canMoveY = Mathf.Abs(currentPosition.z - _lastPosition.z) > Mathf.Epsilon;

        Vector3 velocity = _movementHandler.CalculateVelocity(input, currentPosition, _lastPosition, _canMoveY);
        _rigidbody.linearVelocity = velocity;

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
}
