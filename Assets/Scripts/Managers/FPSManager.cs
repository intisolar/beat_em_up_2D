using UnityEngine;

public class FPSManager : MonoBehaviour
{
    public static FPSManager Instance { get; private set; }

    [SerializeField] private int _limitFPS = 120;
    [SerializeField] private FPSCounter _FPSCounter;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Application.targetFrameRate = _limitFPS;
    }

    private void Update()
    {
        HandleFPSInput();
    }

    private void HandleFPSInput()
    {
        var playerInput = GameManager.Instance._playerInput;
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput no está asignado.");
            return;
        }

        if (playerInput.actions["FPS"].WasPressedThisFrame())
        {
            _FPSCounter.Show();
        }
    }
}