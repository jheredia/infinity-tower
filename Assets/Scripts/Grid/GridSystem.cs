using System;
using UnityEngine;

public class GridSystem<TGridObject>
{
    // X
    private int width;

    // Z
    private int height;
    private float cellSize;
    private float floorHeight;

    private TGridObject[,] gridObjects;

    public GridSystem(
        int width,
        int height,
        float cellSize,
        float floorHeight,
        Func<GridSystem<TGridObject>,
        GridPosition, TGridObject> createGridObject
    )
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.floorHeight = floorHeight;

        // Matrix of game objects
        gridObjects = new TGridObject[width, height];

        // Populate the grid system with positions and objects
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new(x, z);
                gridObjects[x, z] = createGridObject(this, gridPosition);
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition) => new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;

    public GridPosition GetGridPosition(Vector3 worldPosition) =>
        new(Mathf.RoundToInt(worldPosition.x / cellSize), Mathf.RoundToInt(worldPosition.z / cellSize));

    public TGridObject GetGridObject(GridPosition gridPosition) => gridObjects[gridPosition.x, gridPosition.z];

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        GridPosition gridPosition = GetGridPosition(worldPosition);
        return GetGridObject(gridPosition);
    }

    public bool IsValidGridPosition(GridPosition gridPosition) =>
        gridPosition.x >= 0 && gridPosition.x < width && gridPosition.z >= 0 && gridPosition.z < height;

    public int GetWidth() => width;

    public int GetHeight() => height;

    public float GetFloorHeight() => floorHeight;
}
