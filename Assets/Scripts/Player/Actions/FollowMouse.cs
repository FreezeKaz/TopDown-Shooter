using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    // Start is called before the first frame update



    public void LookAtMouse(InputAction.CallbackContext input)
    {
        Vector3 mousePos = input.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        transform.position = mousePos;
        Vector2 mousePosToCam = Camera.main.ScreenToWorldPoint(input.ReadValue<Vector2>());
        rb.transform.up = mousePosToCam - new Vector2(rb.transform.position.x, rb.transform.position.y);
    }
}
