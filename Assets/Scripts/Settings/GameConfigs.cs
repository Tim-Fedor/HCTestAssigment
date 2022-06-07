using UnityEngine;

public class GameConfigs : MonoBehaviour
{
    public ResourcesConfig resourcesConfig;
    public static GameConfigs Instance { get; private set; }

    public ResourcesConfig ResourcesConfig => resourcesConfig;

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