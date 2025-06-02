using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public StateController stateController;

    private void Awake()
    {
        stateController = new StateController(new PatrolState());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        DetectPlayer();
        stateController.UpdateState();
    }

    /// <summary>
    /// player sensor logic
    /// </summary>
    private void DetectPlayer()
    {

    }
}
