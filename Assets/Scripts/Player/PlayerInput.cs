using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    [SerializeField] Transform cameraTarget;
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] float keyboardPanSpeed = 5;
    [SerializeField] float zoomSpeed = 1;
    [SerializeField] float minZoomDistance = 7.5f;

    CinemachineFollow cinemachineFollow;
    float zoomStartTime;
    Vector3 startingFollowOffset;


    void Awake()
    {
        if (cinemachineCamera.TryGetComponent(out cinemachineFollow) == false)
        {
            Debug.LogError("Cinemachine Camera did not have CinemachineFollow. Zoom functionality will not work!");
        }

        startingFollowOffset = cinemachineFollow.FollowOffset;
    }


    void Update()
    {
        HandlePanning();
        HandleZooming();
    }


    void HandleZooming()
    {
        if (ShouldSetZoomStartTime())
        {
            zoomStartTime = Time.time;
        }

        Vector3 targetFollowOffset;
        float zoomTime = Mathf.Clamp01((Time.time - zoomStartTime) * zoomSpeed);

        if (Keyboard.current.endKey.isPressed)
        {
            targetFollowOffset = new Vector3(
                cinemachineFollow.FollowOffset.x,
                minZoomDistance,
                cinemachineFollow.FollowOffset.z
                );
        }
        else
        {
            targetFollowOffset = new Vector3(
                cinemachineFollow.FollowOffset.x,
                startingFollowOffset.y,
                cinemachineFollow.FollowOffset.z
                );
        }

        cinemachineFollow.FollowOffset = Vector3.Slerp(
            cinemachineFollow.FollowOffset,
            targetFollowOffset,
            zoomTime
            );
    }


    bool ShouldSetZoomStartTime()
    {
        return Keyboard.current.endKey.wasPressedThisFrame || Keyboard.current.endKey.wasReleasedThisFrame;
    }


    void HandlePanning()
    {
        Vector2 moveAmount = Vector2.zero;

        if (Keyboard.current.upArrowKey.isPressed)
        {
            moveAmount.y += keyboardPanSpeed;
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            moveAmount.y -= keyboardPanSpeed;
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            moveAmount.x -= keyboardPanSpeed;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            moveAmount.x += keyboardPanSpeed;
        }

        moveAmount *= Time.deltaTime;
        cameraTarget.position += new Vector3(moveAmount.x, 0, moveAmount.y);
    }
}