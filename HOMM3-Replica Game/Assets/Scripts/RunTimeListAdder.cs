using UnityEngine;

public class RunTimeListAdder : MonoBehaviour
{
	[SerializeField] UnitMono unitMono;

	void OnEnable()
	{
		BattlingUnitsRunTimeList.AddUnitToList(unitMono);
	}

	void OnDisable()
	{
		BattlingUnitsRunTimeList.RemoveUnitFromList(unitMono);
	}
}