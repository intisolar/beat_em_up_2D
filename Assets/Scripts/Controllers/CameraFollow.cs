using UnityEngine;

/// <summary>
/// Description: Smoothly follows the active player character.
/// Responsibilities:
/// Ensures the camera always stays focused on the player during gameplay, with smooth transitions for movement.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private bool _canMoveBackward = true;

    [Header("Dependencies")]
    [SerializeField] private Transform _player;

    private float offsetX;
    private float fixedY;
    private float lastX;

    private void Start()
    {
        offsetX = transform.position.x - _player.position.x;
        fixedY = transform.position.y;
        lastX = _player.position.x;
    }

    private void LateUpdate()
    {
        if (_player != null)
        {
            float currentX;
            if (_canMoveBackward)
            {
                currentX = _player.position.x;
            }
            else
            {
                currentX = Mathf.Max(_player.position.x, lastX);
            }

            lastX = currentX;
            transform.position = new Vector3(currentX + offsetX, fixedY, transform.position.z);
        }
    }
}
