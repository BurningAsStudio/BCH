namespace BCH.Game.Player
{
	public interface IInputControl
	{
		public void DisableMovementInput();
		public void EnableMovementInput();
		public void DisableButtonsInput();
		public void EnableButtonsInput();
	}
}