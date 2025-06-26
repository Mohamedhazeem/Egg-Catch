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
            var input = catcher.AddComponent<PlayerCatchInput>();
            PlayerManager.Instance.SetPlayerComponentsData(input, catcher);
            catcher.AddComponent<PlayerUIInput>();
        }
        else
        {
            catcher.SetPlayerId(PlayerRegistry.GetNextAIId());
            var aiInput = catcher.AddComponent<AICatchInput>();
            PlayerManager.Instance.SetPlayerComponentsData(aiInput, catcher);
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
