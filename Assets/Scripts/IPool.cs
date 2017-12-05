using UnityEngine;

public interface IPool<T> where T : MonoBehaviour
{
    T GetObject();
    void Release(T obj);
}