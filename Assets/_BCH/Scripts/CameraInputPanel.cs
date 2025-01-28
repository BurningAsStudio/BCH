using UnityEngine.EventSystems;
using UnityEngine;

namespace Zoonormaly
{
    public class CameraInputPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float _maxInputDistance = 50f;
        [SerializeField] private float _rotationSpeed = 0.2f;
        [SerializeField] private Transform _cameraTransform;

        private PointerEventData _currentEventData;
        private Vector2 _startPosition;
        private Vector2 _currentPosition;

        private bool _isInputProcess;

        public bool IsInputProcess => _isInputProcess;

        public void OnPointerDown(PointerEventData eventData)
        {
            _currentEventData = eventData;
            _startPosition = _currentEventData.position;
            _isInputProcess = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isInputProcess = false;
        }

        private void Update()
        {
            if (_isInputProcess)
            {
                UpdateCameraRotation(_currentEventData);
            }
        }

        private void UpdateCameraRotation(PointerEventData eventData)
        {
            _currentPosition = eventData.position;

            var deltaPosition = _currentPosition - _startPosition;

            var distance = deltaPosition.magnitude;
            var normalizedDistance = Mathf.Clamp01(distance / _maxInputDistance);
            var inputVector = deltaPosition * normalizedDistance;

            _cameraTransform.Rotate(0, inputVector.x * _rotationSpeed, 0, Space.World);
            _cameraTransform.localEulerAngles = new Vector3(10, _cameraTransform.localEulerAngles.y, 0);
            _startPosition = _currentPosition;
        }
    }
}