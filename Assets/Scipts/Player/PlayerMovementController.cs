using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script que controla el moviment i salt del personatge

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float raycastDistance;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void Update() 
    {
        
        Jump();

    }

    private void FixedUpdate() 
    {
        Move();
    }
    
    //Es mou en la direccio entrada per Input
    private void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.fixedDeltaTime;  

        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);

        rb.MovePosition(newPosition);
    }

    //Salta si esta tocant a terra
    private void Jump()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(IsGrounded())
            {
                rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            }
        }
    }

    //Comprova si esta tocant el terra
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, raycastDistance);
    }

    
}
