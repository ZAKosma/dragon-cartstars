using System.Collections;
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
    public float terrainSlowFactor = 0.5f;
    public float baseSteeringSpeed = 50f;
    public float maxSteeringSpeed = 150f;
    public float handbrakeForce = 10f;
    public float jumpForce = 300f;

    public float nitroBoostMultiplier = 2f;
    public int nitroUseRate = 1;
    public int currentNitroAmount = 100;
    private int nitroAmountMax = 100;
    private int nitroRechargeRate = 5;
    public float nitroExhaustCooldown = 3f;
    private bool canUseNitro = true;

    // State variables
    private float currentSpeed = 0f;
    private float currentSteeringSpeed;
    private Rigidbody rb;
    private bool isHandbraking = false;

    // Key mappings
    private KeyCode forwardKey = KeyCode.W;
    private KeyCode backwardKey = KeyCode.S;
    private KeyCode leftKey = KeyCode.A;
    private KeyCode rightKey = KeyCode.D;
    private KeyCode handbrakeKey = KeyCode.Space;
    private KeyCode nitroKey = KeyCode.LeftShift;
    private KeyCode jumpKey = KeyCode.LeftControl;

    private int steeringDirection = 0;
    
    public TMP_Text speedText;

    // Upgrade management
    public int MaxSlots = 3; // Max upgrade slots for this vehicle
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

    private void InitializeUpgradeSlots()
    {
        UpgradeSlots.Clear();

        // Example configuration: 1 Nitro slot, 1 Speed slot, 1 Any slot
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
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("VehicleController requires a Rigidbody component.");
        }

        nitroAmountMax = currentNitroAmount;
    }

    void Update()
    {
        HandleInput();
        UpdateUI();
    }

    void FixedUpdate()
    {
        MoveVehicle();
    }

    void UpdateUI()
    {
        speedText.text = "Speed: " + currentSpeed.ToString("F2") + " km/h";
    }

    private void HandleInput()
    {
        float targetSpeed = 0f;

        // Forward/Backward Input
        if (Input.GetKey(forwardKey))
        {
            targetSpeed = maxSpeed;
        }
        else if (Input.GetKey(backwardKey))
        {
            targetSpeed = reverseSpeed;
        }

        // Adjust speed for nitro boost
        if (Input.GetKey(nitroKey))
        {
            //Nitro amount is greater than 0
            if (currentNitroAmount > 0 && canUseNitro)
            {
                //Add nitro boost to speed
                targetSpeed *= nitroBoostMultiplier;
                currentNitroAmount -= nitroUseRate;
            }
            else
            {
                StartCoroutine(ResetNitroCooldown());
            }
        }
        else
        {
            if(canUseNitro && currentNitroAmount < nitroAmountMax)
                currentNitroAmount += nitroRechargeRate;
        }

        // Lerp to target speed for smooth acceleration/deceleration
        if (targetSpeed != 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        // Adjust for terrain
        // currentSpeed *= GetTerrainSpeedMultiplier();

        // Steering
        if (Input.GetKey(leftKey))
        {
            steeringDirection = -1;
        }
        else if (Input.GetKey(rightKey))
        {
            steeringDirection = 1;
        }
        else
        {
            steeringDirection = 0;
        }

        // Handbrake
        isHandbraking = Input.GetKey(handbrakeKey);

        // Jump
        if (Input.GetKeyDown(jumpKey))
        {
            Jump();
        }
    }

    IEnumerator ResetNitroCooldown()
    {
        canUseNitro = false;
        yield return new WaitForSeconds(nitroExhaustCooldown);
        currentNitroAmount = nitroAmountMax;
        canUseNitro = true;
    }

    private void MoveVehicle()
    {
        // Apply movement
        Vector3 forwardMove = transform.forward * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);

        // Steering with dynamic turn speed
        if (steeringDirection != 0)
        {
            currentSteeringSpeed = Mathf.Lerp(baseSteeringSpeed, maxSteeringSpeed, Mathf.Abs(currentSpeed) / maxSpeed);
            transform.Rotate(Vector3.up, steeringDirection * currentSteeringSpeed * Time.fixedDeltaTime);
        }

        // Apply handbrake effect
        if (isHandbraking)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, handbrakeForce * Time.fixedDeltaTime);
        }
    }

    private float GetTerrainSpeedMultiplier()
    {
        // Cast a ray down to detect the terrain texture
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            Terrain terrain = hit.collider.GetComponent<Terrain>();
            if (terrain != null)
            {
                TerrainData terrainData = terrain.terrainData;
                Vector3 terrainPos = hit.point - terrain.transform.position;

                Vector3 terrainNormalized = new Vector3(
                    terrainPos.x / terrainData.size.x,
                    0,
                    terrainPos.z / terrainData.size.z);

                int mapX = Mathf.FloorToInt(terrainNormalized.x * terrainData.alphamapWidth);
                int mapZ = Mathf.FloorToInt(terrainNormalized.z * terrainData.alphamapHeight);

                float[,,] splatMapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

                float dirtWeight = splatMapData[0, 0, 0];
                float grassWeight = splatMapData[0, 0, 1];
                float rockWeight = splatMapData[0, 0, 2];

                float speedMultiplier = dirtWeight + grassWeight * terrainSlowFactor + rockWeight * terrainSlowFactor;
                return Mathf.Clamp(speedMultiplier, 0.1f, 1f);
            }
        }
        return 1f;
    }

    private void Jump()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.5f) // Ensure grounded before jumping
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
