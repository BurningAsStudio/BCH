using System;

namespace BCH.Game.Player
{
	public interface IReadOnlyInputEvents
	{
		public event Action OnFlashlightButtonClicked;
		public event Action OnInteractButtonDown;
		public event Action OnInteractButtonUp;
	}
}