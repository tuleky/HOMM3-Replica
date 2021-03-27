using Scriptable_Objects;
using UnityEngine;
public class BattleController : MonoBehaviour
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
	[ContextMenu("Set All Units To Their Grid")]
	public void SetAllUnitsToTheirGrid()
	{
		foreach (UnitMono unit in BattlingUnitsRunTimeList.Units)
			unit.SetUnitToGrid();
	}

	public void Attack(UnitMono attackerUnitMono, UnitMono takingAttackUnitMono)
	{
		AttackCommand attackCommand = new AttackCommand(attackerUnitMono, takingAttackUnitMono);
		attackCommand.Execute();
		print("sending attack command");
	}

	public void UndoCommand()
	{
		CommandBuffer.Undo();
	}
}