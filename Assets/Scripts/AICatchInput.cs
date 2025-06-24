using UnityEngine;
using System.Collections.Generic;

public class AICatchInput : MonoBehaviour, ICatchInput
{
    private CatchLane currentLane = CatchLane.Middle;
    private Queue<CatchLane> laneQueue = new();

    [Header("AI Behavior")]
    [SerializeField, Range(0f, 1f), Tooltip("Probability that AI will choose the lane with the egg.")]
    private float catchAccuracy = 0.85f;

    [SerializeField, Range(0f, 1f), Tooltip("Chance that the AI will catch a bomb instead of avoiding it.")]
    private float catchBombChance = 0.2f;

    [SerializeField, Tooltip("Time interval (in seconds) between AI lane decision.")]
    private float decisionInterval = 0.25f;


    private float decisionTimer;

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
        if (!isBomb || Random.value < catchBombChance)
        {
            laneQueue.Enqueue(lane);
        }
    }

    public CatchLane GetLaneInput()
    {
        decisionTimer += Time.deltaTime;

        if (decisionTimer >= decisionInterval && laneQueue.Count > 0)
        {
            decisionTimer = 0f;

            CatchLane target = laneQueue.Dequeue();
            bool willCatchCorrect = Random.value < catchAccuracy;

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
