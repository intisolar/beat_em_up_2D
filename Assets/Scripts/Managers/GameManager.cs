using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game Over")]
    [SerializeField] private GameObject _gameOverScreen;

    private void Awake()
    {
        Time.timeScale = 1;

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void TriggerGameOver()
    {
        if (_gameOverScreen != null)
        {
            _gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogWarning("GameOverScreen no está asignado en el GameManager.");
        }
    }
}
