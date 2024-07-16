using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 0.5f;    // Speed of the enemy movement
    private Vector3[] waypoints;  // Array to store waypoints at the edges of the platform
    private int currentWaypointIndex = 0;  // Current waypoint index

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the waypoints
        waypoints = new Vector3[2];
        waypoints[0] = new Vector3(522.6f, 367.8f, 0.0f);
        waypoints[1] = new Vector3(531.83f, 367.8f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
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
        }
    }
}
