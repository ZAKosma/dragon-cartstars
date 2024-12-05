using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    // Vehicle settings
    public float acceleration = 5f;
    public float deceleration = 5f;
    public float maxSpeed = 20f;
    public float reverseSpeed = -10f;
    public float baseSteeringSpeed = 50f;
    public float maxSteeringSpeed = 150f;
    public float handbrakeForce = 10f;
    public float nitroBoostMultiplier = 2f;

    // State variables
    public float currentSpeed { get; private set; }
    private float currentSteeringSpeed;
    private Rigidbody rb;
    private bool isHandbraking = false;

    public TMP_Text speedText;

    // Upgrade management
    public int MaxSlots = 3;
    public List<UpgradeSlot> UpgradeSlots { get; private set; } = new List<UpgradeSlot>();
    private VehicleUpgradeSystem upgradeSystem;

    private void Awake()
    {
        InitializeUpgradeSlots();

        upgradeSystem = GetComponent<VehicleUpgradeSystem>();
        if (upgradeSystem == null)
        {
            upgradeSystem = gameObject.AddComponent<VehicleUpgradeSystem>();
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("VehicleController requires a Rigidbody component.");
        }
    }

    private void FixedUpdate()
    {
        MoveVehicle();
    }

    private void InitializeUpgradeSlots()
    {
        UpgradeSlots.Clear();
        UpgradeSlots.Add(new UpgradeSlot(UpgradeType.Nitro));
        UpgradeSlots.Add(new UpgradeSlot(UpgradeType.Speed));
        UpgradeSlots.Add(new UpgradeSlot(UpgradeType.Any));
    }

    public bool AttachUpgrade(BaseVehicleUpgrade upgrade)
    {
        return upgradeSystem.AttachUpgrade(upgrade);
    }

    public bool RemoveUpgrade(BaseVehicleUpgrade upgrade)
    {
        return upgradeSystem.RemoveUpgrade(upgrade);
    }

    public void ApplyInput(float throttle, float steering, bool handbrake, bool nitro)
    {
        // Calculate speed
        float targetSpeed = throttle * (throttle > 0 ? maxSpeed : reverseSpeed);

        if (nitro)
        {
            targetSpeed *= nitroBoostMultiplier;
        }

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        if (throttle == 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        // Steering
        currentSteeringSpeed = Mathf.Lerp(baseSteeringSpeed, maxSteeringSpeed, Mathf.Abs(currentSpeed) / maxSpeed);
        transform.Rotate(Vector3.up, steering * currentSteeringSpeed * Time.fixedDeltaTime);

        // Handbrake
        isHandbraking = handbrake;
        if (isHandbraking)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, handbrakeForce * Time.fixedDeltaTime);
        }
    }

    private void MoveVehicle()
    {
        Vector3 forwardMove = transform.forward * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);
    }
}
