using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        LookAtMouse();
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        transform.position = mousePos;
    }

    private void LookAtMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.transform.up = mousePos - new Vector2(rb.transform.position.x, rb.transform.position.y);
    }
}
