using System.Collections.Generic;
using Scriptable_Objects;
public static class GridExtension
{
	public static void SetAllGridsToHighlighted(List<GridPiece> gridPieces, GridPieceHighlightEffectSO gridPieceHighlightEffectSO)
	{
		foreach (GridPiece gridPiece in gridPieces)
		{
			gridPiece.SetGridMaterialToHighlighted(gridPieceHighlightEffectSO);
		}
	}
}