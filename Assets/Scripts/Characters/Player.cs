using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5;

    [Header("Dependencies")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerInput _playerInput;

    void FixedUpdate()
    {
        Vector2 movementInput = _playerInput.actions["Move"].ReadValue<Vector2>();
        _rigidbody.linearVelocity = movementInput * _speed;
    }
}
