using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus;
public class AddressableAIDataLoader : MonoBehaviour
{
    public List<AIDataScriptableObject> LoadedAIDatas { get; private set; }

    public async Task LoadAllAIDatasAsync()
    {
        LoadedAIDatas = new List<AIDataScriptableObject>();

        var handle = Addressables.LoadAssetsAsync<AIDataScriptableObject>(
        "AI_Configs",
        data => LoadedAIDatas.Add(data)
        );


        await handle.Task;

        if (handle.Status == Succeeded)
        {
            Debug.Log($"Loaded {LoadedAIDatas.Count} AI data configs.");
        }
        else
        {
            Debug.LogError("Failed to load AI configs!");
        }
    }

    public AIDataScriptableObject GetRandomAI()
    {
        if (LoadedAIDatas == null || LoadedAIDatas.Count == 0) return null;
        return LoadedAIDatas[Random.Range(0, LoadedAIDatas.Count)];
    }
}
