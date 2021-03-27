using System.Collections.Generic;
using UnityEditor;
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
		List<GridPiece> result = new List<GridPiece>();

		List<Vector2Int> temp = new List<Vector2Int>
		{
			gridPiece.GridIndex + new Vector2Int(-1, 0),
			gridPiece.GridIndex + new Vector2Int(1, 0),
			gridPiece.GridIndex + new Vector2Int(-1, 1),
			gridPiece.GridIndex + new Vector2Int(1, 1),
			gridPiece.GridIndex + new Vector2Int(-1, -1),
			gridPiece.GridIndex + new Vector2Int(1, -1)
		};
		// west and east are straightforward (-1,0) and (+1,0), diagonals are north west (-1, +1), north east (+1, +1), south west (-1, -1), south east (+1, -1)

		foreach (Vector2Int vector2Int in temp)
		{
			GridPiece foundGridPieceByIndex = GridPiece.GetGridPieceByIndex(vector2Int);
			if (foundGridPieceByIndex != null && !foundGridPieceByIndex.HasUnit())
			{
				result.Add(foundGridPieceByIndex);
			}
		}

		return result;
	}
}