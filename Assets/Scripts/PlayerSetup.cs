using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private bool isHuman = false;
    private ICatch catcher;
    private AddressableAIDataLoader aiDataLoader;
    private async void Awake()
    {
        gameObject.TryGetComponent(out aiDataLoader);
        catcher ??= GetComponent<ICatch>();

        if (isHuman)
        {
            catcher.SetPlayerId(PlayerRegistry.GetNextHumanId());
            catcher.AddComponent<PlayerCatchInput>();
        }
        else
        {
            catcher.SetPlayerId(PlayerRegistry.GetNextAIId());
            var aiInput = catcher.AddComponent<AICatchInput>();
            if (aiDataLoader)
            {
                await aiDataLoader.LoadAllAIDatasAsync();
                aiInput.Initialize(aiDataLoader.GetRandomAI());
            }
        }
    }
}
