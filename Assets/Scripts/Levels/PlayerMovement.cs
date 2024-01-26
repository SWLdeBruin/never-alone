using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;

    [SerializeField] private float _walkingSpeed = 1f;
    [SerializeField] private float _runningSpeed = 2f;

    [SerializeField] private AudioSource _footstepAudioSource;
    [SerializeField] private AudioClip _footstepAudioTrack;

    private Vector2 _playerInput;
    private Rigidbody2D _playerRigidbody;
    private Animator _playerAnimator;

    private bool _isRunning = false;
    private float _playerSpeed;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();

        _playerSpeed = _walkingSpeed;
    }

    private void OnMove(InputValue value)
    {
        _playerInput = value.Get<Vector2>();
    }

    private void OnRun(InputValue value)
    {
        _isRunning = value.isPressed;
        _playerAnimator.SetBool("IsRunning", _isRunning);
        _playerSpeed = _isRunning ? _runningSpeed : _walkingSpeed;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(_playerInput.x, _playerInput.y, 0);
        movement = _playerCamera.transform.TransformDirection(movement);
        movement.Normalize();

        _playerRigidbody.MovePosition(transform.position + _playerSpeed * Time.fixedDeltaTime * movement);
    }

    private void Update() {
        if (_playerInput.x == 0 && _playerInput.y == 0) 
        {
            _playerAnimator.SetInteger("WalkDirection", 0);
        }
        else if (_playerInput.x > 0) 
        {
            _playerAnimator.SetInteger("WalkDirection", 3);
        }
        else if (_playerInput.x < 0)
        {
            _playerAnimator.SetInteger("WalkDirection", 2);
        }
        else if (_playerInput.y > 0)
        {
            _playerAnimator.SetInteger("WalkDirection", 1);
        }
        else if (_playerInput.y < 0)
        {
            _playerAnimator.SetInteger("WalkDirection", 4);
        }

        _playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, _playerCamera.transform.position.z);
    }

    public void PlayFootstep()
    {
        _footstepAudioSource.PlayOneShot(_footstepAudioTrack);
    }
}
