using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{

    public static WallGenerator Instance { get; private set; }

    [SerializeField] Transform wallPrefab;

    // Array that represents a grid in which each cell can contain a wall
    private Wall[,] wallGrid;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Multiple instances of {GetType().Name} present {transform} - {Instance}");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelGrid levelGrid = LevelGrid.Instance;
        int levelGridWidth = levelGrid.GetWidth();
        int levelGridHeight = levelGrid.GetHeight();
        float cellSize = levelGrid.GetCellSize();
        wallGrid = new Wall[levelGridWidth, levelGridHeight];
        for (int x = 0; x < levelGridWidth; x++)
        {
            for (int z = 0; z < levelGridHeight; z++)
            {
                GridPosition gridPosition = new(x, z);
                if (x == 0 || x == levelGridWidth - 1 || z == 0 || z == levelGridHeight - 1)
                {
                    Transform wallTransform = Instantiate(wallPrefab, levelGrid.GetWorldPosition(gridPosition), Quaternion.identity);
                    wallTransform.localScale = new Vector3(cellSize, levelGrid.GetFloorHeight(), cellSize);
                    wallTransform.parent = transform;
                    wallGrid[x, z] = wallTransform.GetComponent<Wall>();
                }
            }
        }

    }

    public Wall GetWallAtGridPosition(GridPosition gridPosition)
    {
        if (!LevelGrid.Instance.IsValidGridPosition(gridPosition)) return null;
        return wallGrid[gridPosition.x, gridPosition.z];
    }
}
