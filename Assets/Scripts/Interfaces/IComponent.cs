using UnityEngine;

public interface IComponent
{
    T AddComponent<T>() where T : Component;
}