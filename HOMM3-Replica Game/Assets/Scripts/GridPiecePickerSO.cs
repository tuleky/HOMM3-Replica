using Scriptable_Objects;
using UnityEngine;

[CreateAssetMenu(fileName = "Grid Piece Picker", menuName = "Grid Piece Picker", order = 0)]
public class GridPiecePickerSO : ScriptableObject
{
	[SerializeField] CameraContainer _cameraContainer;
	[SerializeField] BattleSelectionManager battleSelectionManager;

	[SerializeField] LayerMask pickableLayer;

	public void OnUpdate()
	{
		if (Input.GetMouseButtonDown(0))
		{
			battleSelectionManager.OnSelectingNull();
		
			Vector2 originPoint = _cameraContainer.Camera.ScreenToWorldPoint(Input.mousePosition);
			Vector2 targetDirection = _cameraContainer.Camera.transform.forward;
		
			RaycastHit2D hitInfo2D = Physics2D.Raycast(originPoint, targetDirection, 20f, pickableLayer);

			if (hitInfo2D.collider != null)
			{
				GridPiece pickedUpGridPiece = hitInfo2D.collider.GetComponent<GridPiece>();
				if (pickedUpGridPiece != null)
				{
					battleSelectionManager.OnGridPieceSelected(pickedUpGridPiece);
				}
			}
		}
	}
}