using System.Collections.Generic;
using FactoryMechanics;
using Resource;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _backpackCapacity;

    [SerializeField] private Transform _bottomPointBackpack;

    [SerializeField] private float _offsetInBackpack;

    private List<ResourceObject> _resourcesInBackpack;

    private void Start()
    {
        _resourcesInBackpack = new List<ResourceObject>();
    }

    private void OnTriggerStay(Collider other)
    {
        var resource = other.GetComponent<ResourceObject>();
        if (resource != null && resource.State != ResourceState.Backpacked)
        {
            TryToTakeResource(resource);
        }
        else if (other.GetComponent<BaseFactory>() != null)
        {
            var factory = other.GetComponent<BaseFactory>();
            TryGiveResourceToFactory(factory);
            UpdateBackpack();
        }
    }

    private void UpdateBackpack()
    {
        for (var i = 0; i < _resourcesInBackpack.Count; i++)
        {
            var resource = _resourcesInBackpack[i];

            var finishPoint = _bottomPointBackpack.localPosition;
            if (_resourcesInBackpack.Count > 0){
                finishPoint.y = _bottomPointBackpack.localPosition.y + i * _offsetInBackpack;
            }


            resource.MoveToPoint(finishPoint, ResourceState.Backpacked, _bottomPointBackpack.localRotation.eulerAngles);
        }
    }

    private void TryToTakeResource(ResourceObject target)
    {
        if (_resourcesInBackpack.Count < _backpackCapacity)
        {
            var finishPoint = _bottomPointBackpack.localPosition;
            if (_resourcesInBackpack.Count > 0){
                finishPoint.y = _bottomPointBackpack.localPosition.y + _resourcesInBackpack.Count * _offsetInBackpack;
            }

            if (target.MoveToPoint(finishPoint, ResourceState.Backpacked, _bottomPointBackpack.localRotation.eulerAngles))
            {
                target.transform.parent = transform;
                _resourcesInBackpack.Add(target);
            }
        }
    }

    private void TryGiveResourceToFactory(BaseFactory factory)
    {
        foreach (var need in factory.Needs)
        {
            var neededResources = _resourcesInBackpack.FindAll(x => x.Type == need);
            if (neededResources.Count > 0) {
                foreach (var neededResource in neededResources) {
                    if (factory.TryToGiveResource(neededResource)) {
                        if (neededResource.MoveToPoint(factory.transform.position, ResourceState.Stay))
                        {
                            neededResource.transform.parent = transform.parent;
                            var index = _resourcesInBackpack.IndexOf(neededResource);
                            _resourcesInBackpack.Remove(neededResource);
                        }
                    }
                }
                
            }
        }
    }
}