using UnityEngine;
using Zenject;

namespace BCH.Game.Player
{
	public class Player : MonoBehaviour
	{
		[Inject] private PlayerInput _input;
		[Inject] private PlayerMover _mover;

		private void Update()
		{
			_mover.Move();
		}
	}
}