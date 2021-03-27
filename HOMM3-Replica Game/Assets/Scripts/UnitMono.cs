using System.Collections.Generic;
using Scriptable_Objects;
using UnityEngine;

public class UnitMono : MonoBehaviour
{
	[SerializeField] Unit _unit;
	
	public Vector2Int GridIndex;
	
	public int MoveSpeed => _unit.UnitConfig.MoveSpeed;
	public int DamageMultipliedByAttack => _unit.UnitConfig.AttackPower * _unit.UnitConfig.DamagePower;

	int _currentHealth;
	
	[ContextMenu("Set Unit To Grid")]
	public void SetUnitToGrid()
	{
		GridPiece targetGridPiece = GridPiece.GridPieces[GridIndex];
		targetGridPiece.SetUnitToGrid(this);
		transform.position = targetGridPiece.transform.position;
	}

	/// <summary>
	/// This method is called by command
	/// </summary>
	/// <param name="attackCommand"></param>
	public void CheckForSpecialAttack(AttackCommand attackCommand)
	{
		// check whether we have a special ability in this move
		// then if we have one use with that special power
		
		_unit.AttackSpecialIfPossibleToTarget(attackCommand);
	}
	
	void Defense()
	{
		
	}
	
	public void CheckForSpecialDamageTaking(AttackCommand attackCommand,int damage)
	{
		_unit.TakeSpecialDamageIfPossible(attackCommand, damage);
	}

	public void TakeDamage(int damage)
	{
		_currentHealth -= damage;
	}
	
	void Move()
	{
		
	}

	void OnEnable()
	{
		_currentHealth = _unit.UnitConfig.MaxHealth;
	}
}