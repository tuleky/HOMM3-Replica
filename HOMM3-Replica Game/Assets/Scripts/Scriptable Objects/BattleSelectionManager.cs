using System.Collections.Generic;
using UnityEngine;

namespace Scriptable_Objects
{
	[CreateAssetMenu(fileName = "Battle Selection Manager", menuName = "Battle Selection Manager", order = 0)]
	public class BattleSelectionManager : ScriptableObject
	{
		[SerializeField] InfoPanelSO _infoPanelSO;
		[SerializeField] GridPieceHighlightEffectSO _gridPieceHighlightEffectSO;
		
		
		readonly List<GridPiece> _highlightedGridPieces = new List<GridPiece>();
		GridPiece _lastSelectedGridPiece;

		BattleController _battleController;
		
		public void Initialize(BattleController battleController)
		{
			_battleController = battleController;
		}

		public void OnGridPieceSelected(GridPiece selectedGridPiece)
		{
			UnitMono unitMonoOnTopOfGrid = null;
			if (_lastSelectedGridPiece)
			{
				_lastSelectedGridPiece.SetMaterialToDefault();
				unitMonoOnTopOfGrid = _lastSelectedGridPiece.UnitMonoOnTopOfGrid;
			}
			
			if (selectedGridPiece.HasUnit())
			{
				if (unitMonoOnTopOfGrid && unitMonoOnTopOfGrid != selectedGridPiece.UnitMonoOnTopOfGrid)
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
				if (unitMonoOnTopOfGrid)
				{
					// move command
					
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

			List<GridPiece> gridPieces = PathFinder.GetAvailablePaths(selectedGridPiece.UnitMonoOnTopOfGrid);
			foreach (GridPiece gridPiece in gridPieces)
			{
				_highlightedGridPieces.Add(gridPiece);
			}

			GridExtension.SetAllGridsToHighlighted(gridPieces, _gridPieceHighlightEffectSO);
		}

		public void OnSelectingNull()
		{
			if (_highlightedGridPieces.Count > 0)
			{
				foreach (GridPiece gridPiece in _highlightedGridPieces)
				{
					gridPiece.SetMaterialToDefault();
				}
				
				_highlightedGridPieces.Clear();
			}
			ResetLastSelectedGridPiece();
		}
		
		void SendAttackCommandToBattleController(UnitMono attackReceiver)
		{
			_battleController.Attack(_lastSelectedGridPiece.UnitMonoOnTopOfGrid, attackReceiver);
		}
	}
}
