using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private bool isHuman = false;
    private ICatch catcher;
    public AIDataScriptableObject[] AIDatas;
    private void Awake()
    {
        if (catcher == null)
            catcher = GetComponent<ICatch>();

        if (isHuman)
        {
            catcher.SetPlayerId(PlayerRegistry.GetNextHumanId());
            catcher.AddComponent<PlayerCatchInput>();
        }
        else
        {
            catcher.SetPlayerId(PlayerRegistry.GetNextAIId());
            var aiInput = catcher.AddComponent<AICatchInput>();
            AIDataScriptableObject randomData = AIDatas[Random.Range(0, AIDatas.Length)];
            aiInput.Initialize(randomData);
        }
    }
}
