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
    public Animator _animator;
    public Entity EntityStat;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
    }
  
    public void Move(InputAction.CallbackContext input)
    {
        _animator.SetTrigger("isRunning");
        rgBd.velocity = input.ReadValue<Vector2>() * speed * EntityStat.Stats[Entity.Attribute.MoveSpeedRatio].Value;
    }

}
