using UnityEngine;

public interface IVehicleUpgrade
{
    void ApplyUpgrade(Vehicle target);
}

public enum UpgradeType
{
    Nitro,
    Speed,
    Any // Allows any type of upgrade
}

public abstract class BaseVehicleUpgrade : Item, IVehicleUpgrade
{
    public UpgradeType UpgradeType { get; } // Associated type

    public BaseVehicleUpgrade(string upgradeName, float upgradeValue, UpgradeType upgradeType) 
        : base(upgradeName, (int)upgradeValue, (int)upgradeValue / 2)
    {
        UpgradeType = upgradeType;
        this.upgradeValue = upgradeValue;
    }

    public string upgradeName => itemName;
    public readonly float upgradeValue;

    public virtual void ApplyUpgrade(Vehicle target)
    {
        Debug.Log("Applying " + upgradeName + " upgrade to " + target.name + " with value " + upgradeValue);
    }

    public virtual void RemoveUpgrade(Vehicle target)
    {
        Debug.Log("Removing " + upgradeName + " upgrade from " + target.name);
    }
}

public class NitroUpgrade : BaseVehicleUpgrade
{
    public NitroUpgrade(string upgradeName, float upgradeValue) 
        : base(upgradeName, upgradeValue, UpgradeType.Nitro) { }

    public override void ApplyUpgrade(Vehicle target)
    {
        base.ApplyUpgrade(target);
        target.nitroBoostMultiplier += upgradeValue;
    }

    public override void RemoveUpgrade(Vehicle target)
    {
        target.nitroBoostMultiplier -= upgradeValue;
        base.RemoveUpgrade(target);
    }
}

public class SpeedUpgrade : BaseVehicleUpgrade
{
    public SpeedUpgrade(string upgradeName, float upgradeValue) 
        : base(upgradeName, upgradeValue, UpgradeType.Speed) { }

    public override void ApplyUpgrade(Vehicle target)
    {
        base.ApplyUpgrade(target);
        target.maxSpeed += upgradeValue;
    }

    public override void RemoveUpgrade(Vehicle target)
    {
        target.maxSpeed -= upgradeValue;
        base.RemoveUpgrade(target);
    }
}