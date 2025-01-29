using UnityEngine;
using Zenject;
using FS_Atmo;

namespace BCH.Game.Player
{
	public class PlayerInteractor : MonoBehaviour
	{
		[SerializeField] private GameObject _flashlight;
		[SerializeField] private float _rayDistance = 2.3f;

		[Inject] private IReadOnlyInputEvents _inputEvents;
		[Inject] private Camera _mainCamera;
		
		private Transform _cameraTransform;

		private void Awake()
		{
			_cameraTransform = _mainCamera.transform;
		}

		private void OnEnable()
		{
			_inputEvents.OnFlashlightButtonClicked += SwitchFlashlight;
			_inputEvents.OnInteractButtonDown += Interact;
		}
		
		private void OnDisable()
		{
			_inputEvents.OnFlashlightButtonClicked -= SwitchFlashlight;
			_inputEvents.OnInteractButtonDown -= Interact;
		}

		private void Interact()
		{
			RaycastCheck();
		}

		private void SwitchFlashlight()
		{
			if (_flashlight.activeSelf)
				_flashlight.SetActive(false);
			else
				_flashlight.SetActive(true);
		}

		private void RaycastCheck()
        {
	        if (Physics.Raycast(_cameraTransform.position,
		            _cameraTransform.TransformDirection(Vector3.forward), out var hit, _rayDistance))
            {
                if (hit.collider.gameObject.GetComponent<SimpleOpenClose>())
                {
                    // Debug.Log("Object with SimpleOpenClose script found");
                    hit.collider.gameObject.BroadcastMessage("ObjectClicked");
                }
                else
                {
                    // Debug.Log("Object doesn't have script SimpleOpenClose attached");

                }
                // Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                // Debug.Log("Did Hit");
            }
            else
            {
                // Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                //   Debug.Log("Did not Hit");


            }

        }

    }
}