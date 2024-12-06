using System.Collections;
using UnityEngine;

public enum AIMode { Racer, Wanderer }

public class AIController : MonoBehaviour
{
    public AIMode aiMode;
    public Transform[] checkpoints; // Assigned in Racer mode
    private Terrain terrain; // Assigned in Wanderer mode

    private Vehicle vehicle;
    private int currentCheckpointIndex = 0;
    private Vector3 wanderTarget;

    private void Start()
    {
        vehicle = GetComponent<Vehicle>();

        if (terrain == null)
        {
            terrain = GameManager.instance.terrainMap.transform.GetComponent<Terrain>();
        }

        if (aiMode == AIMode.Wanderer)
        {
            SetNewWanderTarget();
        }
    }

    private void Update()
    {
        switch (aiMode)
        {
            case AIMode.Racer:
                HandleRacerMode();
                break;
            case AIMode.Wanderer:
                HandleWandererMode();
                break;
        }
    }

    private void HandleRacerMode()
    {
        if (checkpoints.Length == 0) return;

        Transform targetCheckpoint = checkpoints[currentCheckpointIndex];
        Vector3 direction = (targetCheckpoint.position - transform.position).normalized;

        float throttle = 1f;
        float steering = Vector3.Dot(transform.right, direction);
        bool handbrake = false;

        vehicle.ApplyInput(throttle, steering, handbrake, false);

        if (Vector3.Distance(transform.position, targetCheckpoint.position) < 5f)
        {
            currentCheckpointIndex = (currentCheckpointIndex + 1) % checkpoints.Length;
        }
    }

    private void HandleWandererMode()
    {
        Vector3 direction = (wanderTarget - transform.position).normalized;

        float throttle = 1f;
        float steering = Vector3.Dot(transform.right, direction);
        bool handbrake = false;

        vehicle.ApplyInput(throttle, steering, handbrake, false);

        if (Vector3.Distance(transform.position, wanderTarget) < 5f)
        {
            SetNewWanderTarget();
        }
    }

    private void SetNewWanderTarget()
    {
        // Get the terrain boundaries
        Vector3 terrainPosition = terrain.transform.position;
        Vector3 terrainSize = terrain.terrainData.size;

        // Generate random X and Z positions within the terrain bounds
        float randomX = Random.Range(terrainPosition.x, terrainPosition.x + terrainSize.x);
        float randomZ = Random.Range(terrainPosition.z, terrainPosition.z + terrainSize.z);

        // Get the terrain height at the random point
        float height = terrain.SampleHeight(new Vector3(randomX, 0, randomZ));

        // Set the wander target to this point
        wanderTarget = new Vector3(randomX, height + terrainPosition.y, randomZ);
    }

}
