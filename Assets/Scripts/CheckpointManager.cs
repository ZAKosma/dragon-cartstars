using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    public Material nextCheckpointMaterial;
    public Material upcomingCheckpointMaterial;
    public Material defaultCheckpointMaterial;
    public Material flagMaterial;

    private List<Checkpoint> checkPoints = new List<Checkpoint>();
    private int currentCheckpointIndex = 0;

    public Race currentRace;
    public int currentLap = 1;
    public int totalLaps = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializeCheckpoints()
    {
        checkPoints.Clear();
        currentCheckpointIndex = 0;
        currentLap = 1;
        totalLaps = currentRace.totalLaps;

        foreach (var checkpointObj in currentRace.checkPoints)
        {
            checkpointObj.SetActive(true);
            Checkpoint checkpoint = checkpointObj.GetComponent<Checkpoint>();
            checkpoint.checkPointIndex = checkPoints.Count;
            checkPoints.Add(checkpoint);
        }
        
        if (currentRace.raceType == RaceType.Circuit)
        {
            // Ensure the first checkpoint also acts as the flag in circuits
            checkPoints[0].SetMaterial(flagMaterial);
        }

        UpdateCheckpointMaterials();
    }

    public void PlayerTriggeredCheckpoint(Checkpoint checkpoint)
    {
        if (checkpoint.checkPointIndex == currentCheckpointIndex)
        {
            currentCheckpointIndex++;

            if (currentCheckpointIndex >= checkPoints.Count)
            {
                if (currentRace.raceType == RaceType.Circuit && currentLap < totalLaps)
                {
                    currentLap++;
                    currentCheckpointIndex = 0;
                }
                else
                {
                    FinishRace();
                    return;
                }
            }

            UpdateCheckpointMaterials();
        }
    }

    private void UpdateCheckpointMaterials()
    {
        for (int i = 0; i < checkPoints.Count; i++)
        {
            if (i == currentCheckpointIndex)
            {
                checkPoints[i].SetMaterial(nextCheckpointMaterial);
            }
            else if (i == currentCheckpointIndex + 1 && i < checkPoints.Count)
            {
                checkPoints[i].SetMaterial(upcomingCheckpointMaterial);
            }
            else if (i == checkPoints.Count - 1 && currentRace.raceType != RaceType.Circuit)
            {
                checkPoints[i].SetMaterial(flagMaterial);
            }
            else
            {
                checkPoints[i].SetMaterial(defaultCheckpointMaterial);
            }
        }
    }

    private void FinishRace()
    {
        foreach (var checkpointObj in currentRace.checkPoints)
        {
            checkpointObj.SetActive(false);
        }

        Debug.Log($"Race finished! Reward: {currentRace.raceReward}");
        // Add additional logic for rewards, UI, etc.

        PlayerInventory.instance.AddCoins(currentRace.raceReward);
        
        GameManager.instance.EndRace();
    }
}
