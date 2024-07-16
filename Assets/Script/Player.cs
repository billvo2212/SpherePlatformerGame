using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public Transform groundCheckTransform;
    [SerializeField] private Transform groundCheckTranform = null;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] GameOverScript GameOverScript;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private int superJumpsRemaining = 0;
    private int score = 0;


    //variables for enemy movement
    public Transform platform;  // Reference to the platform the enemy is on
    public Transform grass;
    public float speed = 0.5f;    // Speed of the enemy movement
    private Vector3[] waypoints;  // Array to store waypoints at the edges of the platform
    private int currentWaypointIndex = 0;  // Current waypoint index

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();

        // Initialize the waypoints based on the platform's size
        Vector3 platformPosition = platform.position;
        Debug.Log("Platform's position: " + platformPosition);
        Vector3 platformScale = platform.localScale;
        Vector3 grassPosition = grass.position;
        Vector3 grassScale = grass.localScale;



        waypoints = new Vector3[4];
        //waypoints[0] = platformPosition + new Vector3(platformScale.x / 2, 0, -platformScale.z / 2);   // Bottom-right
        //waypoints[1] = platformPosition + new Vector3(-platformScale.x / 2, 0, -platformScale.z / 2);  // Bottom-left
        //waypoints[2] = platformPosition + new Vector3(-platformScale.x / 2, 0, platformScale.z / 2);   // Top-left
        //waypoints[3] = platformPosition + new Vector3(platformScale.x / 2, 0, platformScale.z / 2);    // Top-right

        waypoints[0] = grassPosition + new Vector3(grassScale.x / 2, grassScale.y / 2 + 1, -grassScale.z / 2);
        waypoints[1] = grassPosition + new Vector3(-grassScale.x / 2, grassScale.y / 2 + 1, -grassScale.z / 2);
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

        if (transform.position.y < 330)
        {
            GameOverScript.Setup(score);
        }
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
                ScoreManager.instance.updateSuperJump(superJumpsRemaining);
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
            score++;
            superJumpsRemaining++;
            ScoreManager.instance.updateSuperJump(superJumpsRemaining);
            ScoreManager.instance.AddScore(1);
        }
        
        if (other.gameObject.layer == 8)
        {
            //winMessageText.gameObject.SetActive(true);
            //winMessageText.text = "You lose! You met an enemy!";
            Debug.Log("Touch Enermy");
            GameOverScript.Setup(score);
        }
    }

   
}
