using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField]private int width, height;
    [SerializeField]private Tile tilePrefab;
    [SerializeField]private Camera camera;
    void Start()
    {
        GenerateGrid(width, height);
    }

    // Update is called once per frame
    public void GenerateGrid(int width, int height)
    {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Tile newTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                newTile.name = $"Tile {x} {y}";

                bool isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                newTile.initializeColor(isOffset);
            }
        }

        camera.transform.position = new Vector3((float)width/2 - 0.5f, (float)height/2 - 0.5f, -10);
    }
}
