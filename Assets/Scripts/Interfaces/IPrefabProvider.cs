using System.Threading.Tasks;
using UnityEngine;

public interface IPrefabProvider
{
    Task<GameObject> LoadEggAsync();
    Task<GameObject> LoadBombAsync();
}
