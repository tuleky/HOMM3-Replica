using Commands;
using UnityEngine;

namespace Scriptable_Objects.Units
{
	[CreateAssetMenu(fileName = "Griffin Unit", menuName = "Unit/Griffin Unit", order = 0)]
	public class GriffinUnit : Unit
	{
		[SerializeField, Range(0f, 1f)] float _uniqueAttackBonus;
		
		public override void TrySpecialAttackToTarget(AttackCommand attackCommand)
		{
			int unitDamageMultipliedByAttack = attackCommand.Attacker.AttackPower;
			int dealingDamageAfterAttackBonus = unitDamageMultipliedByAttack + Mathf.RoundToInt(unitDamageMultipliedByAttack * _uniqueAttackBonus);
			
			attackCommand.AttackReceiver.CheckForSpecialDamageTaking(attackCommand, dealingDamageAfterAttackBonus);
		}
	}
}
