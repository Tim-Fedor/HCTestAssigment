using UnityEngine;

namespace Resource
{
    public class ResourceObject : MonoBehaviour
    {
        public delegate void OnStateChange(ResourceObject resource);

        [SerializeField] 
        private float _movingSpeed;
        [SerializeField] 
        private float _rotateSpeed;
        [SerializeField] 
        private ResourceType _type;
        [SerializeField] 
        private Collider _collider;

        private bool _isMoving;

        private Vector3 _targetDirection;
        private Vector3 _targetPoint;
        
        public ResourceState State { get; private set; }

        public ResourceType Type => _type;

        private void Update()
        {
            if (!_isMoving)
            {
                return;
            }

            transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPoint, _movingSpeed * Time.deltaTime);
            if (_targetDirection != Vector3.zero)
            {
                transform.localEulerAngles = Vector3.Lerp(transform.localRotation.eulerAngles, _targetDirection, _rotateSpeed);
                if (Vector3.Distance(transform.rotation.eulerAngles, _targetDirection) == 0f)
                {
                    _targetDirection = Vector3.zero;
                }
            }

            if (Vector3.Distance(_targetPoint, transform.localPosition) < 0.01f)
            {
                _isMoving = false;
                _collider.enabled = true;
                if (State == ResourceState.Stay)
                {
                    Destroy(gameObject);
                }
            }
        }

        public event OnStateChange StateChanged;

        public bool MoveToPoint(Vector3 target, ResourceState newState)
        {
            if (_isMoving)
            {
                return false;
            }

            State = newState;
            StateChanged?.Invoke(this);
            _targetPoint = target;
            _targetDirection = Vector3.zero;
            _isMoving = true;
            _collider.enabled = false;
            return true;
        }

        public bool MoveToPoint(Vector3 target, ResourceState newState, Vector3 turnTo)
        {
            if (_isMoving)
            {
                return false;
            }

            MoveToPoint(target, newState);
            _targetDirection = turnTo;
            return true;
        }
    }
}