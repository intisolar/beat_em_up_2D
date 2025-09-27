using UnityEngine;
using UnityEngine.UI;

public class BrightnessOption : MonoBehaviour
{
    [SerializeField] private Slider _sliderBrightness;
    [SerializeField] private Image _panelBrightness;

    [SerializeField] private float _valueBrightness;

    private static BrightnessOption s_instance;

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        s_instance = this;

        Transform root = transform;
        while (root.parent != null)
        {
            root = root.parent; // Encontrar el padre más cercano
        }

        DontDestroyOnLoad(root.gameObject); // No destruir el objeto raíz
    }

    private void Start()
    {
        InitializeValues();
        SetupListeners();
    }

    private void InitializeValues()
    {
        if (_sliderBrightness == null)
        {
            Debug.LogWarning("Slider de brillo no asignado en el inspector.");
            return;
        }

        if (_panelBrightness == null)
        {
            Debug.LogWarning("Panel de brillo no asignado en el inspector.");
            return;
        }

        _sliderBrightness.value = PlayerPrefs.GetFloat("Brightness", 5);
        _valueBrightness = _sliderBrightness.value;
        UpdateBrightness();
    }

    private void SetupListeners()
    {
        if (_sliderBrightness != null)
        {
            _sliderBrightness.onValueChanged.AddListener(ChangeSlider);
        }
    }

    private void UpdateBrightness()
    {
        if (_panelBrightness == null) return;

        // Oscurecer
        if (_valueBrightness < 5)
        {
            _panelBrightness.color = new Color(0, 0, 0, 1 - _valueBrightness / 5);
        }
        // Iluminar
        else
        {
            _panelBrightness.color = new Color(1, 1, 1, (_valueBrightness - 5) / 5);
        }
    }

    public void ChangeSlider(float value)
    {
        if (_sliderBrightness == null) return;

        _valueBrightness = value;
        PlayerPrefs.SetFloat("Brightness", _valueBrightness);
        UpdateBrightness();
    }
}
