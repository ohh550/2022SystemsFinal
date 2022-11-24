using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReganAlchemy
{
    [RequireComponent(typeof(CharacterController))]
    public class TopDownPlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        float _movementSpeed = 10;
        [SerializeField]
        float _movementAcceleration = 10;
        [SerializeField]
        float _StopThreshold = 0.02f;
        [SerializeField]
        float _movementDrag = 1;

        [Header("Physics")]
        [SerializeField]
        float _gravity = 10;
        [SerializeField]
        float _fallDrag = 0.02f;

        bool _movementEnabled = true;

        CharacterController _characterController;

        Vector2 _movementInput = new();
        Vector3 _velocity = new();

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            HandleInput();
            HandleGravity();
            HandleMovement();
        }

        private void HandleInput()
        {
            if (!_movementEnabled) return;

            if (Input.GetKey(KeyCode.W))
            {
                _movementInput.y++;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _movementInput.y--;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _movementInput.x++;
            }
            if (Input.GetKey(KeyCode.A))
            {
                _movementInput.x--;
            }

            _movementInput = _movementInput.normalized;

            _velocity = new Vector3(
                Mathf.Clamp(_velocity.x + _movementInput.x * _movementAcceleration * Time.deltaTime, -_movementSpeed, _movementSpeed),
                _velocity.y,
                Mathf.Clamp(_velocity.z + _movementInput.y * _movementAcceleration * Time.deltaTime, -_movementSpeed, _movementSpeed));
        }

        private void HandleGravity()
        {
            if (!_characterController.isGrounded)
            {
                _velocity.y -= _gravity * Time.deltaTime;
                return;
            }
            _velocity.y = -0.1f;
        }

        private void HandleMovement()
        {
            _velocity.x -= Mathf.Sign(_velocity.x) * _movementDrag * _velocity.x * _velocity.x * Time.deltaTime;
            //_velocity.y -= Mathf.Sign(_velocity.y) * _fallDrag * _velocity.y * _velocity.y * Time.deltaTime;
            _velocity.z -= Mathf.Sign(_velocity.z) * _movementDrag * _velocity.z * _velocity.z * Time.deltaTime;

            if (Mathf.Abs(_velocity.x) < _StopThreshold && _movementInput.x == 0) { _velocity.x = 0; }

            if (Mathf.Abs(_velocity.z) < _StopThreshold && _movementInput.y == 0) { _velocity.z = 0; }

            _characterController.Move(_velocity * Time.deltaTime);

            _movementInput = new();
        }
    }
}