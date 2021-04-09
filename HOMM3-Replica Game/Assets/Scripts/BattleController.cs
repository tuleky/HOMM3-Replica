using System.Collections.Generic;
using Commands;
using MLAPI;
using MLAPI.Messaging;
using Scriptable_Objects;
using UnityEngine;

public class BattleController : NetworkBehaviour
{
	[SerializeField] GridPiecePickerSO _gridPiecePickerSO;
	[SerializeField] BattleSelectionManager _battleSelectionManager;
	
	void Awake()
	{
		_battleSelectionManager.Initialize(this);
	}

	void Update()
	{
		_gridPiecePickerSO.OnUpdate();
	}

	// When strategy turn ends, set all of the units onto the grid they stand
	[ContextMenu("Set All Units To Their Grid"), ServerRpc]
	public void SetAllUnitsToTheirGridServerRpc()
	{
		SetAllUnitsToGridClientRpc();
	}

	[ClientRpc]
	void SetAllUnitsToGridClientRpc()
	{
		foreach (UnitMono unit in BattlingUnitsRunTimeList.Units)
			unit.SetUnitToGrid();
	}
	
	public void ExecuteAttack(UnitMono attackerUnitMono, UnitMono takingAttackUnitMono)
	{
		AttackCommand attackCommand = new AttackCommand(attackerUnitMono, takingAttackUnitMono);
		attackCommand.Execute();
		print("executing attack command");
	}

	public void ExecuteMove(UnitMono movingUnit, List<GridPiece> pathToTargetGridPiece)
	{
		MoveCommand moveCommand = new MoveCommand(movingUnit, pathToTargetGridPiece);
		moveCommand.Execute();
		print("executing move command");
	}
	
	public void UndoCommand()
	{
		CommandBuffer.Undo();
	}
}