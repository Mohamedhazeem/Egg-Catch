using UnityEngine;

public class PlayerSetup : MonoBehaviour, IPlayerSetup
{
    [SerializeField] private bool isHuman = false;
    private ICatch catcher;
    private AddressableAIDataLoader aiDataLoader;
    private async void Start()
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
    public void SetAsHuman(bool isHuman)
    {
        this.isHuman = isHuman;
    }

    public bool IsHuman() => isHuman;
}
