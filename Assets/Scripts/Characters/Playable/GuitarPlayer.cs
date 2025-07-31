using UnityEngine;
using UnityEngine.InputSystem;

public class GuitarPlayer : PlayerCharacter
{
    [Header("Attack")]
    [SerializeField] private float _attackDuration = 0.25f;

    [Header("Movement")]
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _maxYPosition = 0.5f;
    [SerializeField] private float _minYPosition = -1f;

    [Header("Dependencies")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _attack;

    private bool _canMoveY = false;
    private Vector3 _lastPosition;

    protected void Start()
    {
        base.InitStats();
    }

    private void Update()
    {
        HandleAttack();
    }

    private void FixedUpdate()
    {
        Move();

        if (!_canMoveY)
        {
            HandleIdleState();
        }

        _lastPosition = _rigidbody.position;
    }

    private void Move()
    {
        Vector2 input = _playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 currentPosition = _rigidbody.position;

        float zVelocity = input.y * _speed;

        _canMoveY = Mathf.Abs(currentPosition.z - _lastPosition.z) > Mathf.Epsilon;

        float clampedY = CalculateClampedY(currentPosition);
        float verticalVelocity = (clampedY - currentPosition.y) / Time.fixedDeltaTime;

        Vector3 velocity = new Vector3(input.x * _speed, verticalVelocity, zVelocity);
        _rigidbody.linearVelocity = velocity;

        FlipCharacter(input.x);
    }

    private float CalculateClampedY(Vector3 currentPosition)
    {
        if (_canMoveY)
        {
            float deltaZ = currentPosition.z - _lastPosition.z;
            float deltaY = deltaZ * (_maxYPosition - _minYPosition) / (_maxYPosition - _minYPosition);
            return Mathf.Clamp(currentPosition.y + deltaY, _minYPosition, _maxYPosition);
        }

        return currentPosition.y;
    }

    private void HandleIdleState()
    {
        Vector3 currentPosition = _rigidbody.position;
        float clampedY = Mathf.Clamp(currentPosition.z, _minYPosition, _maxYPosition);
        _rigidbody.position = new Vector3(currentPosition.x, clampedY, currentPosition.z);
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

    private void HandleAttack()
    {
        if (_playerInput.actions["Attack"].triggered)
        {
            CombatHandler.ExecuteMeleeAttack(this, _attack, _attackDuration);
        }
    }
}
