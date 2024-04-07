using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LifeCameraController : MonoBehaviour
{
    public PlayerInput input;

    public GameObject cameraObj;

    public bool shouldPan = true;
    public float panSpeed = 1f;
    public bool shouldTilt = true;
    public float tiltSpeed = 1f;
    public bool shouldMove = true;
    public float moveSpeed = 1f;
    public bool shouldScroll = true;
    public float scrollSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        input.actions.FindActionMap("Player").FindAction("CameraPanTilt").performed += CameraPanTilt_performed;
        input.actions.FindActionMap("Player").FindAction("CameraMove").performed += CameraMove_performed;
        input.actions.FindActionMap("Player").FindAction("CameraZoom").performed += CameraZoom_performed;

        cameraObj.transform.LookAt(transform.position);
    }

    private void OnDestroy()
    {
        input.actions.FindActionMap("Player").FindAction("CameraPanTilt").performed -= CameraPanTilt_performed;
        input.actions.FindActionMap("Player").FindAction("CameraMove").performed -= CameraMove_performed;
        input.actions.FindActionMap("Player").FindAction("CameraZoom").performed -= CameraZoom_performed;
    }

    private void CameraZoom_performed(InputAction.CallbackContext obj)
    {
        Vector2 data = obj.ReadValue<Vector2>();

        float forward = data.y;

        cameraObj.transform.localPosition = cameraObj.transform.localPosition.normalized * (cameraObj.transform.localPosition.magnitude + (forward * scrollSpeed));
    }

    private void CameraMove_performed(InputAction.CallbackContext obj)
    {
        Vector2 data = obj.ReadValue<Vector2>();

        Vector3 forward = transform.forward * data.y;
        Vector3 right = transform.right * data.x;

        transform.position += moveSpeed * (forward + right).normalized;
    }

    private void CameraPanTilt_performed(InputAction.CallbackContext obj)
    {
        Vector2 data = obj.ReadValue<Vector2>();

        if (shouldPan)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles + (data.x * panSpeed * Vector3.up));
        }
        if (shouldTilt)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles + (data.y * tiltSpeed * Vector3.right));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(cameraObj.transform.position, cameraObj.transform.forward);
    }
}
