using System.Collections.Generic;

namespace Commands
{
	public class MoveCommand : Command
	{
		public UnitMono MovingUnit { get; }
		public List<GridPiece> Path { get; }

		public MoveCommand(UnitMono movingUnit, List<GridPiece> path)
		{
			MovingUnit = movingUnit;
			Path = path;
		}	
		
		public override void Execute()
		{
			MovingUnit.StartCoroutine(MovingUnit.MoveCoroutine(this));
		}
		
		public override void Undo()
		{
			throw new System.NotImplementedException();
		}
	}
}
