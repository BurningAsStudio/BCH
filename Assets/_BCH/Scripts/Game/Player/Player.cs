using UnityEngine;
using Zenject;

namespace BCH.Game.Player
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private Light _flashlight;

		[Inject] private IReadOnlyInputEvents _inputEvents;
		[Inject] private PlayerInput _input;
		[Inject] private PlayerMover _mover;
		
		private void OnEnable()
		{
			_inputEvents.OnFlashlightButtonClicked += SwitchFlashlight;
		}

		private void OnDisable()
		{
			_inputEvents.OnFlashlightButtonClicked -= SwitchFlashlight;
		}

		private void SwitchFlashlight() => _flashlight.enabled = !_flashlight.enabled;

		private void Update()
		{
			_mover.Move();
		}
	}
}