using System;
using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.XR.Interaction.Toolkit; 

public class HandController : MonoBehaviour
{
    [SerializeField] private XRRayInteractor xRRayInteractor;
    [SerializeField] private XRDirectInteractor xRDirectInteractor;
    [SerializeField] private ActionBasedController actionBasedController;
    [SerializeField] private InputActionReference teleportActionRef;

    private void OnEnable()
    {
        teleportActionRef.action.performed += TeleportModeActivate;
        teleportActionRef.action.canceled += TeleportModeCancel;
    }

    private void TeleportModeActivate(InputAction.CallbackContext obj)
    {
        xRRayInteractor.enabled = true;
        xRDirectInteractor.enabled = false;
        actionBasedController.enableInputActions = true;
    }

    private void TeleportModeCancel(InputAction.CallbackContext obj) => Invoke(nameof(DisableTeleport), 0.05f);

    private void DisableTeleport()
    {
        xRRayInteractor.enabled = false;
        xRDirectInteractor.enabled = true;
        actionBasedController.enableInputActions = false;
    }

    private void OnDisable()
    {
        teleportActionRef.action.performed -= TeleportModeActivate;
        teleportActionRef.action.canceled -= TeleportModeCancel;
    }
}
