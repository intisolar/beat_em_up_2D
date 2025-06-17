using UnityEngine;

public class PatrolState : IState
{
    private enum SubState { WaitingStart, Moving, Idle }

    private readonly EnemyCharacter enemy;

    private Vector2 currentDirection;
    private float timer;
    private SubState subState;

    private readonly float initialDelay;
    private readonly float waitTime = 3f;  // how much time it must wait
    private readonly float moveTime = 3f; // how much time it must move on a direction

    public PatrolState(EnemyCharacter character, float initialDelay, Vector2 initialDirection)
    {
        this.enemy = character;
        Debug.Log(enemy.name + " In PatrolState");
        this.initialDelay = initialDelay;
        if (initialDirection == Vector2.zero)
        {
            currentDirection = Vector2.left;
        }
        else
        {
            currentDirection = initialDirection;
        }
        OnEnter();
    }

    public void OnEnter()
    {
        ResetTimer();

        if (initialDelay > 0f)
        {
            subState = SubState.WaitingStart;
        }
        else
        {
            subState = SubState.Moving;
        }

    }

    public void OnExit()
    {
        //reset or clean as needed
    }

    public void OnTick()
    {
        timer++;
    }

    public void OnState()
    {
        // Update timer every time the state gets updated

        switch (subState)
        {
            case SubState.WaitingStart:
                // wait for initial offset 
                if (timer >= initialDelay)
                {
                    ResetTimer();
                    subState = SubState.Moving;
                }
                break;

            case SubState.Moving:
                
                //if it has moved enough 
                if (timer >= moveTime)
                {
                    subState = SubState.Idle;
                    ResetTimer();
                }
                break;

            case SubState.Idle:
                // Do nothing until time's up
                if (timer >= waitTime)
                {
                    ChangeDirection();
                    subState = SubState.Moving;
                    ResetTimer();
                }
                break;
            default:
                throw new System.Exception(" PatrolState.Substate not recongnized.");
        }
    }

    private void Move()
    {
        if (enemy.MoveSpeed == -1f)
        {
            throw new System.Exception(" Careful, _moveSpeed is null in" + enemy.name);
        }
        Vector2 newPosition = enemy.Rigidbody.position
            + currentDirection
            * Time.fixedDeltaTime
            * enemy.MoveSpeed;

        enemy.Rigidbody.MovePosition(newPosition);
    }

    private void ChangeDirection()
    {
        currentDirection = (currentDirection == Vector2.left)
            ? Vector2.right
            : Vector2.left;
    }

    private void ResetTimer()
    {
        timer = 0f;
    }

    public void OnFixedUpdateTick()
    {
        if (subState == SubState.Moving)
            Move(); // it only moves in fixedUpdate time
    }
}
