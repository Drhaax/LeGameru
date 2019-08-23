using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController :MonoBehaviour
{

    [SerializeField, UsedImplicitly] private Transform cameraHolder;

    [SerializeField, UsedImplicitly] private int maxZ = 0;
    [SerializeField, UsedImplicitly] private int minZ = -50;

    [SerializeField, UsedImplicitly] private int maxX = 30;
    [SerializeField, UsedImplicitly] private int minX = -30;



    [SerializeField, UsedImplicitly] private float touchZoomInSpeedMultiplier = 4;
    [SerializeField, UsedImplicitly] private float touchZoomOutSpeedMultiplier = 0.5f;


    [SerializeField, UsedImplicitly] private int zoomOutMax = 20;
    [SerializeField, UsedImplicitly] private int zoomOutMin = -20;


    [SerializeField, UsedImplicitly] private float cameraMovementFrictionLerp = 0.9f;
    [SerializeField, UsedImplicitly] private float zoomFrictionLerp = 0.93f;

    private float mouseZoomOriginY;
    private Camera mainCamera;

    private float zoomValue;
    private float zoomValueChangeSpeed;

    private Vector3 touchStart;
    public float groundY = 0;

    private Vector3 cameraMovementCurrentSpeed = Vector3.zero;
    private float cameraZoomStartValue = 0f;

    private Touch touchZero;
    private Touch touchOne;

    private bool actionNeedsGrace = false;
    private float multitouchDt = 0;
    private const float MULTITOUCH_GRACE_PERIOD_MS = 500f;

   public PlayerView player;
    
    [SerializeField, UsedImplicitly] private Vector3 followCameraPlayerOffset = new Vector3(0f, 12f, -3f);
    [SerializeField, UsedImplicitly] private float followCameraLerpSpeed = 0.1f;
    private void Start() {
        //player  //FindObjectsOfType<Agent>().FirstOrDefault(a => a.nodeMap.agentTypes[a.agentType] == PLAYER_AGENT_TYPE);
        mainCamera = GetComponent<Camera>();
        cameraZoomStartValue = 0f;
        zoomValue = -5;
    }

    void Update() {
        //if (player.isMoving ?? false) {
            HandlePlayerFollowing();
        //} 
        if (Input.touchCount == 2) {
            HandleTouchZooming();
        } else if (Input.GetMouseButton(0) && !actionNeedsGrace) {
            HandleDragging();
        } else if (Input.GetMouseButton(1)) {
            HandleMouseZooming();
        } else {
            HandleFriction();
        }

        //remove funkiness after ending multitouch by adding grace period
        if (actionNeedsGrace) {
            multitouchDt += Time.deltaTime * 1000;

            if (multitouchDt > MULTITOUCH_GRACE_PERIOD_MS) {
                actionNeedsGrace = false;
                multitouchDt = 0;
            }
        }
    }

    private void HandlePlayerFollowing() {
        var playerXZ = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        cameraHolder.position = Vector3.Lerp(cameraHolder.position, playerXZ + followCameraPlayerOffset, followCameraLerpSpeed);
        actionNeedsGrace = true;
    }

    private void HandleFriction() {
        zoomValueChangeSpeed = zoomValueChangeSpeed * zoomFrictionLerp;
        zoomValue += zoomValueChangeSpeed;
        var cameraRotationVector = mainCamera.transform.localRotation * Vector3.forward;
        mainCamera.transform.localPosition = cameraRotationVector * zoomValue;
        ClampZoom();


        cameraMovementCurrentSpeed = Vector3.Lerp(Vector3.zero, cameraMovementCurrentSpeed, cameraMovementFrictionLerp);
        cameraHolder.transform.position += cameraMovementCurrentSpeed;
        ClampCameraPosition();
    }

    private void HandleMouseZooming() {
        if (Input.GetMouseButtonDown(1)) {
            mouseZoomOriginY = Input.mousePosition.y;
            cameraZoomStartValue = zoomValue;
        } else {
            var zoomPosition = Input.mousePosition.y - mouseZoomOriginY;

            zoomPosition = zoomPosition / 300f;
            Zoom(zoomPosition);
        }
    }

    private void HandleDragging() {
        if (Input.GetMouseButtonDown(0)) {
            touchStart = GetWorldPosition(groundY);
        }

        actionNeedsGrace = false;

        Vector3 direction = touchStart - GetWorldPosition(groundY);

        cameraMovementCurrentSpeed = direction;

        cameraHolder.transform.position += direction;
        ClampCameraPosition();
    }

    private void HandleTouchZooming() {
        if (Input.GetTouch(1).phase == TouchPhase.Began) {
            touchZero = Input.GetTouch(0);
            touchOne = Input.GetTouch(1);
            cameraZoomStartValue = zoomValue;
        }


        var newTouchZero = Input.GetTouch(0);
        var newTouchOne = Input.GetTouch(1);

        float originalMagnitude = (touchZero.position - touchOne.position).magnitude;
        float currentMagnitude = (newTouchZero.position - newTouchOne.position).magnitude;

        float zoomPosition = -((currentMagnitude / originalMagnitude) - 1f);

        if (zoomPosition > 0) {
            zoomPosition *= touchZoomInSpeedMultiplier;
        } else {
            zoomPosition *= touchZoomOutSpeedMultiplier;
        }

        Zoom(-zoomPosition);

        actionNeedsGrace = true;
    }

    private Vector3 GetWorldPosition(float y) {
        Ray mousePos = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, new Vector3(0, y, 0));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }

    private void Zoom(float zoom) {
        var oldZoomValue = zoomValue;

        if (zoom < 0f) {
            zoomValue = cameraZoomStartValue + zoom * zoomOutMin;
        } else {
            zoomValue = cameraZoomStartValue + zoom * -zoomOutMax;
        }

        zoomValueChangeSpeed = zoomValue - oldZoomValue;

        var cameraRotationVector = mainCamera.transform.localRotation * Vector3.forward;
        mainCamera.transform.localPosition = cameraRotationVector * zoomValue;
        ClampZoom();
    }

    private void ClampZoom() {
        zoomValue = Mathf.Clamp(zoomValue, -zoomOutMax, -zoomOutMin);
        var cameraRotationVector = mainCamera.transform.rotation * Vector3.forward;
        mainCamera.transform.localPosition = cameraRotationVector * zoomValue;
    }

    private void ClampCameraPosition() {
        //var z = Mathf.Clamp(cameraHolder.transform.position.z, minZ, maxZ);
        //var x = Mathf.Clamp(cameraHolder.transform.position.x, minX, maxX);


        //cameraHolder.transform.position = new Vector3(x,
        //    cameraHolder.transform.position.y, z);
    }
}
