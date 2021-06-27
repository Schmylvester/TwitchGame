using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
{
    public static T instance = default;
    private void Awake()
    {
        if (instance == null)
        {
            instance = (T)(object)this;
        }
        else
        {
            Debug.LogWarning("Duplicate instance of a singleton");
            Destroy(gameObject);
        }
    }
}
