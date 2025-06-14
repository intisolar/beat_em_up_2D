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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Tick());
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Co-rutine for game updates longer than frame by frame
    /// </summary>
    public static Action OnTick;
    IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            OnTick?.Invoke();
        }
    }

}
