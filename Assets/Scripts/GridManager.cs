using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Dictionary<Vector3Int, Tile> tiles =
        new Dictionary<Vector3Int, Tile>();

    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float nodeRadius = 0.4f;
    [SerializeField] private float cellSize = 2f;
    [SerializeField] private Vector2 gridOrigin = new Vector2(-8, -4);

    void Awake()
    {
        RegisterTiles();
    }

    private void RegisterTiles()
    {
        Tile[] allTiles = FindObjectsByType<Tile>(
            FindObjectsSortMode.None
        );

        foreach (Tile tile in allTiles)
        {
            Vector3Int cell = WorldToCell(tile.transform.position);

            if (tiles.ContainsKey(cell))
            {
                Debug.LogWarning($"Duplicate tile at {cell}");
                continue;
            }

            tiles.Add(cell, tile);
        }

        //Debug.Log($"Registered {tiles.Count} tiles");
    }

    public Vector3Int WorldToCell(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt((worldPos.x - gridOrigin.x) / cellSize);
        int y = Mathf.RoundToInt((worldPos.y - gridOrigin.y) / cellSize);

        return new Vector3Int(x, y, 0);
    }

    public Vector3 CellToWorldCenter(Vector3Int cellPos)
    {
        return new Vector3(
            gridOrigin.x + cellPos.x * cellSize,
            gridOrigin.y + cellPos.y * cellSize,
            0
        );
    }

    public Tile GetTile(Vector3Int cellPos)
    {
        tiles.TryGetValue(cellPos, out Tile tile);
        return tile;
    }

    public bool IsWalkable(Vector3Int cellPos)
    {
        if (!tiles.ContainsKey(cellPos))
            return false;

        Vector3 worldCenter = CellToWorldCenter(cellPos);

        Collider2D hit = Physics2D.OverlapCircle(
            worldCenter,
            nodeRadius,
            obstacleLayer
        );

        return hit == null;
    }
}