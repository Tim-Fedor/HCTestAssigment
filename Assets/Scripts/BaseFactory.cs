using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFactory : MonoBehaviour
{
    public ResourceType _resource;
    public List<ResourceType> _needsResource;
    public float _cooldown;
    public List<Storage> _inputStorages;
    public Storage _outputStorage;



    protected BaseFactory(FactoryConfig config)
    {
        _resource = config.resource;
        _cooldown = config.cooldown;
        _needsResource = config.needsResource;
        _inputStorages = new List<Storage>();
        foreach (var resource in config.needsResource)
        {
            _inputStorages.Add(new Storage(resource, config.inputStorageCapacity));
        }
        _outputStorage = new Storage(config.resource, config.outputStorageCapacity);
    }

    protected IEnumerator FactoryProcess()
    {
        yield return new WaitForSeconds(_cooldown);
        TryStartNewProcess();
        
    }
    
    protected virtual void TryStartNewProcess()
    {
        Storage currentStorage; 
        foreach (var resource in _needsResource)
        {
            currentStorage = _inputStorages.Find(x=>x.Resource == resource);
            if (currentStorage == null || currentStorage.Capacity < 1)
            {
                return;
            }
        }
     
        StartCoroutine(FactoryProcess());
    }
}
