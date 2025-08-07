using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    private static bool _isSearched = false;

    public static T Instance
    {
        get
        {
            if (!_isSearched)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                _isSearched = true;
                if (_instance == null)
                {
                    SetUpInstance();
                }
            }
            return _instance;
        }
    }

    private static void SetUpInstance()
    {
        _instance = (T)FindObjectOfType(typeof(T));
        if(_instance == null)
        {
            GameObject newManagerObj = new GameObject(typeof(T).Name);
            newManagerObj.transform.position = Vector3.zero;
            newManagerObj.AddComponent<T>();
            DontDestroyOnLoad(newManagerObj);
        }
    }

    //private void RemoveDuplicates()
    //{
    //    if (_instance == null)
    //    {
    //        _instance = this as T;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
