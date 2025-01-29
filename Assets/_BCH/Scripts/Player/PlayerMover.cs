using UnityEngine;
using Zenject;

namespace BCH.Game.Player
{
	[RequireComponent(typeof(CharacterController))]
	public class PlayerMover : MonoBehaviour
	{
		[SerializeField] private float _maxSpeed = 4f;
		[SerializeField] private float _gravity = 1500f;
		
		[Inject] private CharacterController _characterController;
		[Inject] private IReadOnlyInput _input;
		[Inject] private Camera _mainCamera;
		
		private Transform _cameraTransform;
		private Vector3 _moveDirection;

		private void Awake()
		{
			_characterController = GetComponent<CharacterController>();
			_cameraTransform = _mainCamera.transform;
		}

		public void Move()
		{
			var moveMagnitude = _input.GetMoveVector().magnitude;
			
			if (moveMagnitude > 0)
			{
				var moveSpeed = moveMagnitude * _maxSpeed;

				var forward = _cameraTransform.forward;
				var right = _cameraTransform.right;

				forward.y = 0;
				right.y = 0;

				forward.Normalize();
				right.Normalize();

				var movementDirectionY = _moveDirection.y;
				_moveDirection = (forward * _input.GetMoveVector().y + right * _input.GetMoveVector().x) * moveSpeed;
				_moveDirection.y = movementDirectionY;
			}
			else
			{
				_moveDirection = new Vector3(0, _moveDirection.y, 0);
			}
			
			if (!_characterController.isGrounded)
				_moveDirection.y -= _gravity * Time.deltaTime;

			_characterController.Move(_moveDirection * Time.deltaTime);	
		}
	}
}