using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public Transform groundCheckTransform;
    [SerializeField] private Transform groundCheckTranform = null;
    [SerializeField] private LayerMask playerMask;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private int superJumpsRemaining = 0;


    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if space key is pressed down 
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            
            jumpKeyWasPressed = true;

        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    // FixedUpdate is called once every physic update
    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput, GetComponent<Rigidbody>().velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTranform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (jumpKeyWasPressed)
        {
            float jumpPower = 5f;
            if(superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) 
        { 
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }
}
