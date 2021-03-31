using UnityEngine;

namespace Scriptable_Objects
{
	[CreateAssetMenu(menuName = "Unit/Create New Unit Config")]
	public class UnitConfig : ScriptableObject
	{
		public int AttackPower;
		public int MaxHealth;
		public int MoveSpeed;
		public int CostPerUnit;
		public int Growth;
	
		public int Tier;
		public bool IsUpgraded;
	
	
		[Space(30f)]
		
		// attack types: melee, ranged, support (can't attack)
		public AttackType AttackType;
		// if ranged then this will get new features such as ammo and range penalty
		[Header("If Attack Type is RANGED")]
		public int Ammo;
		public int RangePenalty;
		
		[Space(30f)]
	
		public Faction Faction;
		public SpecialUnitFeature SpecialUnitFeature;
		public Dwelling Dwelling;
		public Sprite UnitPicture;
	}
}