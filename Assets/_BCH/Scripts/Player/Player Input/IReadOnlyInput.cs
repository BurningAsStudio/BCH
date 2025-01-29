using UnityEngine;

namespace BCH.Game.Player
{
	public interface IReadOnlyInput
	{
		public bool IsMovementJoystickActive { get; }
		public bool IsButtonsInputActive { get; }
		
		public Vector2 GetMoveVector();
	}
}