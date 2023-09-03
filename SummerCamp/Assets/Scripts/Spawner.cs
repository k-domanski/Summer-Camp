using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using System.Linq;

public class Spawner : ElympicsMonoBehaviour
{
    [SerializeField] private FloorManager floorManager;

    public static Spawner Instance { get; private set; }
    
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

    public void Spawn<T>(T obj, float range) where T : Component
    {
        Vector3 spawnPoint = floorManager.GetSpawnPointWithoutTypeInRange<T>(range).transform.position;
        obj.transform.position = spawnPoint;
    }
}
