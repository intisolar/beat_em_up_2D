using UnityEngine;

public class StateController
{
    private IState _currentState;
    private float _previousX;
    private bool _hasPrevX;
    private const float RotationEpsilon = 0.02f;

    public StateController(IState initialState)
    {
        _currentState = initialState;
        _currentState.OnEnter();
        _previousX = 0f;
    }

    public void ChangeState(IState newState)
    {
        if (_currentState == newState || newState == null)
        {
            return;
        }
            
        _currentState.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }

    public void UpdateState()
    {
        if (_currentState != null)
        {
            _currentState.OnState();
        }
    }

    private void SetFacing(Transform transform, bool faceRight)
    {
        var rotationEulerAngles = transform.eulerAngles;
        if (faceRight)
        {
            rotationEulerAngles.y = 0f;
        }
        else
        {
            rotationEulerAngles.y = 180f; // visualmente equivale a -180°
        }
        transform.eulerAngles = rotationEulerAngles;
    }

    public void DetectPlayer(EnemyAIController aiController, EnemyCharacter enemy)
    {
        if (!_hasPrevX)
        {
            _previousX = enemy.transform.position.x;
            _hasPrevX = true;
        }

        if (enemy.DetectPlayer(aiController, out var playerTransform))
        {
            bool faceRight = playerTransform.position.x >= enemy.transform.position.x;
            SetFacing(enemy.VisualRoot, faceRight);
            ChangeState(new ChaseState(enemy, playerTransform));
            return;
        }

        if (_currentState is not PatrolState)
        {
            ChangeState(new PatrolState(enemy, 0f, aiController.InitialDirection));
        }

        if (_currentState is PatrolState)
        {
            float deltaX = enemy.transform.position.x - _previousX;
            if (deltaX < -RotationEpsilon)
            {
                SetFacing(enemy.VisualRoot, false); // mirar izquierda
            }
            else if (deltaX > RotationEpsilon)
            {
                SetFacing(enemy.VisualRoot, true); // mirar derecha
            }
        }

        _previousX = enemy.transform.position.x;
    }
}
