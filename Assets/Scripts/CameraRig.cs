using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraRig : MonoBehaviour
{
    [SerializeField] private bool useEdgeScrolling = false;
    [SerializeField] private bool useDragPan = false;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private bool isDragging = false;
    private Vector2 lastMousePos;
    private Vector3 followOffset;
    private float followOffsetMin = 5f;
    private float followOffsetMax = 20f;
    public Camera m_Camera;


    private void Awake()
    {
        followOffset = virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    private void Update()
    {
        HandleCameraMovement();
        HandleCameraRotation();
        if (useEdgeScrolling) HandleCameraMovementMousePan();
        if (useDragPan) HandleCameraMovementDragPan();

        HandleCameraZoom();
        // 
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.mousePosition);
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            Debug.Log(ray);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Renderer rend = hit.collider.gameObject.GetComponent<Renderer>();

                Debug.Log(hit.collider.gameObject.name);
                // Use the hit variable to determine what was clicked on.
            }
        }
    }

    private void HandleCameraMovement()
    {
        // WASD MOVEMENT
        Vector3 inputDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) inputDir.z += 1f;
        if (Input.GetKey(KeyCode.S)) inputDir.z -= 1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x -= 1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x += 1f;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        float moveSpeed = 5f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleCameraMovementMousePan()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);
        int edgeScrollSize = 40;
        if (Input.mousePosition.x < edgeScrollSize) inputDir.x -= 1f;
        if (Input.mousePosition.x > Screen.width - edgeScrollSize) inputDir.x += 1f;
        if (Input.mousePosition.y < edgeScrollSize) inputDir.z -= 1f;
        if (Input.mousePosition.y > Screen.height - edgeScrollSize) inputDir.z += 1f;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        float moveSpeed = 5f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleCameraMovementDragPan()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetMouseButtonDown(1))
        {
            isDragging = true;
            lastMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }
        if (isDragging)
        {
            Vector2 mouseDelta = (Vector2)Input.mousePosition - lastMousePos;
            lastMousePos = Input.mousePosition;

            float dragPanSpeed = 0.1f;
            inputDir.x = -mouseDelta.x * dragPanSpeed;
            inputDir.z = -mouseDelta.y * dragPanSpeed;
        }

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        float moveSpeed = 5f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
    private void HandleCameraRotation()
    {
        // Q, E ROTATION
        float rotateDir = 0f;
        float rotateSpeed = 50f;
        if (Input.GetKey(KeyCode.Q)) rotateDir += 1f;
        if (Input.GetKey(KeyCode.E)) rotateDir -= 1f;
        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }

    private void HandleCameraZoom()
    {
        Vector3 zoomDir = followOffset.normalized;
        float zoomAmt = 3f;
        if (Input.mouseScrollDelta.y > 0)
        {
            followOffset -= zoomDir * zoomAmt;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            followOffset += zoomDir * zoomAmt;
        }

        if (followOffset.magnitude < followOffsetMin)
        {
            followOffset = zoomDir * followOffsetMin;

        }
        if (followOffset.magnitude > followOffsetMax)
        {
            followOffset = zoomDir * followOffsetMax;

        }

        float zoomSpeed = 5f;
        virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            Vector3.Lerp(virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, followOffset, Time.deltaTime * zoomSpeed);

    }


}
