// Interface for triggerable buildings

using System;
using UnityEngine;

public interface IBuilding
{
    void TriggerBuilding();
}

//Building Proximity Trigger Class
public class BuildingTrigger : MonoBehaviour
{
    
    IBuilding building;

    private bool canTrigger;

    private void Start()
    {
        //Get building from parent
        building = GetComponentInParent<IBuilding>();

        canTrigger = true;

        GameManager.instance.onStartRace += ToggleActive;
        GameManager.instance.onEndRace += ToggleActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(canTrigger == false)
        {
            return;
        }
        
        Debug.Log("Triggered building with: " + other.name);
        if (other.CompareTag("Player"))
        {
            building.TriggerBuilding();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(canTrigger == false)
        {
            return;
        }
        
        Debug.Log("Exited building with: " + other.name);
        if (other.CompareTag("Player"))
        {
            building.TriggerBuilding();
        }
    }

    void ToggleActive()
    {
        canTrigger = !canTrigger;
    }
}
