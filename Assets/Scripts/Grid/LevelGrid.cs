using System;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    // Singleton, there will be only one level grid at any time on a scene
    public static LevelGrid Instance { get; private set; }

    /// <summary>
    /// Level grid configuration
    /// </summary>
    [Header("Grid Size")]
    // X
    [SerializeField, Min(1), Tooltip("X")] int width;
    // Z
    [SerializeField, Min(1), Tooltip("Z")] int height;

    /// <summary>
    /// Size that each cell will represent
    /// </summary>
    [SerializeField, Min(1f)] float cellSize;

    /// <summary>
    /// The floor height of the playable level area
    /// </summary>
    [SerializeField,
    Min(1f),
    Tooltip("The height that the playable floor should have, this will determine the size of the external walls")]
    float floorHeight = 1f;

    /// <summary>
    /// System that contains all the grid logic for the level
    /// </summary>
    private GridSystem<GridObject> gridSystem;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Multiple instances of {GetType().Name} present {transform} - {Instance}");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gridSystem = new GridSystem<GridObject>(width, height, cellSize, floorHeight,
        (GridSystem<GridObject> g, GridPosition gridPosition) => new GridObject(g, gridPosition));

    }

    /// <summary>
    /// Returns the height of the level grid. This value is the X value in the space
    /// </summary>
    /// <returns>The height of the level grid</returns>
    public int GetHeight() => height;

    /// <summary>
    /// Returns the width of the level grid. This value is the Z value in the space
    /// </summary>
    /// <returns>The width of the level grid</returns>
    public int GetWidth() => width;

    /// <summary>
    /// Returns the size of each cell of the level grid
    /// </summary>
    /// <returns>The size of any cell</returns>
    public float GetCellSize() => cellSize;

    /// <summary>
    /// Returns the floor height of the playable space of the level grid
    /// </summary>
    /// <returns>The floor height of this level grid</returns>
    public float GetFloorHeight() => floorHeight;

    /// <summary>
    /// Returns the grid system containing all grid logic for this level grid
    /// </summary>
    /// <returns><The grid system of the level grid/returns>
    public GridSystem<GridObject> GetGridSystem() => gridSystem;

    /// <summary>
    /// Given a grid position, access the grid system for this level and return its world position
    /// </summary>
    /// <param name="gridPosition">A grid position</param>
    /// <returns>The world position of this grid position</returns>
    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public bool IsValidGridPosition(GridPosition testGridPosition) => gridSystem.IsValidGridPosition(testGridPosition);
}
