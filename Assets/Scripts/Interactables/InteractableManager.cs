using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public static InteractableManager Instance { get; private set; }

    [SerializeField] List<IInteractable> interactables;

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
        float chanceForInteractable = 5f;
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new(x, z);
                if (WallGenerator.Instance.GetWallAtGridPosition(gridPosition) != null) continue;
                if (Random.Range(0, 101) <= chanceForInteractable)
                {
                    SetInteractableAtGridPosition(gridPosition, GetRandomInteractable());
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<IInteractable> GetInteractables() => interactables;

    public IInteractable GetRandomInteractable()
    {
        int index = Mathf.RoundToInt(Random.Range(0, interactables.Count()));
        return interactables[index];
    }

    private void SetInteractableAtGridPosition(GridPosition gridPosition, IInteractable interactable)
    {
        LevelGrid.Instance.GetGridSystem().GetGridObject(gridPosition).SetInteractable(interactable);
    }
}
