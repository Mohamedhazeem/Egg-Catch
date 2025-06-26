using UnityEngine;
using System.Collections.Generic;

public class AICatchInput : MonoBehaviour, ICatchInput
{
    private CatchLane currentLane = CatchLane.Middle;
    private Queue<CatchLane> eggQueue = new();     // For eggs
    private Queue<CatchLane> bombQueue = new();    // For bombs

    [Header("AI Behavior")]
    public AIDataScriptableObject AIData;
    public bool hasCommittedLane = false;
    private ICatchAnimation catchAnimation;

    public void Initialize(AIDataScriptableObject AIData)
    {
        this.AIData = AIData;
    }

    private void OnEnable()
    {
        FallingObjectSpawner.OnObjectsSpawnedGroup += OnObjectsSpawnedGroup;
    }

    private void OnDisable()
    {
        FallingObjectSpawner.OnObjectsSpawnedGroup -= OnObjectsSpawnedGroup;
    }

    private void Start()
    {
        catchAnimation = GetComponent<ICatchAnimation>();
    }

    // private void OnObjectsSpawnedGroup(List<(CatchLane lane, bool isBomb)> group)
    // {
    //     if (catchAnimation.IsStun())
    //     {
    //         eggQueue.Clear();
    //         bombQueue.Clear();
    //         ResetLaneCommitment();
    //         return;
    //     }
    //     foreach (var obj in group)
    //     {
    //         if (!obj.isBomb)
    //         {
    //             eggQueue.Enqueue(obj.lane);
    //             return;
    //         }
    //     }

    //     bool shouldCatchBomb = Random.value < AIData.catchBombChance;
    //     if (shouldCatchBomb)
    //     {
    //         bombQueue.Enqueue(group[Random.Range(0, group.Count)].lane);
    //     }
    // }
    private void OnObjectsSpawnedGroup(List<(CatchLane lane, bool isBomb)> group)
    {
        if (catchAnimation.IsStun())
        {
            eggQueue.Clear();
            bombQueue.Clear();
            ResetLaneCommitment();
            return;
        }

        List<CatchLane> eggs = new();
        List<CatchLane> bombs = new();

        foreach (var obj in group)
        {
            if (obj.isBomb)
                bombs.Add(obj.lane);
            else
                eggs.Add(obj.lane);
        }

        bool tryCatchBomb = Random.value < AIData.catchBombChance;

        if (tryCatchBomb && bombs.Count > 0)
        {
            bombQueue.Enqueue(bombs[Random.Range(0, bombs.Count)]);
        }

        if (eggs.Count > 0)
        {
            eggQueue.Enqueue(eggs[Random.Range(0, eggs.Count)]);
        }
    }


    public CatchLane GetLaneInput()
    {
        if (AIData == null) return CatchLane.Middle;
        if (hasCommittedLane) return currentLane;
        bool tryBomb = bombQueue.Count > 0 && Random.value < AIData.catchBombChance;
        bool tryEgg = eggQueue.Count > 0 && Random.value < AIData.catchAccuracy;

        if (tryBomb)
        {
            currentLane = bombQueue.Dequeue();
            hasCommittedLane = true;
        }
        else if (tryEgg)
        {
            currentLane = eggQueue.Dequeue();
            hasCommittedLane = true;
        }
        else if (eggQueue.Count > 0)
        {

            currentLane = eggQueue.Dequeue();
            hasCommittedLane = true;
        }
        else if (bombQueue.Count > 0)
        {
            currentLane = bombQueue.Dequeue();
            hasCommittedLane = true;
        }
        return currentLane;
    }
    public void SetLane(CatchLane catchLane)
    {
        currentLane = catchLane;
    }

    public void ResetLaneCommitment()
    {
        hasCommittedLane = false;
        Debug.Log("AI ResetLaneCommitment");
    }
}
