using UnityEngine;
using System;
using BCH.UI;

namespace BCH.Game.Player
{
	[Serializable]
	public class PlayerInputView
	{
		[field: SerializeField] public CustomButton InteractButton { get; private set; }
		[field: SerializeField] public CustomButton FlashlightButton { get; private set; }
	}
}