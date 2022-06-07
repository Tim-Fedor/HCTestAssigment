using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigs : MonoBehaviour
{
    public static GameConfigs Instance { get; private set; }
    public ResourcesConfig resourcesConfig;

    public ResourcesConfig ResourcesConfig
    {
        get
        {
            return resourcesConfig;
        }
    }

    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    
    
}
