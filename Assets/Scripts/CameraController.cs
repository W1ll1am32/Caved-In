using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform player;
    public float smoothSpeed = 20f;
    public Vector3 offset = new Vector3(0, 0.5f, 0); // Default standing height offset
    public float lookSpeed = 2.0f;
    public float lookXLimit = 90.0f;

    private float rotationX = 0;
    private float rotationY = 0;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate() {
        // Smoothly follow player position
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);

        // Handle camera rotation (pitch and yaw)
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        rotationY += Input.GetAxis("Mouse X") * lookSpeed;

        // Apply pitch to camera and yaw to both camera and player
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        player.rotation = Quaternion.Euler(0, rotationY, 0); // Sync player's yaw with camera
    }

    // Call this from Player script to update offset during crouching
    public void UpdateOffset(Vector3 newOffset) {
        offset = newOffset;
    }
}