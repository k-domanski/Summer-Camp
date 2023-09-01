using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Vector2Int maxSize;

    public List<Transform> FloorTiles { get; private set; } = new List<Transform>();

    private System.Random random;

    private void Awake()
    {
        random = new System.Random();

        foreach(var collider in gameObject.GetComponentsInChildren<BoxCollider>())
        {
            FloorTiles.Add(collider.transform);
        }
    }
    
    public Transform GetSpawnPointWithoutTypeInRange<T>(float range) where T : Component
    {
        var randomizedSpawnPoints = GetRandomizedSpawnPoints();

        Transform chosenSpawnPoint = null;

        foreach (Transform transform in randomizedSpawnPoints)
        {
            chosenSpawnPoint = transform;

            Collider[] objectsInRange = Physics.OverlapSphere(
                chosenSpawnPoint.position, 
                range, 
                Physics.AllLayers, 
                QueryTriggerInteraction.Collide);

            if (!objectsInRange.Any(x => x.transform.root.gameObject.TryGetComponent<T>(out _)))
            {
                break;
            }
        }

        return chosenSpawnPoint;
    }

    public void RemoveTile(Transform tile)
    {
        FloorTiles.Remove(tile);
    }

    private IOrderedEnumerable<Transform> GetRandomizedSpawnPoints()
    {
        return FloorTiles.OrderBy(x => random.Next());
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
