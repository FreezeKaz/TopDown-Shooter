using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    // Start is called before the first frame update
    private KeyCode dashKey = KeyCode.LeftShift;

    [Header("Dash Characteristic")] public float dashSpeed = 15f;

    private PlayerMovement _playerMovement;
    float keepSpeed;

    private void Start()
    {
        Console.WriteLine("heyp0");
        _playerMovement = GetComponent<PlayerMovement>();
        keepSpeed = _playerMovement.speed;
    }

    private void Update()
    {
        Console.WriteLine("heyp0");
        if (Input.GetButtonDown("Fire3"))
        {
            Dashing();
            Console.WriteLine("heyp0");
        }
        else _playerMovement.speed = keepSpeed;

    }

    private void Dashing()
    {
        _playerMovement.speed = dashSpeed;
    }
}
