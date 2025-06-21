using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GuitarPlayer : PlayerCharacter
{
    [Header("Movement")]
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _maxYPosition = 0.5f;
    [SerializeField] private float _minYPosition = -1f;

    [Header("Dependencies")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _fist;

    [Header("Attack")]
    [SerializeField] private float _attackDuration = 0.25f;

    private bool _isAttacking = false;
    private float _remainingAttackTime;

    protected override void Start()
    {
        base.InitStats();
        _remainingAttackTime = _attackDuration;
    }

    private void Update()
    {
        HandleAttack();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 input = _playerInput.actions["Move"].ReadValue<Vector2>();

        Vector3 currentPosition = _rigidbody.position;

        float clampedY = Mathf.Clamp(currentPosition.y + input.y * _speed * Time.fixedDeltaTime, _minYPosition, _maxYPosition);
        float verticalVelocity = (clampedY - currentPosition.y) / Time.fixedDeltaTime;

        float zVelocity = verticalVelocity;

        Vector3 velocity = new Vector3(input.x * _speed, verticalVelocity, zVelocity);
        _rigidbody.linearVelocity = velocity;
    }

    private void HandleAttack()
    {
        if (!_isAttacking && _playerInput.actions["Attack"].triggered)
        {
            StartAttack();
        }

        if (_isAttacking)
        {
            _remainingAttackTime -= Time.deltaTime;
            if (_remainingAttackTime <= 0f)
            {
                EndAttack();
            }
        }
    }

    private void StartAttack()
    {
        _isAttacking = true;
        _fist.SetActive(true);
        _remainingAttackTime = _attackDuration;
        //AttackCoolDown()
        //StartCoroutine(AttackCoolDown());
    }

    private void EndAttack()
    {
        _isAttacking = false;
        _fist.SetActive(false);
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(2f);
        //_isAttackEnabled = true;
    }
}
