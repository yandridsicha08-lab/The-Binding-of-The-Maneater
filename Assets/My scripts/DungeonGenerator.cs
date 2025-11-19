using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Grid")]
    public int gridSizeX = 11;
    public int gridSizeY = 11;
    public Vector2Int startCell = new Vector2Int(5, 5);

    [Header("Rooms")]
    public int roomsToCreate = 12;
    public float roomWidth = 16f;   // world units between room centers
    public float roomHeight = 9f;
    public GameObject[] roomPrefabs; // assign different room prefabs (prefabs must be same size)

    // internal
    private HashSet<Vector2Int> placedCells = new HashSet<Vector2Int>();
    private List<Vector2Int> cellList = new List<Vector2Int>();

    void Start()
    {
        GenerateDungeon();
    }

    public void GenerateDungeon()
    {
        placedCells.Clear();
        cellList.Clear();

        Vector2Int current = startCell;
        placedCells.Add(current);
        cellList.Add(current);

        System.Random rng = new System.Random();

        while (placedCells.Count < roomsToCreate)
        {
            // Get neighbours of current cell within grid bounds
            List<Vector2Int> neighbors = GetUnoccupiedNeighbors(current);

            if (neighbors.Count > 0)
            {
                // choose random neighbor and move into it
                Vector2Int next = neighbors[rng.Next(neighbors.Count)];
                placedCells.Add(next);
                cellList.Add(next);
                current = next;
            }
            else
            {
                // backtrack: pick a random already placed cell to continue
                current = cellList[rng.Next(cellList.Count)];
            }

            // safety to avoid infinite loops
            if (placedCells.Count >= gridSizeX * gridSizeY) break;
        }

        SpawnRooms();
    }

    List<Vector2Int> GetUnoccupiedNeighbors(Vector2Int cell)
    {
        List<Vector2Int> list = new List<Vector2Int>();
        Vector2Int[] dirs = new Vector2Int[]
        {
            new Vector2Int(1,0),
            new Vector2Int(-1,0),
            new Vector2Int(0,1),
            new Vector2Int(0,-1)
        };

        foreach (var d in dirs)
        {
            Vector2Int n = cell + d;
            if (n.x < 0 || n.x >= gridSizeX || n.y < 0 || n.y >= gridSizeY) continue;
            if (!placedCells.Contains(n)) list.Add(n);
        }

        return list;
    }

    void SpawnRooms()
    {
        // Optional: parent to keep hierarchy clean
        Transform parent = new GameObject("Rooms").transform;

        foreach (var cell in placedCells)
        {
            Vector3 worldPos = GridToWorld(cell);
            GameObject prefab = roomPrefabs[Random.Range(0, roomPrefabs.Length)];
            GameObject room = Instantiate(prefab, worldPos, Quaternion.identity, parent);
            room.name = $"Room_{cell.x}_{cell.y}";

            // Optional: store grid pos on the Room component if present
            Room r = room.GetComponent<Room>();
            if (r != null) r.gridPosition = cell;
        }

        Debug.Log($"Spawned {placedCells.Count} rooms.");
    }

    Vector3 GridToWorld(Vector2Int cell)
    {
        float x = (cell.x - (gridSizeX / 2f)) * roomWidth;
        float y = (cell.y - (gridSizeY / 2f)) * roomHeight;
        return new Vector3(x, y, 0f);
    }
}

