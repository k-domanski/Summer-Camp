using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSetup : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Vector2Int maxSize;

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
