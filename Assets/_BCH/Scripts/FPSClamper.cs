using UnityEngine;

namespace BCH
{
	public class FPSClamper : MonoBehaviour
    {
		[SerializeField] private int _targetFPS = 60;
		
		private void Start()
		{
			Application.targetFrameRate = _targetFPS;
		}
    }
}