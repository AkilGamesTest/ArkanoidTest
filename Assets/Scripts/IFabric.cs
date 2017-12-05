using UnityEngine;

public interface IFabric<T> where T: MonoBehaviour
{
    T Create();
}