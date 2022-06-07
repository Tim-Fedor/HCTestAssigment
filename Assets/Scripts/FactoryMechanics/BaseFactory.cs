using System.Collections;
using System.Collections.Generic;
using Resource;
using UnityEngine;

namespace FactoryMechanics
{
    public class BaseFactory : MonoBehaviour
    {
        [SerializeField] 
        private ResourceType _resource;
        [SerializeField] 
        private List<ResourceType> _needsResource;
        [SerializeField] 
        private float _cooldown;
        [SerializeField] 
        private Transform _outputStorageStartPoint;
        [SerializeField] 
        private float _offsetForBlocks;
        [SerializeField] 
        private int _storageCapacity = 5;
        [SerializeField] 
        private int _needsCapacity = 3;
        [SerializeField] 
        private GameObject _particles;

        private bool _isWorking;

        public List<ResourceType> Needs => _needsResource;

        public Storage OutputStorage { get; private set; }

        public List<Storage> InputStorages { get; private set; }

        private void Start()
        {
            OutputStorage = new Storage(_storageCapacity, _resource);
            InputStorages = new List<Storage>();
            foreach (var resource in _needsResource)
            {
                InputStorages.Add(new Storage(_needsCapacity, resource));
            }
            TryStartNewProcess();
        }

        protected virtual void TryStartNewProcess()
        {
            if (!CheckIfHaveAllNeeds() || !CheckIfEnoughOutputSpace())
            {
                StopWorking();
                return;
            }
            StartCoroutine(FactoryProcess());
        }

        private IEnumerator FactoryProcess()
        {
            StartWorking();
            yield return new WaitForSeconds(_cooldown);
            var resourceInfo = GameConfigs.Instance?.ResourcesConfig?.allResources?.Find(x => x.type == _resource);
            if (resourceInfo != null && resourceInfo.prefab != null)
            {
                TryCreateNewResource(resourceInfo.prefab);
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
                MoveResourceToOutput(resource);
                resource.StateChanged += OnGiveResource;
                OutputStorage.CurrentAmount++;
            }
        }

        private void MoveResourceToOutput(ResourceObject target)
        {
            var finalPoint = _outputStorageStartPoint.position;
            if (OutputStorage.CurrentAmount > 0){
                finalPoint.y = finalPoint.y + OutputStorage.CurrentAmount * _offsetForBlocks;
            }
            target.MoveToPoint(finalPoint, ResourceState.Storaged);
        }


        private void PayNeeds()
        {
            foreach (var resource in _needsResource)
            {
                var currentStorage = InputStorages.Find(x => x.Resource == resource);
                if (currentStorage != null)
                {
                    currentStorage.CurrentAmount--;
                }
            }
        }


        private void OnGiveResource(ResourceObject resource)
        {
            if (resource != null)
            {
                resource.StateChanged -= OnGiveResource;
            }
            OutputStorage.CurrentAmount--;
            if (!_isWorking)
            {
                TryStartNewProcess();
            }
        }

        public bool TryToGiveResource(ResourceObject resource)
        {
            var storage = InputStorages.Find(x => x.Resource == resource.Type);
            if (storage != null && storage.CurrentAmount < storage.Capacity)
            {
                storage.CurrentAmount++;
                if (!_isWorking) TryStartNewProcess();
                return true;
            }

            return false;
        }

        private void StopWorking()
        {
            _particles.SetActive(false);
            _isWorking = false;
        }

        private void StartWorking()
        {
            _particles.SetActive(true);
            _isWorking = true;
        }

        private bool CheckIfEnoughOutputSpace()
        {
            return OutputStorage.CurrentAmount >= OutputStorage.Capacity;
        }

        private bool CheckIfHaveAllNeeds()
        {
            foreach (var resource in _needsResource)
            {
                var currentStorage = InputStorages.Find(x => x.Resource == resource);
                if (currentStorage == null || currentStorage.CurrentAmount < 1) return false;
            }

            return true;
        }
    }
}