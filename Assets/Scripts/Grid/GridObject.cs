using UnityEngine;

public class GridObject
{
    private GridPosition gridPosition;
    private GridSystem<GridObject> gridSystem;

    private object objectInGrid;
    private IInteractable interactable;

    public GridObject(GridSystem<GridObject> gridSystem, GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
        this.gridSystem = gridSystem;
    }

    public void SetInteractable(IInteractable interactable) => this.interactable = interactable;

    public IInteractable GetInteractable() => interactable;

    public void RemoveInteractable()
    {
        interactable = null;
    }
}
