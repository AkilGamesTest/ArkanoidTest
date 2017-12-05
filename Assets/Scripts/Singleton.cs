using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                T t = GameObject.FindObjectOfType<T>();
                if (t == null)
                {
                    GameObject managers = GameObject.Find("Managers");
                    if(managers == null)
                    {
                        managers = new GameObject("Managers");
                    }
                    instance = managers.AddComponent<T>();
                }
                else
                {
                    instance = t;
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            OnAwake();
        }
        else if (instance != this as T)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnAwake() { }
}