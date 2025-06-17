using UnityEngine;

public interface IState
{
    void OnEnter();  // runs when the state is entered
    void OnState(); // runs each frame while the state is active
    void OnExit(); // runs when exiting the state
    void OnTick();
    void OnFixedUpdateTick(); //runs with co-routine simulating fixedUpdate execution
}
