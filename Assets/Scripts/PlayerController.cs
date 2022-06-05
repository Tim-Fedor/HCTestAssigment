using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int _backpackCapacity;
    [SerializeField]
    private Vector3 _bottomPointBackpack;
    [SerializeField]
    private float _offsetInBackpack;
    private List<ResourceObject> _resourcesInBackpack;

    private void Start()
    {
        _resourcesInBackpack = new List<ResourceObject>();
    }

    private void OnTriggerStay(Collider other)
    {
        var resource = other.GetComponent<ResourceObject>();
        var factory = other.GetComponent<BaseFactory>();
        if (resource != null && resource.State != ResourceState.Backpacked)
        {
            if (_resourcesInBackpack.Count < _backpackCapacity)
            {
                var finishPoint = _bottomPointBackpack;
                if (_resourcesInBackpack.Count > 0)
                {
                    finishPoint.y = _bottomPointBackpack.y + _resourcesInBackpack.Count * _offsetInBackpack;
                }

                
                if(resource.MoveToPoint(finishPoint, ResourceState.Backpacked))
                {
                    other.transform.parent = transform;
                    _resourcesInBackpack.Add(resource);
                }
                
            }
        }else if (factory != null)
        {
            foreach (var need in factory._needsResource)
            {
                var neededResource = _resourcesInBackpack.Find(x => x.Type == need);
                if (neededResource != null)
                {
                    if(factory.TryToGiveResource(neededResource))
                    {
                        if(neededResource.MoveToPoint(factory.transform.position, ResourceState.Stay))
                        {
                            neededResource.transform.parent = factory.transform;
                            int index = _resourcesInBackpack.IndexOf(neededResource);
                            _resourcesInBackpack.Remove(neededResource);
                        }
                    }
                }
            }
        }
    }
    
    
    
}
