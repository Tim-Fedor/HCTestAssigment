using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    [SerializeField]
    private float _movingSpeed;
    [SerializeField]
    private float _rotateSpeed;

    private bool _isMoving;
    private Vector3 _targetPoint;
    private Vector3 _targetDirection;

    private ResourceState _state;

    public ResourceState State
    {
        get
        {
            return _state;
        }
        set
        {
            Debug.Log($"Trying value {value}");
            _state = value;
        }
    }

    public ResourceType Type { get; }

    public delegate void OnStateChange();
    public event OnStateChange StateChanged;

    private void Update()
    {
        if (!_isMoving)
        {
            return;
        }
        
        transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPoint, _movingSpeed * Time.deltaTime);
        if (Vector3.Distance(_targetPoint, transform.localPosition) < 0.01f)
        {
            _isMoving = false;
            if (State == ResourceState.Stay)
            {
                Destroy(gameObject);
            }
        }
    }

    public bool MoveToPoint(Vector3 target, ResourceState newState)
    {
        if (_isMoving)
        {
            return false;
        }

        State = newState;
        StateChanged?.Invoke();
        _targetPoint = target;
        _isMoving = true;
        return true;
    }
}
