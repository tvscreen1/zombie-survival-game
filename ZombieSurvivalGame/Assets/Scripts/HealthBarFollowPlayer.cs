using UnityEngine;

public class HealthBarFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public Camera mainCamera;

    void LateUpdate()
    {
        if (playerTransform != null && mainCamera != null)
        {
            Vector3 worldPosition = playerTransform.position + offset;
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

            // Round to the nearest pixel to avoid jitter
            screenPosition.x = Mathf.Round(screenPosition.x);
            screenPosition.y = Mathf.Round(screenPosition.y);

            transform.position = screenPosition;
        }
    }
}