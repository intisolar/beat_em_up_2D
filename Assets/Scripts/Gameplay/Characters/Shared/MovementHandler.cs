using UnityEngine;

namespace Handlers
{
    public class MovementHandler
    {
        private readonly Rigidbody _rigidbody;
        private readonly float _speed;
        private readonly float _minPosition;
        private readonly float _maxPosition;

        public MovementHandler(Rigidbody rigidbody, float speed, float minPosition, float maxPosition)
        {
            _rigidbody = rigidbody;
            _speed = speed;
            _minPosition = minPosition;
            _maxPosition = maxPosition;
        }

        public Vector3 CalculateVelocity(Vector2 input, Vector3 currentPosition, Vector3 lastPosition, bool canMoveY)
        {
            float zVelocity = input.y * _speed;
            float clampedZ = Mathf.Clamp(currentPosition.z + zVelocity * Time.fixedDeltaTime, _minPosition, _maxPosition);
            float clampedY = CalculateClampedY(currentPosition, lastPosition, canMoveY);
            float verticalVelocity = (clampedY - currentPosition.y) / Time.fixedDeltaTime;

            return new Vector3(input.x * _speed, verticalVelocity, (clampedZ - currentPosition.z) / Time.fixedDeltaTime);
        }

        private float CalculateClampedY(Vector3 currentPosition, Vector3 lastPosition, bool canMoveY)
        {
            if (canMoveY)
            {
                float deltaZ = currentPosition.z - lastPosition.z;
                float deltaY = deltaZ * (_maxPosition - _minPosition) / (_maxPosition - _minPosition);
                return Mathf.Clamp(currentPosition.y + deltaY, _minPosition, _maxPosition);
            }

            return currentPosition.y;
        }

        public void HandleIdleState(Vector3 currentPosition)
        {
            float clampedY = Mathf.Clamp(currentPosition.z, _minPosition, _maxPosition);
            _rigidbody.position = new Vector3(currentPosition.x, clampedY, currentPosition.z);
        }
    }
}
