using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GuitarPlayer : PlayerCharacter
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 5f;

    [Header("Dependencies")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _fist;

    [Header("Attack Settings")]
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
        SyncDepthToYAxis();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 input = _playerInput.actions["Move"].ReadValue<Vector2>();
        _rigidbody.linearVelocity = input * _speed;
    }

    private void HandleAttack()
    {
        if (!_isAttacking && _playerInput.actions["Attack"].triggered) //esta habilitado para atacar? _isAttackEnabled ? if true:
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

    private void SyncDepthToYAxis()
    {
        Vector3 position = _rigidbody.position;
        _rigidbody.MovePosition(new Vector3(position.x, position.y, position.y));
    }
}
