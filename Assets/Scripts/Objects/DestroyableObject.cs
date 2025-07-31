using UnityEngine;

public abstract class DestroyableObject : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] protected int _currentLife = 10;
    private int _maxLife;
    
    [Header("Animation")]
    [SerializeField] protected Animator _animator;
    protected string _animationPrefix;
    [SerializeField] protected byte _stateCount;

    private void Awake()
    {
        _maxLife = _currentLife;

        if (_animator != null)
        {
            _animator.enabled = false;
        }
    }

    public virtual void TakeDamage(byte amount, Transform attackerTransform)
    {
        _currentLife = UpdateHealth(_currentLife, amount);

        if (_animator != null)
        {
            _animator.enabled = false;
        }

        if (_currentLife <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            if (_animator != null)
            {
                _animator.enabled = true;
            }
            UpdateAnimationFrame();
        }
    }

    public int UpdateHealth(int currentHealth, int damageTaken)
    {
        return currentHealth - damageTaken;
    }

    protected void UpdateAnimationFrame()
    {
        if (_animator == null) return;

        int lifePerState = Mathf.CeilToInt((float)_maxLife / _stateCount);
        int stateIndex = Mathf.Clamp((_maxLife - _currentLife) / lifePerState, 0, _stateCount - 1);
        string stateName = $"{_animationPrefix}_State_{stateIndex}";

        if (_animator.HasState(0, Animator.StringToHash(stateName)))
        {
            _animator.Play(stateName);
        }
    }
}
