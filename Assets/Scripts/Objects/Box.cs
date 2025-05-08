using Unity.VisualScripting;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private int _life = 100;
    private float _lastDamageTime;
    [SerializeField] private float _coolDown = 0.5f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack") && Time.time >= _lastDamageTime + _coolDown)
        {
            _life--;
            _lastDamageTime = Time.time;
            
            if (_life <= 0)
            {
                Destroy(gameObject);
            }
        }            
    }
}
