using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The vehicle the camera will follow
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from the target
    public float followSpeed = 10f; // Speed at which the camera follows the target
    public float rotationSpeed = 5f; // Speed at which the camera rotates to match the target

    private void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Camera target is not set!");
            return;
        }

        FollowAndRotateWithTarget();
    }

    private void FollowAndRotateWithTarget()
    {
        // Calculate the desired position relative to the target's rotation
        Vector3 desiredPosition = target.TransformPoint(offset);

        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.fixedDeltaTime);

        // Smoothly rotate the camera to align with the target's rotation
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.fixedDeltaTime);
    }
}