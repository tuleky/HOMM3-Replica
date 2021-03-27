using UnityEngine;

namespace Scriptable_Objects
{
	public abstract class Unit : ScriptableObject
	{
		public UnitConfig UnitConfig;

		// This is standard form
		public virtual void AttackSpecialIfPossibleToTarget(AttackCommand attackCommand)
		{
			//attackCommand.Attacker.SendTakeDamageCommand(attackCommand, attackCommand.Attacker.DamageMultipliedByAttack);
			attackCommand.TargetUnit.CheckForSpecialDamageTaking(attackCommand, attackCommand.Attacker.DamageMultipliedByAttack);
		}

		// This is standard form
		public virtual void TakeSpecialDamageIfPossible(AttackCommand attackCommand, int calculatedFinalDamage)
		{
			attackCommand.TargetUnit.TakeDamage(calculatedFinalDamage);
		}
	}
}
