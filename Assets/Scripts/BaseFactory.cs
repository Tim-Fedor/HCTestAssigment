using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFactory : MonoBehaviour
{
    public ResourceType _resource;
    public List<ResourceType> _needsResource;
    public float _cooldown;
    public List<Storage> _inputStorages;
    public Storage _outputStorage;
    public Transform _outputStorageStartPoint;
    public float _offsetForBlocks;
    public GameObject _particles;
    private bool _isWorking;

    private void Start()
    {
        _outputStorage = new Storage(5, _resource);
        _inputStorages = new List<Storage>();
        foreach (var resource in _needsResource)
        {
            _inputStorages.Add(new Storage(3, resource));
        }
        TryStartNewProcess();
    }

    private IEnumerator FactoryProcess()
    {
        _particles.SetActive(true);
        _isWorking = true;
        yield return new WaitForSeconds(_cooldown);
        var pair = GameConfigs.Instance?.ResourcesConfig?.allResources?.Find(x => x.type == _resource);
        if (pair != null && pair.prefab != null)
        {
            TryCreateNewResource(pair.prefab);
            
        }
        TryStartNewProcess();
        
    }

    private void TryCreateNewResource(GameObject prefab)
    {
        var newResource = Instantiate(prefab, transform.position, transform.rotation);
        var resource = newResource.GetComponent<ResourceObject>();
        if (resource != null)
        {
            PayNeeds();
            var finalPoint = _outputStorageStartPoint.position;
            if (_outputStorage.CurrentAmount > 0)
            {
                finalPoint.y = finalPoint.y + _outputStorage.CurrentAmount * _offsetForBlocks;
            }
            resource.MoveToPoint(finalPoint, ResourceState.Storaged);
            resource.StateChanged += OnGiveResource;
            _outputStorage.CurrentAmount++;
        }
    }
    
    private void PayNeeds()
    {
        foreach (var resource in _needsResource)
        {
            var currentStorage = _inputStorages.Find(x=>x.Resource == resource);
            if (currentStorage != null)
            {
                currentStorage.CurrentAmount--;
            }
        }
    }
    
    protected virtual void TryStartNewProcess()
    {
        foreach (var resource in _needsResource)
        {
            var currentStorage = _inputStorages.Find(x=>x.Resource == resource);
            if (currentStorage == null || currentStorage.CurrentAmount < 1)
            {
                _particles.SetActive(false);
                _isWorking = false;
                return;
            }
        }

        if (_outputStorage.CurrentAmount >= _outputStorage.Capacity)
        {
            _particles.SetActive(false);
            _isWorking = false;
            return;
        }
        StartCoroutine(FactoryProcess());
    }

    private void OnGiveResource(ResourceObject resource)
    {
        if(resource != null){
            resource.StateChanged -= OnGiveResource;
        }
        _outputStorage.CurrentAmount--;
        if(!_isWorking){
            TryStartNewProcess();
        }
    }

    public bool TryToGiveResource(ResourceObject resource)
    {
        var storage = _inputStorages.Find(x => x.Resource == resource.Type);
        if (storage != null && storage.CurrentAmount < storage.Capacity)
        {
            storage.CurrentAmount++;
            if(!_isWorking){
                TryStartNewProcess();
            }
            return true;
        }

        return false;
    }
}
