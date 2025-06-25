using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressablePrefabProvider : MonoBehaviour, IPrefabProvider
{
    [Header("Assign Addressable Prefabs in Inspector")]
    public AssetReference eggReference;
    public AssetReference bombReference;

    public async Task<GameObject> LoadEggAsync()
    {
        return await eggReference.LoadAssetAsync<GameObject>().Task;
    }

    public async Task<GameObject> LoadBombAsync()
    {
        return await bombReference.LoadAssetAsync<GameObject>().Task;
    }
}
