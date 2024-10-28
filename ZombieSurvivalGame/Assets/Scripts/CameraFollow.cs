using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // The player's transform
    public float smoothSpeed = 0.125f;  // The speed at which the camera follows
    public Vector3 offset;              // The offset between the camera and the player

    void LateUpdate()
    {
        if (target != null)
        {
            // Desired position is the target's position plus the offset
            Vector3 desiredPosition = target.position + offset;
            // Smoothly interpolate between current and desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // Update the camera's position
            transform.position = smoothedPosition;
        }
    }
}