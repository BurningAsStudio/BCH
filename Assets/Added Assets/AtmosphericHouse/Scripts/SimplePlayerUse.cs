using UnityEngine.UI;
using UnityEngine;

namespace FS_Atmo
{

	public class SimplePlayerUse : MonoBehaviour
	{
		[SerializeField] private Button _flashlightButton;
		[SerializeField] private Button _interactButton;
		[SerializeField] private GameObject _mainCamera;
		[SerializeField] private GameObject _flashlight;
		[SerializeField] private float _rayDistance = 2.3f;

		private void OnEnable()
		{
			_flashlightButton.onClick.AddListener(SwitchFlashlight);
			_interactButton.onClick.AddListener(Interact);
		}
		
		private void OnDisable()
		{
			_flashlightButton.onClick.AddListener(SwitchFlashlight);
			_interactButton.onClick.AddListener(Interact);
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
	        if (Physics.Raycast(_mainCamera.transform.position,
		            _mainCamera.transform.TransformDirection(Vector3.forward), out var hit, _rayDistance))
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