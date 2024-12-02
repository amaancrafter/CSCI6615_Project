//using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem; 

public class TeleportToWaypoint : MonoBehaviour
{
    public Transform waypoint; 
    public InputAction teleportAction; 
    public GameObject player; 
    public float activationDistance = 0.5f; 

    void OnEnable()
    {
        teleportAction.Enable();
    }

    void OnDisable()
    {
        teleportAction.Disable();
    }

    void Update()
    {
        // Calculate distance between player and waypoint
        float distance = Vector3.Distance(player.transform.position, waypoint.position);

        // Allow teleportation only if within the specified distance
        if (distance <= activationDistance && teleportAction.triggered)
        {
            TeleportPlayer();
        }
    }

    void TeleportPlayer()
    {
        if (player != null && waypoint != null)
        {
            player.transform.position = waypoint.position;
            Debug.Log("Player teleported to waypoint.");
        }
        else
        {
            Debug.LogWarning("Player or waypoint is not assigned.");
        }
    }
}