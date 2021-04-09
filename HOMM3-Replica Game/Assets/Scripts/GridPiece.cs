using System.Collections.Generic;
using Scriptable_Objects;
using UnityEngine;

public class GridPiece : MonoBehaviour
{
    public static Dictionary<Vector2Int, GridPiece> GridPieces = new Dictionary<Vector2Int, GridPiece>();
    public static GridPiece GetGridPieceByIndex(Vector2Int index)
    {
        GridPieces.TryGetValue(index, out GridPiece value);
        return value;
    }
    
    public Vector2Int GridIndex;

    public SpriteRenderer _spriteRenderer;

    Material _defaultMaterial;
    public UnitMono UnitMonoOnTopOfGrid { get; private set; }

    void Awake()
    {
        _defaultMaterial = _spriteRenderer.sharedMaterial;
    }

    public void SetMaterialToDefault()
    {
        _spriteRenderer.material = _defaultMaterial;
    }
    
    // When we pick a grid and show available grids around that
    public void SetGridMaterialToHighlighted(GridPieceHighlightEffectSO gridPieceHighlightEffectSO)
    {
        // Do some fancy stuff here
        _spriteRenderer.material = gridPieceHighlightEffectSO.Material;
    }
    
    public void SetUnitToGrid(UnitMono unitMono)
    {
        UnitMonoOnTopOfGrid = unitMono;
    }

    public bool HasUnit()
    {
        return UnitMonoOnTopOfGrid;
    }
    
    void OnEnable()
    {
        GridPieces.Add(GridIndex, this);
    }
    
    void OnDisable()
    {
        GridPieces.Remove(GridIndex);
    }
}