using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public static Audio instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(transform.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }
    
}
