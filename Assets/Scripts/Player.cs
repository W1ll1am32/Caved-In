using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour {
    public float walkingSpeed = 3.0f;
    public float runningSpeed = 5.0f;
    public float crouchingSpeed = 1.0f;
    public float jumpSpeed = 6.0f;
    public float gravity = 9.18f;
    public Camera playerCamera;
    public float standHeight = 1.75f;
    public float crouchHeight = 0.5f;
    public float crouchTime = 0.25f;
    public Vector3 standCenter = Vector3.zero;
    public Vector3 crouchCenter = new Vector3(0, 0.4f, 0);

    CharacterController characterController;
   
    public SphereCollider Sound;
    public Light Lantern;

    public Vector3 moveDirection = Vector3.zero;

    public bool isRunning = false;

    public bool crouchAnimation = false;
    public bool isCrouching = false;

    [HideInInspector]
    public bool canMove = true;

    [HideInInspector]
    public float currentSpeed = 0.0f;

    bool lightUp = false;
    float inputV = 0;
    float inputH = 0;

    void Start() {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            lightUp = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !crouchAnimation) {
            StartCoroutine(CrouchStand());
        }

        isRunning = characterController.isGrounded ? Input.GetKey(KeyCode.LeftShift) : isRunning;

        inputV = Input.GetAxis("Vertical");
        inputH = Input.GetAxis("Horizontal");
    }

    void FixedUpdate() {
        if (lightUp) {
            Lantern.intensity = Mathf.Clamp(Lantern.intensity + 1, 0, 5);
            lightUp = false;
        } else {
            Lantern.intensity = Mathf.Clamp(Lantern.intensity - Time.fixedDeltaTime, 0, 5);
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float verticalVelocity = moveDirection.y;

        currentSpeed = isCrouching ? (!characterController.isGrounded ? currentSpeed : crouchingSpeed) : (isRunning ? runningSpeed : walkingSpeed);
        moveDirection = currentSpeed * Vector3.ClampMagnitude((forward * inputV) + (right * inputH), 1.0f);

        if (!characterController.isGrounded && !crouchAnimation) {
            verticalVelocity -= gravity * Time.fixedDeltaTime;
        } else {
            verticalVelocity = -3.8f;
        }

        moveDirection.y = verticalVelocity;

        characterController.Move(moveDirection * Time.fixedDeltaTime);
        
        if (moveDirection.x != 0 && moveDirection.z != 0) {
            Sound.radius = isCrouching ? 3 : (isRunning ? 20 : 5);
        } else {
            Sound.radius = 1;
        }
    }

    public CameraController cameraFollow;

    IEnumerator CrouchStand() {
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f)) yield break;

        crouchAnimation = true;

        float passedTime = 0;
        float targetHeight = isCrouching ? standHeight : crouchHeight;
        float currentHeight = characterController.height;

        Vector3 targetCenter = isCrouching ? standCenter : crouchCenter;
        Vector3 currentCenter = characterController.center;
        Vector3 targetCameraOffset = new Vector3(0, 0.5f, 0);

        while (passedTime < crouchTime) {
            passedTime += Time.fixedDeltaTime;
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, Mathf.Clamp01(passedTime / crouchTime));
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, Mathf.Clamp01(passedTime / crouchTime));
            cameraFollow.UpdateOffset(Vector3.Lerp(new Vector3(0, currentHeight * 0.5f, 0), targetCameraOffset, Mathf.Clamp01(passedTime / crouchTime)));
            yield return new WaitForFixedUpdate();
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;
        cameraFollow.UpdateOffset(targetCameraOffset);

        isCrouching = !isCrouching;
        crouchAnimation = false;
        if (!isCrouching) {
            characterController.Move(new Vector3(0, 10f, 0) * Time.fixedDeltaTime);
        }
    }
}