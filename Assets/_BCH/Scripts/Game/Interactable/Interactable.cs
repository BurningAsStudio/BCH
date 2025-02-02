using UnityEngine.Events;
using UnityEngine;

namespace BCH.Game
{
	public class Interactable : MonoBehaviour
	{
		[SerializeField] private Transform _interactPoint;
		[SerializeField] private Collider _collider;
		[SerializeField] private UnityEvent _onInteract;
        
		public Transform InteractPoint => _interactPoint;
        
		public void Restore()
		{
			_collider.enabled = true;
		}
        
		public void Interact()
		{
			_collider.enabled = false;
			_onInteract?.Invoke();
		}
	}
}