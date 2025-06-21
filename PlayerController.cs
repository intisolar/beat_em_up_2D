using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxYPosition; // Límite máximo en Y configurable desde el inspector

    private void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.position += movement * moveSpeed * Time.deltaTime;

        // Verificación del límite máximo en Y
        if (transform.position.y > maxYPosition)
        {
            transform.position = new Vector3(transform.position.x, maxYPosition, transform.position.z);
        }
    }
}