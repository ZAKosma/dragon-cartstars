using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    public int checkPointIndex;

    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.instance.PlayerTriggeredCheckpoint(this);
        }
    }

    public void SetMaterial(Material material)
    {
        
        if (renderer != null)
        {
            renderer.material = material;
        }
    }
}