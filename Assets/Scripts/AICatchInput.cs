using UnityEngine;

public class AICatchInput : MonoBehaviour, ICatchInput
{
    private CatchLane targetLane = CatchLane.Middle;
    private CatchLane currentLane = CatchLane.Middle;
    private float decisionTimer;

    [Header("AI Behavior")]
    [SerializeField, Range(0f, 1f)] private float catchAccuracy = 0.85f;
    [SerializeField] private float decisionInterval = 0.25f;

    public void SetTargetLane(CatchLane lane)
    {
        targetLane = lane;
    }

    public CatchLane GetLaneInput()
    {
        decisionTimer += Time.deltaTime;

        if (decisionTimer >= decisionInterval)
        {
            decisionTimer = 0f;

            bool shouldCatch = Random.value < catchAccuracy;
            currentLane = shouldCatch
                ? targetLane
                : (CatchLane)Random.Range(0, 3);
        }

        return currentLane;
    }
}
