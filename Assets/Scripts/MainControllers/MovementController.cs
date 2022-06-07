using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private Joystick _joystick;
    [SerializeField]
    private CharacterController _character;
    [SerializeField]
    private float _speed;
    private float _gravityValue = -9.81f;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;

    private void Update()
    {
        _groundedPlayer = _character.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }
        
        var move = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        _character.Move(move * (Time.deltaTime * _speed));

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _character.Move(_playerVelocity * Time.deltaTime);
    }
}
