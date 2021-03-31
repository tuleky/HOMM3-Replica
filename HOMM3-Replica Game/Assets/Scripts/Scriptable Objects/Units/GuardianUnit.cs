using Commands;
using UnityEngine;

namespace Scriptable_Objects.Units
{
	[CreateAssetMenu(fileName = "Guardian Unit", menuName = "Unit/Guardian Unit", order = 0)]
	public class GuardianUnit : Unit
	{
		[SerializeField, Range(0f, 1f)] float _uniqueDefenseBonus;

		public override void TrySpecialDamageTake(AttackCommand attackCommand, int calculatedFinalDamage)
		{
			int receivingDamageAfterDefenseBonus = Mathf.RoundToInt((1f - _uniqueDefenseBonus) * calculatedFinalDamage);
			attackCommand.AttackReceiver.TakeDamage(receivingDamageAfterDefenseBonus);
		}
	}
}