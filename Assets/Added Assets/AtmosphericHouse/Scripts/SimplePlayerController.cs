using UnityEngine.InputSystem;
using UnityEngine;

namespace BCH
{
	[RequireComponent(typeof(CharacterController))]
	public class SimplePlayerController : MonoBehaviour
	{
		[SerializeField] private float _maxSpeed = 4f;
		[SerializeField] private float _gravity = 1500f;
		[SerializeField] private InputActionReference _moveAction;
		[SerializeField] private Transform _cameraTransform;

		private CharacterController _characterController;
		private Vector3 _moveDirection;

		private void Awake()
		{
			_characterController = GetComponent<CharacterController>();
		}

		private void OnEnable()
		{
			_moveAction.action.Enable();
		}

		private void OnDisable()
		{
			_moveAction.action.Disable();
		}

		private void Update()
		{
			HandleMovement();
		}

		private void HandleMovement()
		{
			var input = _moveAction.action.ReadValue<Vector2>();
			var moveSpeed = input.magnitude * _maxSpeed;

			var forward = _cameraTransform.forward;
			var right = _cameraTransform.right;

			forward.y = 0;
			right.y = 0;

			forward.Normalize();
			right.Normalize();

			var movementDirectionY = _moveDirection.y;
			_moveDirection = (forward * input.y + right * input.x) * moveSpeed;
			_moveDirection.y = movementDirectionY;

			if (!_characterController.isGrounded)
				_moveDirection.y -= _gravity * Time.deltaTime;

			_characterController.Move(_moveDirection * Time.deltaTime);
		}
	}
}