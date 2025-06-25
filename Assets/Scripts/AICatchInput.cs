using UnityEngine;
using System.Collections.Generic;

public class AICatchInput : MonoBehaviour, ICatchInput
{
    private CatchLane currentLane = CatchLane.Middle;
    private Queue<CatchLane> laneQueue = new();
    [Header("AI Behavior")]
    public AIDataScriptableObject AIData;
    private float decisionTimer;
    public void Initialize(AIDataScriptableObject AIData)
    {
        this.AIData = AIData;
    }
    private void OnEnable()
    {
        FallingObjectSpawner.OnObjectSpawned += OnObjectSpawned;
    }

    private void OnDisable()
    {
        FallingObjectSpawner.OnObjectSpawned -= OnObjectSpawned;
    }

    private void OnObjectSpawned(CatchLane lane, bool isBomb)
    {
        if (!isBomb || Random.value < AIData.catchBombChance)
        {
            laneQueue.Enqueue(lane);
        }
    }

    public CatchLane GetLaneInput()
    {
        if (AIData == null)
            return CatchLane.Middle;
        decisionTimer += Time.deltaTime;

        if (decisionTimer >= AIData.decisionInterval && laneQueue.Count > 0)
        {
            decisionTimer = 0f;

            CatchLane target = laneQueue.Dequeue();
            bool willCatchCorrect = Random.value < AIData.catchAccuracy;

            currentLane = willCatchCorrect
                ? target
                : GetWrongLane(target);
        }

        return currentLane;
    }

    private CatchLane GetWrongLane(CatchLane correctLane)
    {
        var all = new List<CatchLane> { CatchLane.Left, CatchLane.Middle, CatchLane.Right };
        all.Remove(correctLane);
        return all[Random.Range(0, all.Count)];
    }

    public void SetLane(CatchLane catchLane)
    {
        currentLane = catchLane;
    }
}
