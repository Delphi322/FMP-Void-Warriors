using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ae : MonoBehaviour
{

    #region Singleton Shit
    public static ae instance;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(instance);
    }
    #endregion

}
