using System.Collections.Generic;
using UnityEngine;

public static class PathFinder
{
	public static List<GridPiece> GetAvailablePaths(UnitMono chosenUnitMono)
	{
		// try to map a moveable cells based on chosen unit and grids
		
		int moveDistance = chosenUnitMono.MoveSpeed;
		
		// here we will do pathfinding solution for finding all possible moveable paths
		Vector2Int startingPointIndex = chosenUnitMono.GridIndex;
		GridPiece startingPointGridPiece = GridPiece.GetGridPieceByIndex(startingPointIndex);

		List<GridPiece> availableGridPieces = new List<GridPiece>();

		List<GridPiece> firstIteration = new List<GridPiece>();
		firstIteration.Add(startingPointGridPiece);
		
		for (int i = 0; i < moveDistance; i++)
		{
			// dynamically change startingPointGridPiece so every iteration calculates for new points

			List<GridPiece> newNeighbors = new List<GridPiece>();
			foreach (GridPiece gridPiece in firstIteration)
			{
				newNeighbors.AddRange(GetNeighbors(gridPiece));
			}
			firstIteration = newNeighbors;

			foreach (GridPiece gridPiece in newNeighbors)
			{
				if (!availableGridPieces.Contains(gridPiece))
				{
					availableGridPieces.Add(gridPiece);
				}
			}
		}

		return availableGridPieces;
	}

	static List<GridPiece> GetNeighbors(GridPiece gridPiece)
	{
		List<GridPiece> gridPieces = new List<GridPiece>();

		for (int i = 0; i < 360; i += 90)
		{
			Vector2Int neighborIndex = gridPiece.GridIndex
				+ new Vector2Int(Mathf.RoundToInt(Mathf.Cos(i * Mathf.Deg2Rad)),
					Mathf.RoundToInt(Mathf.Sin(i * Mathf.Deg2Rad)));

			GridPiece result = GridPiece.GetGridPieceByIndex(neighborIndex);
			if (result != null)
				gridPieces.Add(result);
		}
		
		return gridPieces;
	}
}