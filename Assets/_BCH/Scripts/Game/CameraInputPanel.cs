using UnityEngine.EventSystems;
using UnityEngine;

namespace Zoonormaly
{
    public class CameraInputPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float _maxInputDistance = 50f;
        [SerializeField] private float _horizontalRotationSpeed = 0.2f;
        [SerializeField] private float _verticalRotationSpeed = 0.01f;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _minVerticalAngle = -30f;
        [SerializeField] private float _maxVerticalAngle = 60f;

        private PointerEventData _currentEventData;
        private Vector2 _startPosition;
        private Vector2 _currentPosition;

        private bool _isInputProcess;
        private float _currentVerticalAngle;

        public void OnPointerDown(PointerEventData eventData)
        {
            _currentEventData = eventData;
            _startPosition = _currentEventData.position;
            _isInputProcess = true;
            _currentVerticalAngle = _cameraTransform.localEulerAngles.x;
            if (_currentVerticalAngle > 180f) _currentVerticalAngle -= 360f;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isInputProcess = false;
        }

        private void UpdateCameraRotation(PointerEventData eventData)
        {
            _currentPosition = eventData.position;

            var deltaPosition = _currentPosition - _startPosition;

            var distance = deltaPosition.magnitude;
            var normalizedDistance = Mathf.Clamp01(distance / _maxInputDistance);
            var inputVector = deltaPosition * normalizedDistance;

            _cameraTransform.Rotate(0, inputVector.x * _horizontalRotationSpeed, 0, Space.World);

            _currentVerticalAngle -= inputVector.y * _verticalRotationSpeed;
            _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, _minVerticalAngle, _maxVerticalAngle);
            _cameraTransform.localEulerAngles = new Vector3(_currentVerticalAngle, _cameraTransform.localEulerAngles.y, 0);

            _startPosition = _currentPosition;
        }

        private void Update()
        {
            if (_isInputProcess)
                UpdateCameraRotation(_currentEventData);
        }
    }
}