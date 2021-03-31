using Commands;
using UnityEngine;

namespace Scriptable_Objects
{
	public abstract class Unit : ScriptableObject
	{
		public UnitConfig UnitConfig;

		// This is standard form
		public virtual void TrySpecialAttackToTarget(AttackCommand attackCommand)
		{
			attackCommand.AttackReceiver.CheckForSpecialDamageTaking(attackCommand, attackCommand.Attacker.AttackPower);
		}

		// This is standard form
		public virtual void TrySpecialDamageTake(AttackCommand attackCommand, int calculatedFinalDamage)
		{
			attackCommand.AttackReceiver.TakeDamage(calculatedFinalDamage);
		}
	}
}
