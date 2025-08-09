using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    [Header("Dependencies")]
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _finish;

    private bool _isGamePaused = false;

    private void Start()
    {
        StartCoroutine(WaitForPlayerInput());
    }

    IEnumerator WaitForPlayerInput()
    {
        while (_playerInput == null)
        {
            _playerInput = Object.FindFirstObjectByType<PlayerInput>();
            yield return null;
        }
    }

    private void Update()
    {
        if (_playerInput == null) return;

        if (_playerInput.actions["Pause"].WasPressedThisFrame())
        {
            _isGamePaused = !_isGamePaused;
            PauseGame();
        }
    }

    private void PauseGame()
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

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
