using System.Collections;
using UnityEngine;

public enum AIMode { Racer, Wanderer }

public class AIController : MonoBehaviour
{
    public AIMode aiMode;
    public Transform[] checkpoints; // Assigned in Racer mode
    public Transform wanderArea; // Assigned in Wanderer mode

    private Vehicle vehicle;
    private int currentCheckpointIndex = 0;
    private Vector3 wanderTarget;

    private void Start()
    {
        vehicle = GetComponent<Vehicle>();

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
        wanderTarget = new Vector3(
            Random.Range(wanderArea.position.x - wanderArea.localScale.x / 2, wanderArea.position.x + wanderArea.localScale.x / 2),
            transform.position.y,
            Random.Range(wanderArea.position.z - wanderArea.localScale.z / 2, wanderArea.position.z + wanderArea.localScale.z / 2)
        );
    }
}
