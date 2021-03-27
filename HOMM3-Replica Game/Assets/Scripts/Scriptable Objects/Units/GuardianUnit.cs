using UnityEngine;

namespace Scriptable_Objects.Units
{
	[CreateAssetMenu(fileName = "Guardian Unit", menuName = "Unit/Guardian Unit", order = 0)]
	public class GuardianUnit : Unit
	{
		[SerializeField, Range(0f, 1f)] float _uniqueDefenseBonus;
		
		
		public override void TakeSpecialDamageIfPossible(AttackCommand attackCommand, int calculatedFinalDamage)
		{
			int damageAfterDefenseBonus = calculatedFinalDamage / Mathf.Max(1, Mathf.RoundToInt(_uniqueDefenseBonus * calculatedFinalDamage));
			attackCommand.TargetUnit.TakeDamage(damageAfterDefenseBonus);
		}
	}
}