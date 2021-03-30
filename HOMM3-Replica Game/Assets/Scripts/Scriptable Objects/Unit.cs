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
			//attackCommand.Attacker.SendTakeDamageCommand(attackCommand, attackCommand.Attacker.DamageMultipliedByAttack);
			attackCommand.AttackReceiver.CheckForSpecialDamageTaking(attackCommand, attackCommand.Attacker.DamageMultipliedByAttack);
		}

		// This is standard form
		public virtual void TrySpecialDamageTake(AttackCommand attackCommand, int calculatedFinalDamage)
		{
			int finalDamageReducedByDefense = calculatedFinalDamage - attackCommand.AttackReceiver.DefensePower;
			attackCommand.AttackReceiver.TakeDamage(finalDamageReducedByDefense);
		}
	}
}
