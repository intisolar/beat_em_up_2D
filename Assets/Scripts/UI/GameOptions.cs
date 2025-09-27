using UnityEngine;

public class GameOptions : MonoBehaviour
{
    public static GameOptions Instance { get; private set; }

    [SerializeField] private GameObject _optionsPanel;
    private bool isActive;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        var playerInput = GameManager.Instance._playerInput;
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput no está asignado.");
            return;
        }

        if (playerInput.actions["Options"].WasPressedThisFrame())
        {
            if (_optionsPanel == null)
            {
                Debug.LogError("OptionsPanel no están asignados.");
                return;
            }

            isActive = _optionsPanel.activeSelf;
            _optionsPanel.SetActive(!isActive);
            if (isActive)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }

    public void HideOptionsPanel()
    {
        if (_optionsPanel == null)
        {
            Debug.LogError("OptionsPanel no está asignado.");
            return;
        }

        _optionsPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
