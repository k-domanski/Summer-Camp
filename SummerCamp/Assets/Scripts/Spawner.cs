using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using System.Linq;

public class Spawner : ElympicsMonoBehaviour, IInitializable
{
    [SerializeField] private FloorManager floorManager;

    public static Spawner Instance { get; private set; }
    
    private List<Transform> transforms;
    private System.Random random = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Initialize()
    {
        if (!Elympics.IsServer)
            return;

        random = new System.Random();

        transforms = floorManager.FloorTiles;
    }

    public void Spawn<T>(T obj, float range) where T : Component
    {
        Vector3 spawnPoint = GetSpawnPointWithoutTypeInRange<T>(range).position;

        obj.transform.position = spawnPoint;
    }

    private Transform GetSpawnPointWithoutTypeInRange<T> (float range) where T : Component
    {
        var randomizedSpawnPoints = GetRandomizedSpawnPoints();

        Transform chosenSpawnPoint = null;

        foreach (Transform transform in randomizedSpawnPoints)
        {
            chosenSpawnPoint = transform;

            Collider[] objectsInRange = Physics.OverlapSphere(chosenSpawnPoint.position, range);

            if (!objectsInRange.Any(x => x.transform.root.gameObject.TryGetComponent<T>(out _)))
            {
                break;
            }
        }

        return chosenSpawnPoint;
    }

    private IOrderedEnumerable<Transform> GetRandomizedSpawnPoints()
    {
        return transforms.OrderBy(x => random.Next());
    }
}
