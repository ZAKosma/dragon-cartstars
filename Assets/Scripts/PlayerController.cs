using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vehicle vehicle;

    private void Start()
    {
        vehicle = GetComponent<Vehicle>();
    }

    private void Update()
    {
        float throttle = Input.GetAxis("Vertical");
        float steering = Input.GetAxis("Horizontal");
        bool handbrake = Input.GetKey(KeyCode.Space);
        bool nitro = Input.GetKey(KeyCode.LeftShift);

        vehicle.ApplyInput(throttle, steering, handbrake, nitro);
    }
}