using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;
    private int playerCoins = 100;
    public List<BaseVehicleUpgrade> ownedUpgrades = new List<BaseVehicleUpgrade>();

    public TMP_Text coinText;

    private void Awake()
    {
        //Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateCoinUI();
    }

    public bool PurchaseUpgrade(VehicleUpgradeData upgradeData)
    {
        if (playerCoins >= upgradeData.cost)
        {
            SubtractCoins(upgradeData.cost);
            ownedUpgrades.Add(upgradeData.CreateUpgradeInstance());
            return true;
        }
        return false;
    }

    public void SellUpgrade(BaseVehicleUpgrade upgrade)
    {
        playerCoins += upgrade.sellValue;
        ownedUpgrades.Remove(upgrade);
    }

    [ContextMenu("Print Inventory to Log")]
    public void DisplayInventory()
    {
        Debug.Log("Player Inventory:");
        foreach (var upgrade in ownedUpgrades)
        {
            Debug.Log($"Owned Upgrade: {upgrade.upgradeName}");
        }
    }
    
    public int GetPlayerCoins()
    {
        return playerCoins;
    }
    //Methods for adding, checking for spend, and spending coins
    public void AddCoins(int amount)
    {
        playerCoins += amount;
        UpdateCoinUI();
    }
    public bool SpendCoins(int amount)
    {
        if(playerCoins < amount)
        {
            return false;
        }
        SubtractCoins(amount);
        return true;
    }

    private void SubtractCoins(int amount)
    {
        playerCoins -= amount;
        UpdateCoinUI();
    }

    void UpdateCoinUI()
    {
        coinText.text = "Cash: " + playerCoins;
    }
}