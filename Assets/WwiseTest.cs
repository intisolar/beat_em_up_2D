using UnityEngine;

public class WwiseTest : MonoBehaviour
{
    public AK.Wwise.Event testEvent; // arrastr� aqu� Play_MenuMusic

    void Start()
    {
        testEvent.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
