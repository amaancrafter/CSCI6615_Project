using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FlightController : MonoBehaviour
{
    public ActionBasedContinuousMoveProvider continuousMoveProvider;
    private bool isFlying = false;

    private InputDevice leftHandDevice;
    private float buttonCooldown = 0.5f; 
    private float lastButtonPressTime = 0f;

    // Configurable properties for flight mode
    public float flightModeSpeed = 10f;
    public float normalMoveSpeed = 4f;
    public Transform flightForwardSource; 

    private void Start()
    {
        InitializeControllers();

        if (continuousMoveProvider == null)
        {
            Debug.LogError("ContinuousMoveProvider reference is missing. Please assign it in the Inspector.");
        }
    }

    private void InitializeControllers()
    {
        var leftHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);

        if (leftHandDevices.Count > 0)
        {
            leftHandDevice = leftHandDevices[0];
        }

        if (!leftHandDevice.isValid)
        {
            Debug.LogError("Left-hand controller is not valid. Ensure it is connected.");
        }
    }

    private void Update()
    {
        if (!leftHandDevice.isValid)
        {
            InitializeControllers();
        }

        if (Time.time - lastButtonPressTime > buttonCooldown &&
            leftHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool xButtonPressed) && xButtonPressed)
        {
            lastButtonPressTime = Time.time;
            ToggleFlightMode();
        }
    }

    private void ToggleFlightMode()
    {
        isFlying = !isFlying;

        if (isFlying)
        {
            Debug.Log("Flight mode activated!");

            // Enable flight mode
            continuousMoveProvider.enableFly = true;
            continuousMoveProvider.useGravity = false;

            // Update move speed and forward source
            continuousMoveProvider.moveSpeed = flightModeSpeed;

            if (flightForwardSource != null)
            {
                continuousMoveProvider.forwardSource = flightForwardSource;
            }
        }
        else
        {
            Debug.Log("Flight mode deactivated!");

            // Disable flight mode
            continuousMoveProvider.enableFly = false;
            continuousMoveProvider.useGravity = true;

            // Reset move speed and forward source to default
            continuousMoveProvider.moveSpeed = normalMoveSpeed;

            continuousMoveProvider.forwardSource = Camera.main.transform; // Default to Main Camera
        }
    }
}
