using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputActionReference move, look, fire;
    public Rigidbody2D rgBd;
    private Vector2 moveInput;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        // moveInput.x = Input.  
    }

   

    private void Move()
    {
        
        Console.Write("I'm moving");
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rgBd.velocity = input.normalized * speed;
    }

}
