using UnityEngine;

public class PatrolState : IState
{
    private enum SubState { WaitingStart, Moving, Idle }

    private readonly EnemyCharacter character;

    private Vector2 currentDirection;
    private float timer;
    private SubState subState;

    private readonly float initialDelay;
    private readonly float waitTime = 3f;  // how much time it must wait
    private readonly float moveTime = 3f; // how much time it must move on a direction

    public PatrolState(EnemyCharacter character, float initialDelay, Vector2 initialDirection)
    {
        Debug.Log(" In PatrolState");
        this.character = character;
        this.initialDelay = initialDelay;
        if (initialDirection == null)
        {
            currentDirection = Vector3.left;
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
                Move();
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
        Vector2 pos = character.Rigidbody.position
    + currentDirection
      * Time.deltaTime
      * character.MoveSpeed;
        character.Rigidbody.MovePosition(pos);
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


}
