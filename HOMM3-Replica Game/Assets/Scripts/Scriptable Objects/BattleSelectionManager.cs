using System.Collections.Generic;
using UnityEngine;

namespace Scriptable_Objects
{
	[CreateAssetMenu(fileName = "Battle Selection Manager", menuName = "Battle Selection Manager", order = 0)]
	public class BattleSelectionManager : ScriptableObject
	{
		[SerializeField] InfoPanelSO _infoPanelSO;
		[SerializeField] GridPieceHighlightEffectSO _gridPieceHighlightEffectSO;
		
		
		readonly List<GridPiece> _moveableGridPieces = new List<GridPiece>();
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
				if (lastSelectedUnitMono && lastSelectedUnitMono != selectedGridPiece.UnitMonoOnTopOfGrid)
				{
					// attack command
					SendAttackCommandToBattleController(selectedGridPiece.UnitMonoOnTopOfGrid);
					ResetLastSelectedGridPiece();
				}
				else
				{
					UnitSelection(selectedGridPiece);

				}
			}
			else
			{
				if (lastSelectedUnitMono && _moveableGridPieces.Contains(selectedGridPiece))
				{
					// move command
					List<GridPiece> pathToTargetGridPiece = PathFinder.GetPathToTargetGridPiece(_lastSelectedGridPiece, selectedGridPiece);
					SendMoveCommandToBattleController(selectedGridPiece.UnitMonoOnTopOfGrid, pathToTargetGridPiece);
				}
				else
				{
					// show info about land
					GridSelection(selectedGridPiece);
				}
			}

			_lastSelectedGridPiece = selectedGridPiece;
		}

		void ResetLastSelectedGridPiece() { _lastSelectedGridPiece = null; }
		
		void GridSelection(GridPiece selectedGridPiece)
		{
			_infoPanelSO.ShowGridPieceInfo(selectedGridPiece);
			selectedGridPiece.SetGridMaterialToHighlighted(_gridPieceHighlightEffectSO);
		}

		/// <summary>
		/// Shows all possible pathways
		/// </summary>
		/// <param name="selectedGridPiece"></param>
		void UnitSelection(GridPiece selectedGridPiece)
		{
			// show info about unit
			_infoPanelSO.ShowUnitInfo(selectedGridPiece.UnitMonoOnTopOfGrid);
			_lastSelectedGridPiece = selectedGridPiece;

			List<GridPiece> gridPieces = PathFinder.CalculateMoveablePaths(selectedGridPiece.UnitMonoOnTopOfGrid);
			foreach (GridPiece gridPiece in gridPieces)
			{
				_moveableGridPieces.Add(gridPiece);
			}

			GridExtension.SetAllGridsToHighlighted(gridPieces, _gridPieceHighlightEffectSO);
		}

		public void OnSelectingNull()
		{
			if (_moveableGridPieces.Count > 0)
			{
				foreach (GridPiece gridPiece in _moveableGridPieces)
				{
					gridPiece.SetMaterialToDefault();
				}
				
				_moveableGridPieces.Clear();
			}
			ResetLastSelectedGridPiece();
		}
		
		void SendAttackCommandToBattleController(UnitMono attackReceiver)
		{
			_battleController.ExecuteAttack(_lastSelectedGridPiece.UnitMonoOnTopOfGrid, attackReceiver);
		}
		
		void SendMoveCommandToBattleController(UnitMono movingUnit, List<GridPiece> pathToTargetGridPiece)
		{
			_battleController.ExecuteMove(movingUnit, pathToTargetGridPiece);		
		}
	}
}