using UnityEngine;

[CreateAssetMenu(fileName = "AIDataScriptableObject", menuName = "AIDataScriptableObject", order = 0)]
public class AIDataScriptableObject : ScriptableObject
{
    [Header("AI Behavior")]
    [Range(0f, 1f), Tooltip("Probability that AI will choose the lane with the egg.")]
    public float catchAccuracy = 0.85f;

    [Range(0f, 1f), Tooltip("Chance that the AI will catch a bomb instead of avoiding it.")]
    public float catchBombChance = 0.2f;

    [Tooltip("Time interval (in seconds) between AI lane decision.")]
    public float decisionInterval = 0.25f;
}