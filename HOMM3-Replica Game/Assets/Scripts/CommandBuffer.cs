using System.Collections.Generic;
using Commands;

public static class CommandBuffer
{
	static readonly Stack<Command> COMMANDS = new Stack<Command>();

	public static void AddToStack(Command commandToAdd)
	{
		COMMANDS.Push(commandToAdd);
	}

	public static void ResetQueue()
	{
		COMMANDS.Clear();
	}

	public static Command Undo()
	{
		return COMMANDS.Pop();
	}
}