using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Singleton class that gives access to all the actions implemented in the input action system
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    /// <summary>
    /// Input action asset
    /// </summary>
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Multiple instances of {GetType().Name} present {transform} - {Instance}");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Create and enable the input actions asset
        playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Enable();
    }

    /**
    *    Mouse related actions and inputs
    */

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Vector2 GetMouseScreenPosition() => Mouse.current.position.ReadValue();

    public Vector2 GetCameraMoveVector() => playerInputActions.Player.CameraMovement.ReadValue<Vector2>();

    public bool IsLeftMouseButtonDownThisFrame() => playerInputActions.Player.LeftClick.WasPressedThisFrame();

    public bool IsRightMouseButtonDownThisFrame() => playerInputActions.Player.RightClick.WasPressedThisFrame();

    public float GetCameraRotationAmount() => playerInputActions.Player.CameraRotation.ReadValue<float>();

    public float GetCameraZoomAmount() => playerInputActions.Player.CameraZoom.ReadValue<float>();

}
