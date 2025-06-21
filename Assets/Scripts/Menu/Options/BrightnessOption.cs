using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessOption : MonoBehaviour
{
    [SerializeField] Slider _sliderBrightness;
    [SerializeField] Image _panelBrightness;

    [SerializeField] float _valueBrightness;

    void Start()
    {
        _sliderBrightness.value = PlayerPrefs.GetFloat("Brightness", 5);
        _valueBrightness = _sliderBrightness.value;
    }

    void Update()
    {
        // Oscurecer.
        if (_valueBrightness < 5)
        {
            _panelBrightness.color = new Color(0, 0, 0, 1 - _valueBrightness / 5);
        }

        // Iluminar.
        else
        {
            _panelBrightness.color = new Color(1, 1, 1, (_valueBrightness - 5) / 5);
        }
    }

    public void ChangeSlider(float value)
    {
        _valueBrightness = value;
        PlayerPrefs.SetFloat("Brightness", _valueBrightness);
    }
}