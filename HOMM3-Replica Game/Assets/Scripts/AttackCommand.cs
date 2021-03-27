using Scriptable_Objects;
public class AttackCommand : Command
{
	public UnitMono Attacker { get; }
	public UnitMono TargetUnit { get; }
	
	public AttackCommand(UnitMono attacker, UnitMono targetUnit)
	{
		Attacker = attacker;
		TargetUnit = targetUnit;
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