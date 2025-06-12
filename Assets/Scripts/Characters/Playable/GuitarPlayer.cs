using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GuitarPlayer : PlayerCharacter
{

    [SerializeField] private float _speed = 5;
    private bool _isAttacking = false;
    private float _attackDuration = 0.25f;
    private float _remainingAttackTime;

    [Header("Dependencies")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _fist;

    protected override void Start()
    {
        base.InitStats();
        _remainingAttackTime = _attackDuration;

    }

    private void Update()
    {
        HandleAttack();
    }

    void FixedUpdate()
    {
        Vector2 movementInput = _playerInput.actions["Move"].ReadValue<Vector2>();
        _rigidbody.linearVelocity = movementInput * _speed;
    }

    void HandleAttack()
    {
        if (!_isAttacking && _playerInput.actions["Attack"].triggered) //esta habilitado para atacar? _isAttackEnabled ? if true:
        {
            _isAttacking = true;
            _fist.SetActive(true);
            _remainingAttackTime = _attackDuration;
            //AttackCoolDown()
            //StartCoroutine(AttackCoolDown());
        } //else 

        if (_isAttacking)
        {
            _remainingAttackTime -= Time.deltaTime;
            if (_remainingAttackTime <= 0f)
            {
                _fist.SetActive(false);
                _isAttacking = false;
            }
        }
    }
    IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(2);
        //_isAttackEnabled = true;
    }
    
}
