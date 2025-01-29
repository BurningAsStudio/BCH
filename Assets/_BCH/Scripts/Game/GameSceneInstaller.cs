using UnityEngine;
using VInspector;
using Zenject;

namespace BCH.Game
{
	public class GameSceneInstaller : MonoInstaller
	{
		[SerializeField] private Camera _mainCamera;
		
		public override void InstallBindings()
		{
			Container.BindInstance(_mainCamera).AsSingle();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_mainCamera = Camera.main;
		}
#endif
	}
}