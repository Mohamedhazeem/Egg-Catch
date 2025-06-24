using UnityEngine;

public interface IComponent
{
    void AddComponent<T>() where T : Component;
}