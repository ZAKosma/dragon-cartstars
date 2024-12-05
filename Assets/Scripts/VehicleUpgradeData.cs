using UnityEngine;

[CreateAssetMenu(fileName = "NewVehicleUpgrade", menuName = "Upgrades/Vehicle Upgrade")]
public class VehicleUpgradeData : ScriptableObject
{
    public string upgradeName;
    public UpgradeType upgradeType;
    public float upgradeValue;
    public int cost;
    public int sellValue;

    public BaseVehicleUpgrade CreateUpgradeInstance()
    {
        return upgradeType switch
        {
            UpgradeType.Nitro => new NitroUpgrade(upgradeName, upgradeValue),
            UpgradeType.Speed => new SpeedUpgrade(upgradeName, upgradeValue),
            _ => null,
        };
    }
}