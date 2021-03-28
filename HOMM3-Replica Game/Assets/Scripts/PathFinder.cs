using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class PathFinder
{
	// west and east are straightforward (-1,0) and (+1,0), diagonals are north west (-1, +1), north east (+1, +1), south west (-1, -1), south east (+1, -1)
	static readonly Vector2Int[] DIRECTIONS =
	{
		new Vector2Int(-1, 0),
		new Vector2Int(-1, 0),
		new Vector2Int(1, 0),
		new Vector2Int(-1, 1),
		new Vector2Int(1, 1),
		new Vector2Int(-1, -1), 
		new Vector2Int(1, -1)
	};

	static Dictionary<GridPiece, GridPiece> cameFromDictionary = new Dictionary<GridPiece, GridPiece>();
	
	public static List<GridPiece> GetAvailablePaths(UnitMono chosenUnitMono)
	{
		// try to map a moveable cells based on chosen unit and grids
		
		// here we will do pathfinding solution for finding all possible moveable paths
		GridPiece startingPointGridPiece = GridPiece.GetGridPieceByIndex(chosenUnitMono.GridIndex);
		
		
		List<GridPiece> availableGridPieces = new List<GridPiece>();

		List<GridPiece> tempIteration = new List<GridPiece>
		{
			startingPointGridPiece
		};

		for (int i = 0; i < chosenUnitMono.MoveSpeed; i++)
		{
			// dynamically change startingPointGridPiece so every iteration calculates for new points

			List<GridPiece> newNeighbors = new List<GridPiece>();
			foreach (GridPiece gridPiece in tempIteration)
			{
				newNeighbors.AddRange(GetNeighbors(gridPiece));
			}
			tempIteration = newNeighbors;

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
		
		foreach (Vector2Int vector2Int in DIRECTIONS)
		{
			GridPiece foundGridPieceByIndex = GridPiece.GetGridPieceByIndex(gridPiece.GridIndex + vector2Int);
			if (foundGridPieceByIndex != null && !foundGridPieceByIndex.HasUnit())
			{
				result.Add(foundGridPieceByIndex);
			}
		}

		return result;
	}

	static void TraceBackFromGoalToStart()
	{
		
	}
	
}