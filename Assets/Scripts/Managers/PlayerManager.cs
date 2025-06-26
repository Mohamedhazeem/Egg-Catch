
using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private MonoBehaviour prefabProviderSource;
    private IPrefabProvider prefabProvider;
    private GameObject cachedPlayerPrefab;
    [SerializeField] private Transform[] spawnPoints;
    public List<PlayerComponentsData> playerComponentsDatas = new();
    private int spawnedPlayers = 0;
    protected override async void Awake()
    {
        base.Awake();
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
        var playerSetup = obj.GetComponentsInChildren<IPlayerSetup>();
        foreach (var item in playerSetup)
        {
            item.SetAsHuman(isHuman);
        }
        var controller = obj.GetComponentInChildren<ICatch>();
        controller.SetPlayerId(id);
    }
    public (ICatchInput input, ICatch catcher) GetPlayerComponentsData(int index)
    {
        var data = playerComponentsDatas[index];
        return (data.catchInput, data.catcher);
    }


    public void SetPlayerComponentsData(ICatchInput catchInput, ICatch catcher)
    {
        PlayerComponentsData data = new PlayerComponentsData
        {
            catchInput = catchInput,
            catcher = catcher
        };

        playerComponentsDatas.Add(data);
        spawnedPlayers++;
    }

    public int GetSpawnPointCount()
    {
        return spawnPoints.Length;
    }
    public bool AreAllPlayersReady() => spawnedPlayers >= spawnPoints.Length;
}
public struct PlayerComponentsData
{
    public ICatchInput catchInput;
    public ICatch catcher;
}
