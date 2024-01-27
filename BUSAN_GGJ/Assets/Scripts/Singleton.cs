using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour 
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    obj.TryGetComponent(out instance);
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        Set_Instance();
    }

    private void Set_Instance()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }

    protected void Set_Instance2()
    {
        if (instance == null)
        {
            instance = this as T;
        }
    }
}
