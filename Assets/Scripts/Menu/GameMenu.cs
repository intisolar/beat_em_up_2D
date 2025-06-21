using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenu : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] GameObject _pausePanel;
    bool _isGamePaused = false;
    
    void Update()
    {
        if(_playerInput.actions["Pause"].WasPressedThisFrame())
        {
            _isGamePaused = !_isGamePaused;
            PauseGame();
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
