using System;
using System.Collections;
using UnityEngine;

/***
 * Manages the global game state and transitions between levels or stages.
 * 
 * Responsibilities:
 * Manages the game state (Start, Pause, Win, Lose).
 * Controls level transitions and the conditions for progressing from one stage to another.
*/
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    /// <summary>
    /// Initialize class attributes
    /// </summary>
    private void Init()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        Init();
    }

    void Start()
    {
        StartCoroutine(Tick());
        StartCoroutine(FixedUpdateTick());  // L�gica frecuente (movimiento f�sico)
    }

    void Update()
    {

    }

    /// <summary>
    /// Co-rutine for game updates longer than frame by frame
    /// </summary>
    public static Action OnTick1s;
    IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            OnTick1s?.Invoke();
        }
    }

    /// <summary>
    /// Co-rutine to use the same time than fixedUpdate for non-monobehaviour classes (like states)
    /// </summary>
    public static Action OnFixedUpdateTick;

    IEnumerator FixedUpdateTick()
    {
        var wait = new WaitForSeconds(Time.fixedDeltaTime); // ~0.02s por defecto
        while (true)
        {
            yield return wait;
            OnFixedUpdateTick?.Invoke();
        }
    }
}
