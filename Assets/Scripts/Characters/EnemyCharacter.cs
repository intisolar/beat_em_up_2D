using UnityEngine;

/***
 * Represents the playable characters.
 * 
 * It manages generic behavior of enemy subclasses
 ***/
public abstract class EnemyCharacter : CharacterBase, IOnGuard, IGenerous
{
    public Rigidbody2D Rigidbody { get; private set; }

    [Header("Patrol and Detect")]
    [SerializeField] private float initialDelayMin = 0f;
    [SerializeField] private float initialDelayMax = 3f;
    [SerializeField] private float visionRadius = 1f;
    [SerializeField] private Vector2 initialDirection;

    public float VisionRadius { get => visionRadius; private set => visionRadius = value; }
    public float InitialDelayMin { get => initialDelayMin; set => initialDelayMin = value; }
    public float InitialDelayMax { get => initialDelayMax; set => initialDelayMax = value; }
    public Vector2 InitialDirection { get => initialDirection; set => initialDirection = value; }

    protected override void Awake()
    {
        base.Awake();
        Rigidbody = GetComponent<Rigidbody2D>();
    }
    
    public bool isLucky()
    {
        return false;
    }

    public void OnDrop()
    {

    }

    /// <summary>
    /// Cómo hago para que se mueva por determinado tiempo y pare? o pare si colisiona con algo y se detenga unos segundos y arranque en dirección contraria? 
    /// </summary>
    /// <param name="direction"></param>
    public void Patrol(string direction)
    {

    }

    public override bool PerformAttack()
    {
        throw new System.NotImplementedException();
    }

    private void OnEnable()
    {
      
    }
}
