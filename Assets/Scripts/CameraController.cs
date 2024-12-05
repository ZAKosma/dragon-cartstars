using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The vehicle the camera will follow
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from the target
    public float followSpeed = 10f; // Speed at which the camera follows the target
    public float rotationSpeed = 5f; // Speed at which the camera rotates to match the target
    public float lookAheadDistance = 2f; // Distance ahead of the vehicle the camera looks

    private Vector3 currentVelocity;

    private void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Camera target is not set!");
            return;
        }

        FollowTarget();
        RotateTowardsTarget();
    }

    private void FollowTarget()
    {
        // Compute the target position with offset and look-ahead
        Vector3 targetPosition = target.position + offset + target.forward * lookAheadDistance;

        // Smoothly move the camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.fixedDeltaTime);
    }

    private void RotateTowardsTarget()
    {
        // Smoothly rotate the camera to face the target
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }
}