using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Vehicle vehicle;
    
    public TMP_Text speedText;

    private void Start()
    {
        vehicle = GetComponent<Vehicle>();
        if(speedText == null)
            speedText = GameObject.Find("SpeedText").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        float throttle = Input.GetAxis("Vertical");
        float steering = Input.GetAxis("Horizontal");
        bool handbrake = Input.GetKey(KeyCode.Space);
        bool nitro = Input.GetKey(KeyCode.LeftShift);

        vehicle.ApplyInput(throttle, steering, handbrake, nitro);
        
        speedText.text = $"Speed: {vehicle.currentSpeed:0.0} km/s";

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}