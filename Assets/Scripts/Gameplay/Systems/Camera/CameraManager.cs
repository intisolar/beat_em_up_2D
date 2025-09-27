using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Variables
    [Header("Section")]
    [SerializeField] private SectionData[] _sections;
    [SerializeField] private int _currentSection = 0;

    [Header("Player")]
    [SerializeField] private float _limitsExtra;

    [Header("UI")]
    [SerializeField] private GameObject _nextUI;
    [SerializeField] private float _nextUIDuration = 2f;
    [SerializeField] private bool _showNextUIOnLastSection = true;

    [Header("Dependencies")]
    [SerializeField] private GameObject _playerParent;
    [SerializeField] private Camera _targetCamera;

    [Header("Camera Transition")]
    [SerializeField] private float _cameraTransitionSpeed = 5f;
    [SerializeField] private bool _enableCameraSmoothing = true;

    [Header("Enemy Spawn")]
    [SerializeField] private float _enemySpawnOffsetX = 7.5f;

    private readonly List<GameObject> _activeEnemies = new();
    private Vector3 _targetCameraPosition;
    #endregion

    #region Player Handling
    private Transform GetPlayerTransform()
    {
        if (_playerParent == null) return null;
        Transform parentT = _playerParent.transform;
        return parentT.childCount > 0 ? parentT.GetChild(0) : parentT;
    }

    private void ClampPlayerPosition()
    {
        var currentSectionData = _sections[_currentSection];
        float playerMinX = 0 - _limitsExtra;
        float playerMaxX = currentSectionData.MaxBoundX + _limitsExtra;
        Transform playerTransform = GetPlayerTransform();
        if (playerTransform == null) return;
        float playerX = Mathf.Clamp(playerTransform.position.x, playerMinX, playerMaxX);
        playerTransform.position = new Vector3(playerX, playerTransform.position.y, playerTransform.position.z);
    }
    #endregion

    #region Camera Handling
    private void UpdateCameraPosition()
    {
        var currentSectionData = _sections[_currentSection];
        Transform playerTransform = GetPlayerTransform();
        if (playerTransform == null) return;

        float camX = Mathf.Clamp(playerTransform.position.x, 0, currentSectionData.MaxBoundX);
        _targetCameraPosition = new Vector3(camX, _targetCameraPosition.y, _targetCameraPosition.z);

        if (_enableCameraSmoothing)
        {
            // Interpolación suave hacia la posición objetivo
            _targetCamera.transform.position = Vector3.Lerp(
                _targetCamera.transform.position,
                _targetCameraPosition,
                Time.deltaTime * _cameraTransitionSpeed
            );
        }
        else
        {
            // Movimiento instantáneo hacia la posición objetivo
            _targetCamera.transform.position = _targetCameraPosition;
        }
    }

    #endregion Enemy Handling
    private void CheckEnemies()
    {
        _activeEnemies.RemoveAll(e => e == null);
        if (_activeEnemies.Count == 0)
        {
            AdvanceSection();
        }
    }

    private void SpawnEnemies(SectionData sectionData)
    {
        _activeEnemies.Clear();

        foreach (var enemyInfo in sectionData.Enemies)
        {
            for (int i = 0; i < enemyInfo.Quantity; i++)
            {
                // Generar enemigos en MaxBoundX + _enemySpawnOffsetX de la sección actual
                Vector3 spawnLocation = new Vector3(sectionData.MaxBoundX + _enemySpawnOffsetX, 0, 0);
                GameObject enemy = Instantiate(enemyInfo.Prefab, spawnLocation, Quaternion.identity);
                _activeEnemies.Add(enemy);
            }
        }
    }

    private void AdvanceSection()
    {
        _currentSection++;
        if (_currentSection < _sections.Length)
        {
            Debug.Log("Sección completada, avanzando a la siguiente.");
            StartCoroutine(ShowNextUI());
            SpawnEnemies(_sections[_currentSection]);

            // Actualizar la posición objetivo de la cámara al inicio de la nueva sección
            _targetCameraPosition = new Vector3(
                Mathf.Clamp(_targetCamera.transform.position.x, 0, _sections[_currentSection].MaxBoundX),
                _targetCamera.transform.position.y,
                _targetCamera.transform.position.z
            );
        }
        else
        {
            Debug.Log("¡Nivel Completado!");
            if (_showNextUIOnLastSection)
            {
                StartCoroutine(ShowNextUI());
            }
        }
    }

    private IEnumerator ShowNextUI()
    {
        if (_nextUI != null)
        {
            _nextUI.SetActive(true);
            yield return new WaitForSeconds(_nextUIDuration);
            _nextUI.SetActive(false);
        }
    }

    #region Unity Lifecycle
    private void Start()
    {
        if (_sections.Length > 0)
        {
            SpawnEnemies(_sections[0]);
            // Inicializar la posición objetivo de la cámara
            _targetCameraPosition = _targetCamera.transform.position;
        }
    }

    private void LateUpdate()
    {
        if (_playerParent == null || _targetCamera == null || _currentSection >= _sections.Length) return;

        UpdateCameraPosition();
        ClampPlayerPosition();
        CheckEnemies();
    }
    #endregion
}
