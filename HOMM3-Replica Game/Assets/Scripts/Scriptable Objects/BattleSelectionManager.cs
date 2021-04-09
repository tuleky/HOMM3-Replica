using System.Collections.Generic;
using MLAPI.Messaging;
using UnityEngine;

namespace Scriptable_Objects
{
	[CreateAssetMenu(fileName = "Battle Selection Manager", menuName = "Battle Selection Manager", order = 0)]
	public class BattleSelectionManager : ScriptableObject
	{
		[SerializeField] InfoPanelSO _infoPanelSO;
		[SerializeField] GridPieceHighlightEffectSO _gridPieceHighlightEffectSO;

		List<GridPiece> _moveableGridPieces = new List<GridPiece>();
		GridPiece _lastSelectedGridPiece;

		BattleController _battleController;
		
		public void Initialize(BattleController battleController)
		{
			_battleController = battleController;
		}

		public void OnGridPieceSelected(GridPiece selectedGridPiece)
		{
			UnitMono lastSelectedUnitMono = null;
			if (_lastSelectedGridPiece)
			{
				_lastSelectedGridPiece.SetMaterialToDefault();
				lastSelectedUnitMono = _lastSelectedGridPiece.UnitMonoOnTopOfGrid;
			}
			
			if (selectedGridPiece.HasUnit())
			{
				if (lastSelectedUnitMono && lastSelectedUnitMono != selectedGridPiece.UnitMonoOnTopOfGrid && _moveableGridPieces.Contains(selectedGridPiece))
				{
					// attack command
					SendAttackCommandToBattleController(selectedGridPiece.UnitMonoOnTopOfGrid);
					ResetAllSelectionStates();
					return;
				}
				else
				{
					ResetAllSelectionStates();
					UnitSelectionServerRpc(selectedGridPiece);
				}
			}
			else
			{
				if (lastSelectedUnitMono && _moveableGridPieces.Contains(selectedGridPiece))
				{
					// move command
					SendMoveCommandToBattleControllerServerRpc(selectedGridPiece);
					ResetAllSelectionStates();
					return;
				}
				else
				{
					// show info about land
					ResetAllSelectionStates();
					GridSelection(selectedGridPiece);
				}
			}
			
			_lastSelectedGridPiece = selectedGridPiece;
		}
		
		void GridSelection(GridPiece selectedGridPiece)
		{
			_infoPanelSO.ShowGridPieceInfo(selectedGridPiece);
			selectedGridPiece.SetGridMaterialToHighlighted(_gridPieceHighlightEffectSO);
		}
		
		[ServerRpc]
		void UnitSelectionServerRpc(GridPiece selectedGridPiece)
		{
			// show info about unit
			//_infoPanelSO.ShowUnitInfo(selectedGridPiece.UnitMonoOnTopOfGrid);

			_moveableGridPieces = PathFinder.CalculateMoveablePaths(selectedGridPiece.UnitMonoOnTopOfGrid);
			GridExtension.SetAllGridsToHighlighted(_moveableGridPieces, _gridPieceHighlightEffectSO);
		}
		
		public void ResetAllSelectionStates()
		{
			if (_moveableGridPieces.Count > 0)
			{
				foreach (GridPiece gridPiece in _moveableGridPieces)
				{
					gridPiece.SetMaterialToDefault();
				}
				
				_moveableGridPieces.Clear();
			}
			_lastSelectedGridPiece = null;
		}
		
		void SendAttackCommandToBattleController(UnitMono attackReceiver)
		{
			_battleController.ExecuteAttack(_lastSelectedGridPiece.UnitMonoOnTopOfGrid, attackReceiver);
		}
		
		[ServerRpc]
		void SendMoveCommandToBattleControllerServerRpc(GridPiece targetGridPiece)
		{
			List<GridPiece> pathToTargetGridPiece = PathFinder.GetPathToTargetGridPiece(_lastSelectedGridPiece, targetGridPiece);
			_battleController.ExecuteMove(_lastSelectedGridPiece.UnitMonoOnTopOfGrid, pathToTargetGridPiece);
			
			GridPieceUnitStateUpdate(targetGridPiece);
		}
		
		void GridPieceUnitStateUpdate(GridPiece targetGridPiece)
		{
			targetGridPiece.SetUnitToGrid(_lastSelectedGridPiece.UnitMonoOnTopOfGrid);
			_lastSelectedGridPiece.SetUnitToGrid(null);
		}
	}
}