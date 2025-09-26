using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerInput _playerInput;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
    }
}
