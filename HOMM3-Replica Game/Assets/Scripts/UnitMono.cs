using System.Collections;
using Commands;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using Scriptable_Objects;
using UnityEngine;

public class UnitMono : MonoBehaviour
{
	public NetworkVariableVector2 Position = new NetworkVariableVector2(new NetworkVariableSettings
	{
		WritePermission = NetworkVariablePermission.ServerOnly,
		ReadPermission = NetworkVariablePermission.Everyone
	});
	
	[SerializeField] Unit _unit;
	[SerializeField] UnitUIController _unitUIController;
	
	public Vector2Int GridIndex;
	
	public int MoveSpeed => _unit.UnitConfig.MoveSpeed;
	public int AttackPower => _unit.UnitConfig.AttackPower;
	
	int _currentHealth;
	
	public void SetUnitToGrid()
	{
		GridPiece targetGridPiece = GridPiece.GridPieces[GridIndex];
		targetGridPiece.SetUnitToGrid(this);
		SetPosition(targetGridPiece);
	}
	void SetPosition(GridPiece targetGridPiece)
	{
		if (NetworkManager.Singleton.IsServer)
		{
			
		}
		var position = targetGridPiece.transform.position;
		Position.Value = position;
		transform.position = position;
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

	public void TakeDamage(int damage)
	{
		_currentHealth -= damage;
		_unitUIController.UpdateHealthText(_currentHealth);
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

		if (NetworkManager.Singleton.IsServer)
		{
			transform.position = targetGridPiece.transform.position;
			Position.Value = targetGridPiece.transform.position;
		}
		else
		{
			SubmitPositionRequestServerRpc(targetGridPiece.transform.position);
		}
	}

	[ServerRpc]
	void SubmitPositionRequestServerRpc(Vector3 newPosition)
	{
		Position.Value = newPosition;
		transform.position = Position.Value;
	}
	
	void OnEnable()
	{
		_currentHealth = _unit.UnitConfig.MaxHealth;
		_unitUIController.UpdateHealthText(_currentHealth);
		_unitUIController.UpdateDamagePowerText(AttackPower);
	}
}