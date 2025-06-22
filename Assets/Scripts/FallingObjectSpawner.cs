using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class FallingObjectSpawner : MonoBehaviour
{
    public List<FallingObjectSpawnSet> fallingObjectSpawnSets;
    public GameObject eggPrefab;
    public GameObject bombPrefab;

    [Header("Spawn Behavior Settings")]
    // [Tooltip("Start allowing 2-lane spawns after this many eggs have been spawned")]
    [SerializeField]
    private int enableMultiLaneSpawnAfter = 60;
    [SerializeField] private float multiLaneChanceMin = 0.2f;
    [SerializeField] private float multiLaneChanceMax = 0.6f;

    [Header("Bomb Spawn Settings")]
    [SerializeField, Tooltip("Minimum number of eggs before bombs start appearing")]
    private int startSpawningBombsAfter = 4;
    [SerializeField] private float bombSpawnChanceMin = 0.1f;
    [SerializeField] private float bombSpawnChanceMax = 0.4f;

    [Header("Spawn Delay Settings")]
    [SerializeField, Tooltip("Delay between spawns at the start of the game (easier phase)")]
    private float spawnDelayStart = 1.5f;
    [SerializeField, Tooltip("Delay between spawns near the end of the game (harder phase)")]
    private float spawnDelayEnd = 0.3f;

    public int totalEggs = 100;
    private int eggsSpawned = 0;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (eggsSpawned < totalEggs)
        {
            int spawnsThisRound = DetermineSpawnCount();
            List<CatchPosition> chosenLanes = PickUniqueLanes(spawnsThisRound);

            foreach (var lane in chosenLanes)
            {
                float currentBombChance = GetScaledValue(bombSpawnChanceMin, bombSpawnChanceMax);
                bool spawnBomb = eggsSpawned > startSpawningBombsAfter && Random.value < currentBombChance;
                GameObject prefab = spawnBomb ? bombPrefab : eggPrefab;

                foreach (var spawnSet in fallingObjectSpawnSets)
                {
                    Transform spawnPoint = spawnSet.GetLane(lane);
                    GameObject obj = LeanPool.Spawn(prefab, spawnPoint.position, Quaternion.identity);
                    if (obj.TryGetComponent<FallingObject>(out var egg))
                        egg.fallLane = lane;
                }

                if (!spawnBomb) eggsSpawned++;
            }

            float delay = GetScaledValue(spawnDelayStart, spawnDelayEnd);
            yield return new WaitForSeconds(delay);

        }
    }
    private int DetermineSpawnCount()
    {
        float currentMultiLaneChance = GetScaledValue(multiLaneChanceMin, multiLaneChanceMax);

        if (eggsSpawned < enableMultiLaneSpawnAfter)
            return 1;
        else
            return Random.value < currentMultiLaneChance ? 2 : 1;
    }


    List<CatchPosition> PickUniqueLanes(int count)
    {
        var all = new List<CatchPosition> { CatchPosition.Left, CatchPosition.Middle, CatchPosition.Right };
        var result = new List<CatchPosition>();

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, all.Count);
            result.Add(all[index]);
            all.RemoveAt(index);
        }

        return result;
    }
    private float GetProgress() => Mathf.Clamp01((float)eggsSpawned / totalEggs);

    private float GetScaledValue(float min, float max)
    {
        return Mathf.Lerp(min, max, GetProgress());
    }

}
[System.Serializable]
public class FallingObjectSpawnSet
{
    public Transform left;
    public Transform middle;
    public Transform right;

    public Transform GetLane(CatchPosition lane)
    {
        switch (lane)
        {
            case CatchPosition.Left: return left;
            case CatchPosition.Middle: return middle;
            case CatchPosition.Right: return right;
            default: return middle;
        }
    }
}
public enum CatchPosition { Left, Middle, Right }