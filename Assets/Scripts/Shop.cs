using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour, IBuilding
{
    public GameObject shopPanel;
    public Transform shopSlotsContainer; // Parent container holding the 8 shop slots
    // public GameObject vehiclePanel;
    // public Transform vehicleSpawnPoint;
    public List<VehicleUpgradeData> availableUpgrades; // Upgrades sold in this shop

    private List<GameObject> shopSlots = new List<GameObject>(); // Cache of shop slot GameObjects

    private void Start()
    {
        // vehiclePanel.SetActive(false);
        InitializeShopSlots();
        PopulateShopUI();
        shopPanel.SetActive(false);

    }
    
    private void InitializeShopSlots()
    {
        // Cache all shop slots in the container
        foreach (Transform child in shopSlotsContainer)
        {
            shopSlots.Add(child.gameObject);
            // Debug.Log("Shop slot added:  " + child.gameObject.name);
        }
    }

    public void PopulateShopUI()
    {
        // Ensure no more than 8 items are shown
        int maxSlots = Mathf.Min(shopSlots.Count, availableUpgrades.Count);

        for (int i = 0; i < shopSlots.Count - 1; i++)
        {
            if (i < maxSlots)
            {
                // Enable the slot and populate it with upgrade data
                shopSlots[i].SetActive(true);
                // Debug.Log("Shop slot enabled:  " + shopSlots[i].name);
                // Debug.Log("Available upgrade:  " + availableUpgrades[i].upgradeName);
                SetupShopSlot(shopSlots[i], availableUpgrades[i]);
            }
            else
            {
                // Disable unused slots
                shopSlots[i].SetActive(false);
            }
        }
    }

    private void SetupShopSlot(GameObject slot, VehicleUpgradeData upgradeData)
    {
        // Set item name
        TMP_Text itemNameText = slot.transform.GetChild(0).GetComponent<TMP_Text>();
        itemNameText.text = upgradeData.upgradeName;

        // Set item visual
        // Image itemVisual = slot.transform.Find("Image").GetComponent<Image>();
        // itemVisual.sprite = upgradeData.itemSprite;

        // Add button functionality
        Button slotButton = slot.GetComponent<Button>();
        slotButton.onClick.RemoveAllListeners(); // Clear previous listeners
        slotButton.onClick.AddListener(() => BuyUpgrade(upgradeData));
    }


    public void DisplayShopItems()
    {
        foreach (var upgrade in availableUpgrades)
        {
            Debug.Log($"Shop sells: {upgrade.upgradeName} for {upgrade.cost} coins.");
        }
    }

    public void BuyUpgrade(VehicleUpgradeData upgradeData)
    {
        if (PlayerInventory.instance.PurchaseUpgrade(upgradeData))
        {
            Debug.Log($"Purchased {upgradeData.upgradeName}.");
        }
        else
        {
            Debug.Log("Not enough coins to purchase upgrade.");
        }
    }

    public void TriggerBuilding()
    {
        Debug.Log("Shop!");
        shopPanel.SetActive(!shopPanel.gameObject.activeSelf);
    }
}