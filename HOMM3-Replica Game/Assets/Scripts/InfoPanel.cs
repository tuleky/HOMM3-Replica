using Scriptable_Objects;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
	[SerializeField] InfoPanelSO _infoPanelSO;

	void OnEnable()
	{
		_infoPanelSO.Register(this);
	}

	void OnDisable()
	{
		_infoPanelSO.Unregister();
	}

	public void ShowUnitInfo(UnitMono unitMono)
	{
		print("Show Unit Info");
	}
	
	public void ShowGridPieceInfo(GridPiece unit)
	{
		print("Show Grid Info");
	}
}