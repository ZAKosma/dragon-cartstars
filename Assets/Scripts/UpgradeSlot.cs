public class UpgradeSlot
{
    public bool IsOccupied => Upgrade != null;
    public BaseVehicleUpgrade Upgrade { get; private set; }
    public UpgradeType AllowedType; // Enum for allowed type

    public UpgradeSlot(UpgradeType allowedType = UpgradeType.Any)
    {
        AllowedType = allowedType;
    }

    public bool IsCompatible(BaseVehicleUpgrade upgrade)
    {
        return AllowedType == UpgradeType.Any || upgrade.UpgradeType == AllowedType;
    }

    public void Attach(BaseVehicleUpgrade upgrade)
    {
        Upgrade = upgrade;
    }

    public void Detach(Vehicle vehicle)
    {
        if (Upgrade != null)
        {
            Upgrade.RemoveUpgrade(vehicle);
            Upgrade = null;
        }
    }
}