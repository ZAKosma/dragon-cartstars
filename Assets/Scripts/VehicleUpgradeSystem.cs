using System.Collections.Generic;
using UnityEngine;

public class VehicleUpgradeSystem : MonoBehaviour
{
    public Vehicle vehicle;

    private void Awake()
    {
        if (vehicle == null)
        {
            vehicle = GetComponent<Vehicle>();
        }

        if (vehicle == null)
        {
            Debug.LogError("VehicleUpgradeSystem requires a Vehicle component.");
        }
    }

    // Attach an upgrade to the first available slot
    public bool AttachUpgrade(BaseVehicleUpgrade upgrade)
    {
        foreach (UpgradeSlot slot in vehicle.UpgradeSlots)
        {
            if (slot.IsCompatible(upgrade) && !slot.IsOccupied)
            {
                slot.Attach(upgrade);
                upgrade.ApplyUpgrade(vehicle);
                return true;
            }
        }
        Debug.Log("No compatible or available slot for upgrade: " + upgrade.upgradeName);
        return false;
    }

    // Remove an upgrade from the vehicle
    public bool RemoveUpgrade(BaseVehicleUpgrade upgrade)
    {
        foreach (UpgradeSlot slot in vehicle.UpgradeSlots)
        {
            if (slot.Upgrade == upgrade)
            {
                slot.Detach(vehicle);
                return true;
            }
        }
        Debug.Log("Upgrade not found on this vehicle: " + upgrade.upgradeName);
        return false;
    }
}