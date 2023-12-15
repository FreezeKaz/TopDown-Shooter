using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements.Experimental;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rgBd;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
    }
  
    public void Move(InputAction.CallbackContext input)
    {
      
        rgBd.velocity = input.ReadValue<Vector2>() * speed;
    }

}
