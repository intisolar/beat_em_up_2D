using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _velocity = 5f;

    [Header("Dependencies")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private PlayerInput _playerInput;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_playerInput.actions["Movement"].ReadValue<Vector2>() != Vector2.zero)
        {
            Vector2 moveInput = _playerInput.actions["Movement"].ReadValue<Vector2>();
            float moveDirection = moveInput.x;
            _rigidbody2D.linearVelocity = new Vector2(moveDirection * _velocity, 0);
        }
    }
}
