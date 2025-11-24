using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector] public Vector2Int gridPosition;
    [HideInInspector] public bool visited = false;

    // Optional: references to door objects inside this prefab for locking
    public GameObject doorTop;
    public GameObject doorBottom;
    public GameObject doorLeft;
    public GameObject doorRight;

    // Called when player enters
    public void OnPlayerEnter()
    {
        visited = true;
        // TODO: reveal minimap icon, spawn enemies, etc.
    }
}
