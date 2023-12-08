using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance { get; private set; }

    // Grid cell visual prefab
    [SerializeField] Transform gridCellPrefab;

    // Array of visual representation of a single cell of a grid
    private GridCell[,] gridCells;

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
        gridCells = new GridCell[levelGridWidth, levelGridHeight];
        for (int x = 0; x < levelGridWidth; x++)
        {
            for (int z = 0; z < levelGridHeight; z++)
            {
                GridPosition gridPosition = new(x, z);
                Transform gridCellTransform = Instantiate(gridCellPrefab, levelGrid.GetWorldPosition(gridPosition), Quaternion.identity);
                gridCellTransform.localScale = new Vector3(cellSize, 1, cellSize);
                gridCellTransform.parent = transform;
                gridCells[x, z] = gridCellTransform.GetComponent<GridCell>();
            }
        }
        ShowAllGridPositions();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ShowGridRange(GridPosition gridPosition, int range)
    {

        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                GridPosition offsetGridPosition = new(x, z);
                GridPosition testGridPosition = gridPosition + offsetGridPosition;
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;

            }
        }
    }

    private void ShowGridPositions(List<GridPosition> gridPositions)
    {
        foreach (GridPosition gridPosition in gridPositions)
        {
            if (!gridPosition.isActive) continue;
            GridCell gridCell = GetGridCell(gridPosition);
            gridCell.Show();
        }
    }

    private void ShowAllGridPositions()
    {
        foreach (GridCell gridCell in gridCells)
        {
            gridCell.Show();
        }
    }

    private void HideGridPosition(List<GridPosition> gridPositions)
    {
        foreach (GridPosition gridPosition in gridPositions)
        {
            if (!gridPosition.isActive) continue;
            GridCell gridCell = GetGridCell(gridPosition);
            gridCell.Hide();
        }
    }

    private void HideAllGridPosition()
    {
        foreach (GridCell gridCell in gridCells)
        {
            gridCell.Hide();
        }
    }

    private GridCell GetGridCell(GridPosition gridPosition) => gridCells[gridPosition.x, gridPosition.z];
}
