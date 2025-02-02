using UnityEngine;
using Zenject;

namespace BCH.Game.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private PlayerInteractionView _view;
        [SerializeField] private LayerMask _interactionLayer;
        [SerializeField] private float _maxInteractionDistance = 2f;

        [Inject] private IReadOnlyInputEvents _inputEvents;
        [Inject] private Camera _mainCamera;
        
        private Interactable _currentInteractable;
        
        private Vector3 _screenCenter;

        private void Awake()
        {
	        _screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        }

        private void OnEnable()
        {
	        _inputEvents.OnInteractButtonDown += TryInteract;
        }

        private void OnDisable()
        {
	        _inputEvents.OnInteractButtonDown -= TryInteract;
        }

        private void TryInteract()
        {
            if (_currentInteractable != null)
                Interact();
        }

        private void Interact()
        {
            _currentInteractable.Interact();
            _view.Hide();
            _currentInteractable = null;
        }
        
        private void CheckInteractionRay()
        {
            var ray = _mainCamera.ScreenPointToRay(_screenCenter);
            
            if (Physics.Raycast(ray, out var hit, _maxInteractionDistance, _interactionLayer))
            {
                if (hit.transform.TryGetComponent(out Interactable interactable))
                {
                    _currentInteractable = interactable;
                    _view.Show();
                }
            }
            else if (_currentInteractable != null)
            {
                _view.Hide();
                _currentInteractable = null;
            }
        }
        
        private void Update()
        {
	        CheckInteractionRay();
        }

        private void LateUpdate()
        {
            if (_currentInteractable != null)
                _view.UpdateInteractPointPosition(
                    _mainCamera.WorldToScreenPoint(_currentInteractable.InteractPoint.position));
        }
    }
}