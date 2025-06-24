using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private bool isHuman = false;
    [SerializeField] private ICatch catcher;

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
            catcher.AddComponent<AICatchInput>();
        }
    }
}
