using UnityEngine;
using BCH.UI;

namespace BCH.Game.Player
{
	public class PlayerInteractionView : UIInteractableScreen
	{
		[SerializeField] private RectTransform _interactPoint;

		public void UpdateInteractPointPosition(Vector3 position) => _interactPoint.position = position;
	}
}