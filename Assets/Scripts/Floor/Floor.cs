using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public static Floor Instance { get; private set; }

    // Floor tile visual prefab
    [SerializeField] Transform floorTilePrefab;
    [SerializeField] Transform wallPrefab;

    // Array of visual representation of a single cell of a grid
    private FloorTile[,] floorTiles;

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
        floorTiles = new FloorTile[levelGridWidth, levelGridHeight];
        for (int x = 0; x < levelGridWidth; x++)
        {
            for (int z = 0; z < levelGridHeight; z++)
            {
                GridPosition gridPosition = new(x, z);
                Transform floorTileTransform = Instantiate(floorTilePrefab, levelGrid.GetWorldPosition(gridPosition), Quaternion.identity);
                floorTileTransform.localScale = new Vector3(cellSize, 1, cellSize);
                floorTileTransform.parent = transform;
                floorTiles[x, z] = floorTileTransform.GetComponent<FloorTile>();
                if (x == 0 || x == levelGridWidth - 1 || z == 0 || z == levelGridHeight - 1)
                {
                    Transform wallTransform = Instantiate(wallPrefab, levelGrid.GetWorldPosition(gridPosition), Quaternion.identity);
                    wallTransform.localScale = new Vector3(1, levelGrid.GetFloorHeight(), 1);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
