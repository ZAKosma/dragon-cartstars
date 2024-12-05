using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Garage : MonoBehaviour, IBuilding
{
    public PlayerInventory playerInventory;
    public Vehicle playerVehicle;

    // UI Elements
    public GameObject garagePanel; // Main panel for the garage
    public Transform inventorySlotsContainer; // Parent for inventory slots
    public Transform equippedSlotsContainer; // Parent for equipped upgrade slots
    public GameObject upgradeSlotPrefab; // Prefab for upgrade slots

    private List<GameObject> inventorySlots = new List<GameObject>();
    private List<GameObject> equippedSlots = new List<GameObject>();

    private void Start()
    {
        InitializeUI();
        CloseGarage();
    }

    public void InitializeUI()
    {
        // Initialize equipped slots
        foreach (Transform child in equippedSlotsContainer)
        {
            equippedSlots.Add(child.gameObject);
        }

        // Initialize inventory slots
        foreach (Transform child in inventorySlotsContainer)
        {
            inventorySlots.Add(child.gameObject);
        }

        PopulateEquippedSlots();
        PopulateInventorySlots();
    }

    public void OpenGarage()
    {
        garagePanel.SetActive(true);
        PopulateEquippedSlots();
        PopulateInventorySlots();
    }

    public void CloseGarage()
    {
        garagePanel.SetActive(false);
    }

    public void PopulateEquippedSlots()
    {
        for (int i = 0; i < equippedSlots.Count; i++)
        {
            if (i < playerVehicle.UpgradeSlots.Count)
            {
                UpgradeSlot vehicleSlot = playerVehicle.UpgradeSlots[i];
                SetupSlot(equippedSlots[i], vehicleSlot.Upgrade, () => UnequipUpgrade(vehicleSlot.Upgrade));
            }
            else
            {
                equippedSlots[i].SetActive(false);
            }
        }
    }

    public void PopulateInventorySlots()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (i < playerInventory.ownedUpgrades.Count)
            {
                BaseVehicleUpgrade inventoryUpgrade = playerInventory.ownedUpgrades[i];
                SetupSlot(inventorySlots[i], inventoryUpgrade, () => EquipUpgrade(inventoryUpgrade));
            }
            else
            {
                inventorySlots[i].SetActive(false);
            }
        }
    }

    private void SetupSlot(GameObject slot, BaseVehicleUpgrade upgrade, UnityEngine.Events.UnityAction onClickAction)
    {
        slot.SetActive(upgrade != null);

        if (upgrade != null)
        {
            TMP_Text itemNameText = slot.transform.GetChild(0).GetComponent<TMP_Text>();
            itemNameText.text = upgrade.upgradeName;

            // Image itemVisual = slot.transform.Find("Image").GetComponent<Image>();
            // itemVisual.sprite = upgrade.itemSprite; // Assuming BaseVehicleUpgrade has a `Sprite itemSprite`

            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.RemoveAllListeners();
            slotButton.onClick.AddListener(onClickAction);
        }
    }

    public void EquipUpgrade(BaseVehicleUpgrade upgrade)
    {
        if (playerVehicle.AttachUpgrade(upgrade))
        {
            playerInventory.ownedUpgrades.Remove(upgrade);
            Debug.Log($"Equipped {upgrade.upgradeName} on vehicle.");
            PopulateEquippedSlots();
            PopulateInventorySlots();
        }
        else
        {
            Debug.Log("No compatible slot available to equip this upgrade.");
        }
    }

    public void UnequipUpgrade(BaseVehicleUpgrade upgrade)
    {
        if (playerVehicle.RemoveUpgrade(upgrade))
        {
            playerInventory.ownedUpgrades.Add(upgrade);
            Debug.Log($"Unequipped {upgrade.upgradeName} from vehicle.");
            PopulateEquippedSlots();
            PopulateInventorySlots();
        }
        else
        {
            Debug.Log("Upgrade is not equipped on the vehicle.");
        }
    }

    public void TriggerBuilding()
    {
        if (garagePanel.activeSelf)
        {
            CloseGarage();
        }
        else
        {
            OpenGarage();
        }
    }
}
