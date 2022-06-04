using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraTarget;
    [SerializeField]
    private Vector3 _targetOffset;
    [SerializeField]
    private float _moveSpeed = 2f;

    private void LateUpdate()
    {
        if (_cameraTarget != null){
            transform.position = Vector3.Lerp(transform.position, _cameraTarget.position + _targetOffset, _moveSpeed * Time.deltaTime);
        }
    }
}
