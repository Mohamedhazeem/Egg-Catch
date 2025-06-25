
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private MonoBehaviour prefabProviderSource;
    private IPrefabProvider prefabProvider;
    private GameObject cachedPlayerPrefab;
    [SerializeField] private Transform[] spawnPoints;
    async void Awake()
    {
        prefabProvider = prefabProviderSource as IPrefabProvider;

        if (prefabProvider == null)
        {
            Debug.LogError("Prefab Provider is not set or doesn't implement IPrefabProvider.");
            return;
        }

        cachedPlayerPrefab = await prefabProvider.LoadPlayerAsync();
        SetupPlayers();
    }
    public void SetupPlayers()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            var spawnPoint = spawnPoints[i];

            if (i == 0)
            {
                SpawnPlayer(PlayerId.Player_1, spawnPoint);
            }
            else
            {
                var id = (PlayerId)Enum.Parse(typeof(PlayerId), $"AI_{i}");
                SpawnPlayer(id, spawnPoint);
            }
        }
    }


    void SpawnPlayer(PlayerId id, Transform spawnPoint)
    {
        var obj = Instantiate(cachedPlayerPrefab, spawnPoint.position, spawnPoint.rotation, transform);
        bool isHuman = id.ToString().StartsWith("Player");
        var playerSetup = obj.GetComponentInChildren<IPlayerSetup>();
        playerSetup.SetAsHuman(isHuman);
        var controller = obj.GetComponentInChildren<CatcherController>();
        controller.SetPlayerId(id);
    }
}
