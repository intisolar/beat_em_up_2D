using UnityEngine;

namespace Handlers
{
    public abstract class AttackHitBox : MonoBehaviour
    {
        [SerializeField] protected byte _attackPower = 1;
        private GameObject _owner;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void SetAttackPower(byte attackPower)
        {
            _attackPower = attackPower;
        }

        public void SetOwner(GameObject owner)
        {
            _owner = owner;
        }
    }
}
