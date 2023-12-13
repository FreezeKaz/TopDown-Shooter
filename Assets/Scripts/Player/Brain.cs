using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.InputSystem;

public class Brain : MonoBehaviour
{

    [SerializeField] private PlayerMovement _moveComp;
    [SerializeField] private Shooting _shootingComp;
    [SerializeField] private FollowMouse _followMouseComp;

    [SerializeField] private InputActionReference _move, _shoot, _pointer;
    // Start is called before the first frame update

    private void Awake()
    {
        _move.action.started += _moveComp.Move;
        _move.action.performed += _moveComp.Move;
        _move.action.canceled += _moveComp.Move;

        _shoot.action.started += _shootingComp.EnableShoot;
        _shoot.action.canceled += _shootingComp.DisableShoot;

        _pointer.action.started += _followMouseComp.LookAtMouse;
        _pointer.action.performed += _followMouseComp.LookAtMouse;
        _pointer.action.canceled += _followMouseComp.LookAtMouse;

    }
    void Start()
    {
       
    }

    private void OnDestroy()
    {
        _move.action.started -= _moveComp.Move;
        _move.action.performed -= _moveComp.Move;
        _move.action.canceled -= _moveComp.Move;

        _shoot.action.started -= _shootingComp.EnableShoot;
        _shoot.action.canceled -= _shootingComp.DisableShoot;

        _pointer.action.started -= _followMouseComp.LookAtMouse;
        _pointer.action.performed -= _followMouseComp.LookAtMouse;
        _pointer.action.canceled -= _followMouseComp.LookAtMouse;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
