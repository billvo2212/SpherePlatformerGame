using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool jumpKeyPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private bool isGrounded;

    private int score = 0;
    public TextMeshProUGUI valueText;
    public TextMeshProUGUI winMessageText;

    private bool collidedWithFinalBlock = false;

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

        winMessageText.gameObject.SetActive(false);

        //code for enemy automatic movement

        if (platform == null || grass == null)
        {
            Debug.LogError("Platform or grass is not assigned!");
            return;
        }

        // Initialize the waypoints based on the platform's size
        Vector3 platformPosition = platform.position;
        Debug.Log("Platform's position: "+platformPosition);
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
        waypoints[2] = grassPosition + new Vector3(-grassScale.x / 2, grassScale.y / 2 + 1, grassScale.z / 2);
        waypoints[3] = grassPosition + new Vector3(grassScale.x / 2, grassScale.y / 2 + 1, grassScale.z / 2);

    }//Start

    // Update is called once per frame
    void Update()
    {
        valueText.text = "Score: " + score.ToString();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key was pressed");
            jumpKeyPressed = true;
        }//if

        horizontalInput = Input.GetAxis("Horizontal");

        //code for enemy automatic movement

        if (waypoints == null || waypoints.Length == 0)
            return;

        // Move towards the current waypoint
        Vector3 targetPosition = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Checking if enemy has reached the current waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }//if
    }//Update 

    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);

        if (!isGrounded)
        {
            return;
        }//if

        if (jumpKeyPressed)
        {
            rigidbodyComponent.AddForce(Vector3.up * 6, ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }//if

    }//FixedUpdate

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (!collidedWithFinalBlock && collision.gameObject.name == "Final Grass")
        {
            collidedWithFinalBlock = true;
            winMessageText.gameObject.SetActive(true);
            winMessageText.text = "You won!";
        }//if
    }//OnCollisionEnter

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }//OnCollisionExit

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            Destroy(other.gameObject);
            score+=10;
        }//if
        else if(other.gameObject.layer == 7)
        {
            winMessageText.gameObject.SetActive(true);
            winMessageText.text = "You lose! You met an enemy!";
        }//else if
    }//OnTriggerEnter
}
