using System.Collections.Generic;
using UnityEngine;

public static class PathFinder
{
	// west and east are straightforward (-1,0) and (+1,0), diagonals are north west (-1, +1), north east (+1, +1), south west (-1, -1), south east (+1, -1)
	static readonly Vector2Int[] DIRECTIONS =
	{
		new Vector2Int(-2, 0),
		new Vector2Int(2, 0),
		new Vector2Int(-1, 1),
		new Vector2Int(1, 1),
		new Vector2Int(-1, -1), 
		new Vector2Int(1, -1)
	};

	static Dictionary<GridPiece, GridPiece> cameFrom = new Dictionary<GridPiece, GridPiece>();

	public static List<GridPiece> CalculateMoveablePaths(UnitMono chosenUnitMono)
	{
		GridPiece startingPointGridPiece = GridPiece.GetGridPieceByIndex(chosenUnitMono.GridIndex);

		cameFrom[startingPointGridPiece] = startingPointGridPiece;
		
		List<GridPiece> moveableGridPieces = new List<GridPiece>();

		Queue<GridPiece> frontiers = new Queue<GridPiece>();
		frontiers.Enqueue(startingPointGridPiece);
		
		for (int i = 0; i < chosenUnitMono.MoveSpeed; i++)
		{
			int frontierCount = frontiers.Count;
			for (int j = 0; j < frontierCount; j++)
			{
				GridPiece currentPiece = frontiers.Dequeue();

				// new neighbors is new ones we just found
				foreach (GridPiece gridPiece in GetNeighbors(currentPiece))
				{
					if (!moveableGridPieces.Contains(gridPiece))
					{
						moveableGridPieces.Add(gridPiece);
						frontiers.Enqueue(gridPiece);
						cameFrom[gridPiece] = currentPiece;
					}
				}
			}
		}

		return moveableGridPieces;
	}

	public static List<GridPiece> GetPathToTargetGridPiece(GridPiece startingGridPiece, GridPiece targetGridPiece)
	{
		List<GridPiece> tracedBackPath = new List<GridPiece>();
		GridPiece currentPiece = targetGridPiece;
		
		while (currentPiece != startingGridPiece)
		{
			tracedBackPath.Add(currentPiece);
			currentPiece = cameFrom[currentPiece];
		}
		tracedBackPath.Reverse();
		return tracedBackPath;
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
}