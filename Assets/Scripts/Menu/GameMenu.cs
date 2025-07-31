using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject _pausePanel;

    [Header("Dependencies")]
    [SerializeField] PlayerInput _playerInput;
    
    bool _isGamePaused = false;

    void Update()
    {
        if (_playerInput.actions["Pause"].WasPressedThisFrame())
        {
            if (_playerInput != null)
            {
                _isGamePaused = !_isGamePaused;
                PauseGame();
            }
            else
            {
                _playerInput = Object.FindFirstObjectByType<PlayerInput>();
            }
        }
    }

    void PauseGame()
    {
        if (_isGamePaused)
        {
            Time.timeScale = 0;
            _pausePanel.SetActive(true);
            Application.targetFrameRate = 30;
        }
        else
        {
            Time.timeScale = 1;
            _pausePanel.SetActive(false);
            Application.targetFrameRate = 120;
        }
    }
}
