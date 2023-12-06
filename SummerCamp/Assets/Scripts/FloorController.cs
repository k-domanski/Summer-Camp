using System.Collections;
using UnityEngine;
using Elympics;

public class FloorController : ElympicsMonoBehaviour, IInitializable
{
    [Header("Parameters")]
    [SerializeField] private float timeStepForTileRemove = 5.0f;
    [SerializeField] private GameManager gameManager;
    
    private FloorManager floorManager;
    private int timeMultiplier = 1;

    public void Initialize()
    {
        if (!Elympics.IsServer)
            return;

        floorManager = GetComponent<FloorManager>();

        gameManager.RaceStarted += ProcessGameStart;
    }

    private void ProcessGameStart()
    {
        gameManager.CurrentTime.ValueChanged += ProcessCurrentTime;
    }

    private void ProcessCurrentTime(float lastValue, float newValue)
    {
        if(newValue >= (timeStepForTileRemove * timeMultiplier))
        {
            StartCoroutine(Test());
        }
    }

    private IEnumerator Test()
    {
        FloorTile tile = floorManager.GetSpawnPointWithoutTypeInRange<PlayerData>(2 * Mathf.Sqrt(2));
        
        //material only on server but maybe it looks good? if so move to client
        tile.SetColor();
        
        timeMultiplier++;
        // spawn which tile to remove indicator?
        yield return new WaitForSeconds(2);
        // remove tile indicator?
        RemoveTile(tile);
    }

    private void RemoveTile(FloorTile tile)
    {
        CleanObjectsFromTile(tile);
        floorManager.RemoveTile(tile);
        var rb = tile.gameObject.AddComponent<Rigidbody>();
        rb.mass = 100;
        rb.useGravity = true;
    }

    private void CleanObjectsFromTile(FloorTile tile)
    {
        Collider[] objects = Physics.OverlapBox(tile.transform.position, new Vector3(2.0f, 2.0f, 2.0f));

        foreach(Collider obj in objects)
        {
            if(obj.TryGetComponent<IRemovable>(out var removable))
            {
                removable.Remove();
            }
        }
    }
}
