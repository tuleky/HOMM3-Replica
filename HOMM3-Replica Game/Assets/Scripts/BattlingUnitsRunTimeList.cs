using System.Collections.Generic;

public static class BattlingUnitsRunTimeList
{
	public static List<UnitMono> Units { get; } = new List<UnitMono>();

	static void Initialize()
	{
		Units.Clear();
	}
	
	public static void AddUnitToList(UnitMono unitMono)
	{
		Units.Add(unitMono);
	}

	public static void RemoveUnitFromList(UnitMono unitMono)
	{
		if (!Units.Contains(unitMono))
		{
			Units.Remove(unitMono);
		}
	}
}