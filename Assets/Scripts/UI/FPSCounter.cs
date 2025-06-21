using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textFPS;
    float _elapsedTime = 0.5f;

    [Tooltip("Número de decimales. Utilización de un Start(), no se actualizará frame a frame.")]
    [SerializeField, Range(0, 5)] int _decimals = 0;
    string _decimalString;
    

    [Header("Dependencies")]
    [SerializeField] private PlayerInput _playerInput;

    private bool _isFPSVisible = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _decimalString = "F" + _decimals;
    }

    private void Update()
    {
        Show();

        if (_isFPSVisible)
        {
            UpdateFPS();
        }
    }

    private void UpdateFPS()
    {
        _elapsedTime += Time.unscaledDeltaTime;

        if (_elapsedTime >= 0.5f)
        {
            // Calcular FPS Promedio
            float FPSValue = 1f / Time.unscaledDeltaTime;

            // Actualizar el Texto de FPS
            _textFPS.text = "FPS " + FPSValue.ToString(_decimalString);

            _elapsedTime = 0f;
        }
    }

    private void Show()
    {
        if (_playerInput.actions["FPS"].triggered)
        {
            _isFPSVisible = !_isFPSVisible;
            _textFPS.gameObject.SetActive(_isFPSVisible);
        }
    }
}