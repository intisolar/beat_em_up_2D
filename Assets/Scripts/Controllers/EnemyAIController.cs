using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAIController : MonoBehaviour
{
    public StateController stateController;
    private EnemyCharacter _owner;
    private Rigidbody2D _rigidbody;


    public EnemyCharacter Owner { get => _owner; private set => _owner = value; }


    private void Awake()
    {
        Owner = gameObject.GetComponent<EnemyCharacter>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody2D is missing in " + name);
        }

        // random delay
        float offset = Random.Range(Owner.InitialDelayMin, Owner.InitialDelayMax);

        stateController = new StateController(new PatrolState(Owner, offset, Owner.InitialDirection));
        SinchronizeEventsToOnTick();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        stateController.UpdateState();
    }

    /// <summary>
    /// player sensor logic
    /// </summary>
    private void DetectPlayer()
    {
        stateController.DetectPlayer(Owner);
    }


    private void SinchronizeEventsToOnTick()
    {
        GameManager.OnTick += DetectPlayer; //suscribed to timer instead of updating frame by frame
        GameManager.OnTick += stateController.OnStateTick;
    }
}
