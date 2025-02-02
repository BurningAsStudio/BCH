using UnityEngine.InputSystem;
using UnityEngine;
using System;
using Zenject;

namespace BCH.Game.Player
{
	public class PlayerInput : MonoBehaviour, IReadOnlyInput, IInputControl
	{
		[SerializeField] private PlayerInputView _view;
		[SerializeField] private InputActionReference _moveAction;

		[Inject] private PlayerInputEvents _events;
		
		public bool IsMovementJoystickActive { get; private set; } = true;
		public bool IsButtonsInputActive { get; private set; } = true;

		private void OnEnable()
		{
			_view.FlashlightButton.OnClick.AddListener(OnFlashlightButtonClicked);
			_view.InteractButton.OnButtonDown.AddListener(OnInteractButtonDown);
			_view.InteractButton.OnButtonUp.AddListener(OnInteractButtonUp);
			
			_moveAction.action.Enable();
		}

		private void OnDisable()
		{
			_view.FlashlightButton.OnClick.RemoveListener(OnFlashlightButtonClicked);
			_view.InteractButton.OnButtonDown.RemoveListener(OnInteractButtonDown);
			_view.InteractButton.OnButtonUp.RemoveListener(OnInteractButtonUp);
			
			_moveAction.action.Disable();
		}
		
		public void HideInteractButton()
		{
			if (_view.InteractButton.IsHold)
				OnInteractButtonUp();
			
			_view.InteractButton.gameObject.SetActive(false);
		}

		public void ShowInteractButton()
		{
			_view.InteractButton.gameObject.SetActive(true);
		}
		
		private void OnFlashlightButtonClicked() => TrySendOnButtonClickEvent(_events.InvokeOnFlashlightButtonClicked);
		
		private void OnInteractButtonDown() => TrySendOnButtonClickEvent(_events.InvokeOnInteractButtonDown);
		
		private void OnInteractButtonUp() => TrySendOnButtonClickEvent(_events.InvokeOnInteractButtonUp);

		private void TrySendOnButtonClickEvent(Action action)
		{
			if (IsButtonsInputActive)
				action?.Invoke();
		}
		
		public Vector2 GetMoveVector()
		{
			var moveVector = Vector2.zero;
			
			if (IsMovementJoystickActive)
				moveVector = _moveAction.action.ReadValue<Vector2>();
			
			return moveVector;
		}

		public void DisableMovementInput() => IsMovementJoystickActive = false;
		
		public void EnableMovementInput() => IsMovementJoystickActive = true;
		
		public void DisableButtonsInput() => IsButtonsInputActive = false;
		
		public void EnableButtonsInput() => IsButtonsInputActive = true;
	}
}