using UnityEngine;
using VInspector;
using Zenject;

namespace BCH.Game.Player
{
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(PlayerInput))]
	[RequireComponent(typeof(PlayerMover))]
	[RequireComponent(typeof(Player))]
	public class PlayerInstaller : MonoInstaller
	{
		[SerializeField] private CharacterController _characterController;
		[SerializeField] private PlayerInput _input;
		[SerializeField] private PlayerMover _mover;
		[SerializeField] private Player _player;
		
		private readonly PlayerInputEvents _events = new ();
		
		public override void InstallBindings()
		{
			Container.Bind<IReadOnlyInputEvents>().FromInstance(_events).AsSingle();
			Container.Bind<IReadOnlyInput>().FromInstance(_input).AsSingle();
			Container.Bind<IInputControl>().FromInstance(_input).AsSingle();
			
			Container.BindInstance(_events).WhenInjectedIntoInstance(_input);
			Container.BindInstance(_input).WhenInjectedIntoInstance(_player);
			Container.BindInstance(_mover).WhenInjectedIntoInstance(_player);
			
			Container.BindInstance(_characterController).AsSingle();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_characterController = GetComponentInChildren<CharacterController>(true);
			_mover = GetComponentInChildren<PlayerMover>(true);
			_input = GetComponentInChildren<PlayerInput>(true);
			_player = GetComponentInChildren<Player>(true);
		}
#endif
	}
}