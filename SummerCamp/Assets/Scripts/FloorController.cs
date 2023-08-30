using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class FloorController : ElympicsMonoBehaviour, IInitializable
{
    [Header("Parameters")]
    [SerializeField] private float timeStepForTileRemove = 5.0f;

    private FloorManager floorManager;
    private GameManager gameManager;
    private int timeMultiplier = 1;

    public void Initialize()
    {
        if (!Elympics.IsServer)
            return;

        floorManager = GetComponent<FloorManager>();
        gameManager = FindObjectOfType<GameManager>();


        gameManager.IsRunning.ValueChanged += ProcessGameStart;
    }

    private void ProcessGameStart(bool lastValue, bool newValue)
    {
        if(newValue)
        {
            gameManager.CurrentTime.ValueChanged += ProcessCurrentTime;
        }
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
        Transform tile = floorManager.GetSpawnPointWithoutTypeInRange<PlayerData>(2 * Mathf.Sqrt(2));
        
        //material only on server but maybe it looks good? if so move to client
        tile.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        
        timeMultiplier++;
        // spawn which tile to remove indicator?
        yield return new WaitForSeconds(2);
        // remove tile indicator?
        RemoveTile(tile);
    }

    private void RemoveTile(Transform tile)
    {
        floorManager.RemoveTile(tile);
        var rb = tile.gameObject.AddComponent<Rigidbody>();
        rb.mass = 100;
        rb.useGravity = true;
    }
}
