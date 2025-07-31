using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GuitarPlayer : PlayerCharacter
{
    [Header("Attack")]
    [SerializeField] private float _attackDuration = 0.25f;
    [SerializeField] private float _attackDelay = 0.1f;
    [SerializeField] private GameObject _attackHitBox;

    [Header("Movement")]
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _maxPosition = 0.5f;
    [SerializeField] private float _minPosition = -1f;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Animation")]
    [SerializeField] private Animator _animator;

    private bool _canMoveY = false;
    private Vector3 _lastPosition;

    private MovementHandler _movementHandler;
    private AnimationHandler _animationHandler;
    private AttackHandler _attackHandler;

    protected void Start()
    {
        base.InitStats();
        _movementHandler = new MovementHandler(_rigidbody, _speed, _minPosition, _maxPosition);
        _animationHandler = new AnimationHandler(_animator);
        _attackHandler = new AttackHandler(_animationHandler, _attackHitBox, _attackDelay, _attackDuration);
    }

    private void Update()
    {
        if (_playerInput.actions["Attack"].triggered)
        {
            StartCoroutine(_attackHandler.ExecuteAttack());
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

public class MovementHandler
{
    private readonly Rigidbody _rigidbody;
    private readonly float _speed;
    private readonly float _minPosition;
    private readonly float _maxPosition;

    public MovementHandler(Rigidbody rigidbody, float speed, float minPosition, float maxPosition)
    {
        _rigidbody = rigidbody;
        _speed = speed;
        _minPosition = minPosition;
        _maxPosition = maxPosition;
    }

    public Vector3 CalculateVelocity(Vector2 input, Vector3 currentPosition, Vector3 lastPosition, bool canMoveY)
    {
        float zVelocity = input.y * _speed;
        float clampedZ = Mathf.Clamp(currentPosition.z + zVelocity * Time.fixedDeltaTime, _minPosition, _maxPosition);
        float clampedY = CalculateClampedY(currentPosition, lastPosition, canMoveY);
        float verticalVelocity = (clampedY - currentPosition.y) / Time.fixedDeltaTime;

        return new Vector3(input.x * _speed, verticalVelocity, (clampedZ - currentPosition.z) / Time.fixedDeltaTime);
    }

    private float CalculateClampedY(Vector3 currentPosition, Vector3 lastPosition, bool canMoveY)
    {
        if (canMoveY)
        {
            float deltaZ = currentPosition.z - lastPosition.z;
            float deltaY = deltaZ * (_maxPosition - _minPosition) / (_maxPosition - _minPosition);
            return Mathf.Clamp(currentPosition.y + deltaY, _minPosition, _maxPosition);
        }

        return currentPosition.y;
    }

    public void HandleIdleState(Vector3 currentPosition)
    {
        float clampedY = Mathf.Clamp(currentPosition.z, _minPosition, _maxPosition);
        _rigidbody.position = new Vector3(currentPosition.x, clampedY, currentPosition.z);
    }
}

public class AnimationHandler
{
    private readonly Animator _animator;

    public AnimationHandler(Animator animator)
    {
        _animator = animator;
    }

    public void UpdateWalkAnimation(bool isWalking)
    {
        _animator.SetBool("isWalk", isWalking);
    }

    public void TriggerAttackAnimation()
    {
        _animator.SetTrigger("isAttack");
    }
}

public class AttackHandler
{
    private readonly AnimationHandler _animationHandler;
    private readonly GameObject _attackHitBox;
    private readonly float _attackDelay;
    private readonly float _attackDuration;

    public AttackHandler(AnimationHandler animationHandler, GameObject attackHitBox, float attackDelay, float attackDuration)
    {
        _animationHandler = animationHandler;
        _attackHitBox = attackHitBox;
        _attackDelay = attackDelay;
        _attackDuration = attackDuration;
    }

    public IEnumerator ExecuteAttack()
    {
        _animationHandler.TriggerAttackAnimation();
        yield return new WaitForSeconds(_attackDelay);
        CombatHandler.ExecuteMeleeAttack(null, _attackHitBox, _attackDuration);
    }
}
