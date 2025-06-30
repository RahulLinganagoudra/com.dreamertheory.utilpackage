using System;
using UnityEngine;
namespace DT_Util
{
	/// <summary>
	/// Base class for damage-related information.
	/// </summary>
	[Serializable]
	public abstract class DamageInfoBase
	{
		/// <summary>
		/// The GameObject that caused the damage.
		/// </summary>
		public GameObject damageCauser;

		/// <summary>
		/// The amount of damage inflicted.
		/// </summary>
		public int damageAmmount;

		/// <summary>
		/// Indicates whether the damage can be blocked.
		/// </summary>
		public bool canBlock;

		/// <summary>
		/// Indicates whether the damage can be parried.
		/// </summary>
		public bool canParry;

		/// <summary>
		/// Indicates whether the damage can interrupt an action.
		/// </summary>
		public bool canIntrupt;
	}
}