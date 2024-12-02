using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectTeleportInteraction : MonoBehaviour
{
    public GameObject targetObject; 
    public Transform waypoint; 
    public InputAction teleportAction; 
    public GameObject player; 
    public float activationDistance = 0.5f; 
    public FlightController flightController; 

    private void OnEnable()
    {
        teleportAction.Enable();
        DisableFlightController(); 
    }

    private void OnDisable()
    {
        teleportAction.Disable();
    }

    private void Update()
    {
        // Calculate the distance between the player and the target object
        if (targetObject != null)
        {
            float distance = Vector3.Distance(player.transform.position, targetObject.transform.position);

            // Check if the player is within the activation distance and presses the teleport button
            if (distance <= activationDistance && teleportAction.triggered)
            {
                TeleportPlayer();
            }
        }
        else
        {
            Debug.LogWarning("Target object is not assigned.");
        }
    }

    private void TeleportPlayer()
    {
        if (player != null && waypoint != null)
        {
            // Teleport the player to the waypoint
            player.transform.position = waypoint.position;
            Debug.Log("Player teleported to waypoint.");

            // Enable the FlightController script after teleportation
            EnableFlightController();
        }
        else
        {
            Debug.LogWarning("Player or waypoint is not assigned.");
        }
    }

    private void EnableFlightController()
    {
        if (flightController != null)
        {
            flightController.enabled = true;
            Debug.Log("FlightController script enabled.");
        }
        else
        {
            Debug.LogWarning("FlightController script is not assigned.");
        }
    }

    private void DisableFlightController()
    {
        if (flightController != null)
        {
            flightController.enabled = false;
            Debug.Log("FlightController script disabled.");
        }
    }
}
