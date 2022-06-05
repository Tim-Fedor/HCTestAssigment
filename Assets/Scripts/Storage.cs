using UnityEngine;

public class Storage
{
    private int _cur;
    public int CurrentAmount
    {
        get
        {
            return _cur;
        }
        set
        {
            Debug.Log($"Trying set {value}");
            _cur = value;
        }
    }

    public ResourceType Resource { get; }

    public int Capacity { get; }

    public Storage(int capacity, ResourceType type)
    {
        Resource = type;
        Capacity = capacity;
        CurrentAmount = 0;
    }

    public void GetItemFromStorage()
    {
        
    }
    
    public void GiveItemToStorage()
    {
        
    }
}
