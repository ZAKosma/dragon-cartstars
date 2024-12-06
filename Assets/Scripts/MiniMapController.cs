using System;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public Transform target; // The object the camera will follow
    public GameObject miniMapRoot;
    public GameObject miniMapUIRoot;
    private float fixedYPosition;
    private Quaternion fixedRotation;
    private Camera miniMapCamera;

    private void Start()
    {
        miniMapCamera = GetComponent<Camera>();
        
        fixedYPosition = transform.position.y;
        fixedRotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            miniMapRoot.SetActive(!miniMapRoot.activeSelf);
            miniMapUIRoot.SetActive(!miniMapUIRoot.activeSelf);
            miniMapCamera.enabled = !miniMapCamera.isActiveAndEnabled;
        }
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("MiniMapController target is not set!");
            return;
        }

        FollowTarget();
    }

    private void FollowTarget()
    {
        // Set the camera's position instantly to follow the target
        transform.position = new Vector3(target.position.x, fixedYPosition, target.position.z);

        // Set the camera's rotation instantly to the fixed rotation
        transform.rotation = fixedRotation;
    }
}
