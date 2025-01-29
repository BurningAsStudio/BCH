using System;

namespace BCH.Game.Player
{
	public class PlayerInputEvents : IReadOnlyInputEvents
	{
		public event Action OnFlashlightButtonClicked;
		public event Action OnInteractButtonDown;
		public event Action OnInteractButtonUp;

		public void InvokeOnFlashlightButtonClicked() => OnFlashlightButtonClicked?.Invoke();
		
		public void InvokeOnInteractButtonDown() => OnInteractButtonDown?.Invoke();
		
		public void InvokeOnInteractButtonUp() => OnInteractButtonUp?.Invoke();
	}
}