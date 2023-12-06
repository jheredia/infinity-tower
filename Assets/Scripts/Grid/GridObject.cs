using UnityEngine;

public class GridObject
{
    private GridPosition gridPosition;
    private GridSystem<GridObject> gridSystem;

    private object objectInGrid;

    public GridObject(GridSystem<GridObject> gridSystem, GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
        this.gridSystem = gridSystem;
    }
}
