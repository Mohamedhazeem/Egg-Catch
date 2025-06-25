using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class FallingObjectSpawner : MonoBehaviour
{
    public List<FallingObjectSpawnSet> fallingObjectSpawnSets;
    [SerializeField] private MonoBehaviour prefabProviderSource;
    private IPrefabProvider prefabProvider;
    private GameObject cachedEggPrefab;
    private GameObject cachedBombPrefab;


    [Header("Spawn Behavior Settings")]
    [SerializeField] private float initialSpawnDelay = 1f;
    [Tooltip("Start allowing 2-lane spawns after this many eggs have been spawned")]
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


    [Header("Falling Objects Setting")]
    public int totalFallingObjectsToSpawn = 100;
    [SerializeField, Tooltip("Delay for show winner")]
    private float delayForShowWinner = 1.5f;
    private int spawnedFallingObjects = 0;
    public static event Action<CatchLane, bool> OnObjectSpawned;
    private IRemainingFallingObjectCounterUI remainingFallingObjectCounterUI;
    async void Start()
    {
        prefabProvider = prefabProviderSource as IPrefabProvider;

        if (prefabProvider == null)
        {
            Debug.LogError("Prefab Provider is not set or doesn't implement IPrefabProvider.");
            return;
        }

        cachedEggPrefab = await prefabProvider.LoadEggAsync();
        cachedBombPrefab = await prefabProvider.LoadBombAsync();

        remainingFallingObjectCounterUI = UIManager.Instance.GetUI<IRemainingFallingObjectCounterUI>(UITypes.ObjectSpawnLeftOverCounterUI);
        remainingFallingObjectCounterUI.SetRemainingFallingObjectCounterText(totalFallingObjectsToSpawn.ToString());
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        while (spawnedFallingObjects < totalFallingObjectsToSpawn)
        {
            int spawnsThisRound = DetermineSpawnCount();
            List<CatchLane> chosenLanes = PickUniqueLanes(spawnsThisRound);
            bool hasSpawnedEgg = false;

            foreach (var lane in chosenLanes)
            {
                bool isBomb = false;
                if (!hasSpawnedEgg && spawnedFallingObjects > startSpawningBombsAfter)
                {
                    float bombChance = GetScaledValue(bombSpawnChanceMin, bombSpawnChanceMax);
                    isBomb = UnityEngine.Random.value < bombChance;
                }
                else
                {
                    isBomb = true;
                }

                if (spawnedFallingObjects <= startSpawningBombsAfter)
                    isBomb = false;

                GameObject prefab = isBomb ? cachedBombPrefab : cachedEggPrefab;
                OnObjectSpawned?.Invoke(lane, isBomb);

                foreach (var spawnSet in fallingObjectSpawnSets)
                {
                    Transform spawnPoint = spawnSet.GetLane(lane);
                    GameObject obj = LeanPool.Spawn(prefab, spawnPoint.position, Quaternion.identity);
                    if (obj.TryGetComponent<FallingObject>(out var falling))
                        falling.fallLane = lane;
                }

                if (!isBomb)
                {
                    ++spawnedFallingObjects;
                    remainingFallingObjectCounterUI.UpdateRemainingFallingObjectCounterText(RemainingFallingObjects().ToString());
                    hasSpawnedEgg = true;
                }
            }

            float delay = GetScaledValue(spawnDelayStart, spawnDelayEnd);
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(delayForShowWinner);
        ScoreManager.Instance.ShowWinnerAndLosers();
    }

    private int DetermineSpawnCount()
    {
        float currentMultiLaneChance = GetScaledValue(multiLaneChanceMin, multiLaneChanceMax);

        if (spawnedFallingObjects < enableMultiLaneSpawnAfter)
            return 1;
        else
            return UnityEngine.Random.value < currentMultiLaneChance ? 2 : 1;
    }


    List<CatchLane> PickUniqueLanes(int count)
    {
        var all = new List<CatchLane> { CatchLane.Left, CatchLane.Middle, CatchLane.Right };
        var result = new List<CatchLane>();

        for (int i = 0; i < count; i++)
        {
            int index = UnityEngine.Random.Range(0, all.Count);
            result.Add(all[index]);
            all.RemoveAt(index);
        }

        return result;
    }
    public int RemainingFallingObjects() => totalFallingObjectsToSpawn - spawnedFallingObjects;

    private float GetProgress() => Mathf.Clamp01((float)spawnedFallingObjects / totalFallingObjectsToSpawn);

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

    public Transform GetLane(CatchLane lane)
    {
        switch (lane)
        {
            case CatchLane.Left: return left;
            case CatchLane.Middle: return middle;
            case CatchLane.Right: return right;
            default: return middle;
        }
    }
}
public enum CatchLane { Left, Middle, Right }