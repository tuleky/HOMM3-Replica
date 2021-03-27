using System.Collections.Generic;
using UnityEngine;
namespace Scriptable_Objects
{
	[CreateAssetMenu(fileName = "Available Grid Pieces", menuName = "Available Grid Pieces", order = 0)]
	public class AvailableGridPieces : ScriptableObject
	{
		public List<GridPiece> GridPieces;
	}
}