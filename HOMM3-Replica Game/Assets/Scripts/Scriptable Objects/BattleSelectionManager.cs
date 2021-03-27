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
		UnitMono _lastPickedUnit;
		
		BattleController _battleController;
		
		public void Initialize(BattleController battleController)
		{
			_battleController = battleController;
		}

		public void OnGridPieceSelected(GridPiece selectedGridPiece)
		{
			selectedGridPiece._spriteRenderer.material = _gridPieceHighlightEffectSO.Material;
			
			CheckGridPieceState(selectedGridPiece);
		}
		
		void CheckGridPieceState(GridPiece selectedGridPiece)
		{
			if (selectedGridPiece.HasUnit())
			{
				if (_lastPickedUnit)
				{
					// attack command
					SendAttackCommandToBattleController(selectedGridPiece.UnitMonoOnTopOfGrid);
				}
				else
				{
					UnitSelection(selectedGridPiece);
				}
			}
			else
			{
				// show info about land
				_infoPanelSO.ShowGridPieceInfo(selectedGridPiece);
			}
		}
		void UnitSelection(GridPiece selectedGridPiece)
		{
			// show info about unit
			_infoPanelSO.ShowUnitInfo(selectedGridPiece.UnitMonoOnTopOfGrid);
			_lastPickedUnit = selectedGridPiece.UnitMonoOnTopOfGrid;

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
		}
		
		void SendAttackCommandToBattleController(UnitMono attacker)
		{
			_battleController.Attack(attacker, _lastPickedUnit);
		}
	}
}
