namespace Commands
{
	public class AttackCommand : Command
	{
		public UnitMono Attacker { get; }
		public UnitMono AttackReceiver { get; }
	
		public AttackCommand(UnitMono attacker, UnitMono attackReceiver)
		{
			Attacker = attacker;
			AttackReceiver = attackReceiver;
		}
	
		public override void Execute()
		{
			Attacker.CheckForSpecialAttack(this);
			CommandBuffer.AddToStack(this);
		}

		public override void Undo()
		{
		
		}
	}
}