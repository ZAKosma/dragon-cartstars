using UnityEngine;

public class BounceEffect : MonoBehaviour
{
    public RectTransform targetUIElement; // Assign the UI element in the inspector

    [Header("Bounce Settings")]
    public bool enableBounce = true;
    public float bounceSpeed = 2.0f; // Speed of the bounce animation
    public float bounceAmount = 10.0f; // Amount of vertical bounce
    public float bounceVariance = 0.2f; // Variance in bounce speed (adds randomness)

    [Header("Rotation Settings")]
    public bool enableRotation = true;
    public float rotationSpeed = 50.0f; // Speed of rotation
    public float rotationVariance = 5.0f; // Variance in rotation speed
    public Vector3 rotationDirection = new Vector3(0, 0, 1); // Axis of rotation

    [Header("Scale Settings")]
    public bool enableScale = true;
    public float scaleSpeed = 2.0f; // Speed of the scaling effect
    public float scaleAmount = 0.1f; // Amount of scale change
    public float scaleVariance = 0.05f; // Variance in scale amount
    public Vector3 scaleDirection = new Vector3(1, 1, 0); // Scale axis (x, y, z)

    private Vector3 initialPosition;
    private Vector3 initialScale;

    void Start()
    {
        if (targetUIElement == null)
        {
            targetUIElement = GetComponent<RectTransform>();
        }

        initialPosition = targetUIElement.anchoredPosition;
        initialScale = targetUIElement.localScale;

        // Add variance to speeds
        bounceSpeed += Random.Range(-bounceVariance, bounceVariance);
        rotationSpeed += Random.Range(-rotationVariance, rotationVariance);
        scaleAmount += Random.Range(-scaleVariance, scaleVariance);
    }

    void Update()
    {
        if (targetUIElement != null)
        {
            // Bounce effect
            if (enableBounce)
            {
                float bounceOffset = Mathf.Sin(Time.time * bounceSpeed) * bounceAmount;
                targetUIElement.anchoredPosition = new Vector3(
                    initialPosition.x,
                    initialPosition.y + bounceOffset,
                    initialPosition.z
                );
            }

            // Rotation effect
            if (enableRotation)
            {
                float rotationOffset = Mathf.Sin(Time.time * rotationSpeed);
                targetUIElement.localRotation = Quaternion.Euler(rotationDirection * rotationOffset);
            }

            // Scaling effect
            if (enableScale)
            {
                float scaleOffset = Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
                Vector3 scaleChange = Vector3.Scale(scaleDirection, new Vector3(scaleOffset, scaleOffset, scaleOffset));
                targetUIElement.localScale = initialScale + scaleChange;
            }
        }
    }
}
