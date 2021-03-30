using System.Collections;
using System.Collections.Generic;
using Commands;
using Scriptable_Objects;
using UnityEngine;

public class UnitMono : MonoBehaviour
{
	[SerializeField] Unit _unit;
	
	public Vector2Int GridIndex;
	
	public int MoveSpeed => _unit.UnitConfig.MoveSpeed;
	public int DamageMultipliedByAttack => _unit.UnitConfig.AttackPower * _unit.UnitConfig.DamagePower;
	public int DefensePower => _unit.UnitConfig.DefensePower;
	
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
		
		_unit.TrySpecialAttackToTarget(attackCommand);
	}

	public void CheckForSpecialDamageTaking(AttackCommand attackCommand,int damage)
	{
		_unit.TrySpecialDamageTake(attackCommand, damage);
	}

	public int GetReducedDamageByDefensePower(int damage)
	{
		return damage - DefensePower;
	}
	
	public void TakeDamage(int damage)
	{
		_currentHealth -= damage;
	}

	public IEnumerator MoveCoroutine(MoveCommand moveCommand)
	{
		int lastElementIndex = moveCommand.Path.Count - 1;
		GridPiece targetPoint = moveCommand.Path[lastElementIndex];
		int i = 0;
		while (GridIndex != targetPoint.GridIndex)
		{
			MoveToGrid(moveCommand.Path[i]);
			i++;
			
			yield return new WaitForSeconds(0.2f);
		}
	}

	void MoveToGrid(GridPiece targetGridPiece)
	{
		GridIndex = targetGridPiece.GridIndex;
		transform.position = targetGridPiece.transform.position;
	}
	
	void OnEnable()
	{
		_currentHealth = _unit.UnitConfig.MaxHealth;
	}
}