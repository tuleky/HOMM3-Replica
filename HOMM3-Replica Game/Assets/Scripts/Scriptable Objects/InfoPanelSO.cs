using UnityEngine;
namespace Scriptable_Objects
{
	[CreateAssetMenu(fileName = "Info Panel", menuName = "Info Panel", order = 0)]
	public class InfoPanelSO : ScriptableObject
	{ 
		InfoPanel _infoPanel;

		public void Register(InfoPanel infoPanel)
		{
			_infoPanel = infoPanel;
		}
	
		public void Unregister()
		{
			_infoPanel = null;
		}
	
		public void ShowUnitInfo(UnitMono unitMono)
		{
			if (_infoPanel)
			{
				_infoPanel.ShowUnitInfo(unitMono);
			}
		}

		public void ShowGridPieceInfo(GridPiece gridPiece)
		{
			if (_infoPanel)
			{
				_infoPanel.ShowGridPieceInfo(gridPiece);
			}
		}
	}
}