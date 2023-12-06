using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using System;

public class SkillSpawnerManager : ElympicsMonoBehaviour, IInitializable
{
    [SerializeField] private float timeToSpawn = 10.0f;
    [SerializeField] private int maxSpawners = 3;

    protected ElympicsFloat currentSpawnTime = new ElympicsFloat();

    private GameManager gameManager;
    private FloorManager floorManager;

    private List<SkillSpawner> activeSpawners = new List<SkillSpawner>();
    private Queue<SkillSpawner> availableSpawners = new Queue<SkillSpawner>();
    private int timeMultiplier = 1;

    public void Initialize()
    {
        foreach (SkillSpawner skillSpawner in GetComponentsInChildren<SkillSpawner>())
        {
            availableSpawners.Enqueue(skillSpawner);
            skillSpawner.gameObject.SetActive(false);
        }

        if (Elympics.IsClient || Elympics.IsBot)
            return;

        gameManager = FindObjectOfType<GameManager>();
        floorManager = FindObjectOfType<FloorManager>();

        gameManager.IsRunning.ValueChanged += ProcessGameStart;

        
    }

    private void ProcessGameStart(bool lastValue, bool newValue)
    {
        if(newValue)
        {
            Spawn();
            gameManager.CurrentTime.ValueChanged += ProcessGameTimeChanged;
        }
    }

    private void ProcessGameTimeChanged(float lastValue, float newValue)
    {
        if(newValue >= (timeToSpawn * timeMultiplier))
        {
            Spawn();
            timeMultiplier++;
        }
    }

    private void Spawn()
    {
        if (activeSpawners.Count >= maxSpawners)
            return;

        var spawner = availableSpawners.Dequeue();
        spawner.transform.position = floorManager.GetSpawnPointWithoutTypeInRange<SkillSpawnerManager>(2.0f * Mathf.Sqrt(2)).transform.position;

        activeSpawners.Add(spawner);
        spawner.gameObject.SetActive(true);
        spawner.ResetSpawner();
        spawner.onSkillPickedUp += OnSkillPickedUp;
    }

    private void OnSkillPickedUp(SkillSpawner spawner)
    {
        activeSpawners.Remove(spawner);
        availableSpawners.Enqueue(spawner);

        spawner.onSkillPickedUp -= OnSkillPickedUp;
        spawner.gameObject.SetActive(false);
    }
}
