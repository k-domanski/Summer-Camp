using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Vector2Int maxSize;

    public List<Transform> FloorTiles { get; private set; } = new List<Transform>();

    private void Awake()
    {
        foreach(var collider in gameObject.GetComponentsInChildren<BoxCollider>())
        {
            FloorTiles.Add(collider.transform);
        }
    }

    [ContextMenu("Set Tiles Position")]
    private void SetTiles()
    {
        Queue<Transform> queue = new Queue<Transform>();
        foreach(var collider in gameObject.GetComponentsInChildren<BoxCollider>())
        {
            queue.Enqueue(collider.transform);
        }
        Debug.Log($"QUEUE SIZE: {queue.Count}");
        for (int x = 0; x < maxSize.x; x++)
        {
            for (int y = 0; y < maxSize.y; y++)
            {
                var trans = queue.Dequeue();
                trans.position = grid.GetCellCenterWorld(new Vector3Int(x, y));
            }
        }
    }

}
