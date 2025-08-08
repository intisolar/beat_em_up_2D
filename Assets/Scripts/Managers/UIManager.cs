using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Score")]
    [SerializeField] private int _score;
    [SerializeField] private TextMeshProUGUI _textUI;

    [Header("Health Bar")]
    [SerializeField] private HealthBar _healthBar;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        _textUI.text = _score.ToString();
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (_healthBar != null)
        {
            _healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
    }
}
