using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFactory 
{
    protected ResourceType _resource;
    protected List<ResourceType> _needsResource;
    protected float _cooldown;
    protected List<Storage> _inputStorages;
    protected Storage _outputStorage;

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
    }
}
