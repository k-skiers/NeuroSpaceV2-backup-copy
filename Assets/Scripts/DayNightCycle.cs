using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    public Material skybox1;  // Day skybox
    public Material skybox2;  // Night skybox
    public Button dayNightButton;  // DayNight button to toggle
    public GameObject lightingObject;  // Reference to the Lighting GameObject
    public GameObject[] lightsToHide;  // Array of GameObjects to hide during the day

    private bool isDay = true;

    void Start()
    {
        // Ensure Button Click Listener is added
        if (dayNightButton != null)
        {
            dayNightButton.onClick.AddListener(OnDayNightButtonClicked);
        }
        else
        {
            Debug.LogError("DayNightButton is not assigned in the Inspector!");
        }

        // Set the initial skybox and lighting object state
        if (skybox1 != null)
        {
            RenderSettings.skybox = skybox1;
        }
        else
        {
            Debug.LogError("Skybox1 is not assigned in the Inspector!");
        }

        // Ensure Lighting object is enabled initially if day
        if (lightingObject != null)
        {
            lightingObject.SetActive(true); // Lighting should be visible during the day
        }
        else
        {
            Debug.LogError("Lighting GameObject is not assigned in the Inspector!");
        }

        // Initially, disable all lights (as it's day)
        if (lightsToHide != null)
        {
            foreach (var lightObj in lightsToHide)
            {
                if (lightObj != null)
                {
                    lightObj.SetActive(false); // Disable lights during the day
                }
            }
        }
    }

    void OnDayNightButtonClicked()
    {
        if (skybox1 == null || skybox2 == null)
        {
            Debug.LogError("Skyboxes are not assigned properly!");
            return;
        }

        // Toggle between day and night skyboxes
        if (isDay)
        {
            RenderSettings.skybox = skybox2;  // Change to night skybox
            if (lightingObject != null)
            {
                lightingObject.SetActive(false); // Disable Lighting object at night
            }

            // Enable lights when it's night
            if (lightsToHide != null)
            {
                foreach (var lightObj in lightsToHide)
                {
                    if (lightObj != null)
                    {
                        lightObj.SetActive(true); // Enable lights during the night
                    }
                }
            }
        }
        else
        {
            RenderSettings.skybox = skybox1;  // Change to day skybox
            if (lightingObject != null)
            {
                lightingObject.SetActive(true); // Enable Lighting object during the day
            }

            // Disable lights when it's day
            if (lightsToHide != null)
            {
                foreach (var lightObj in lightsToHide)
                {
                    if (lightObj != null)
                    {
                        lightObj.SetActive(false); // Disable lights during the day
                    }
                }
            }
        }

        // Toggle the state (day/night)
        isDay = !isDay;
    }
}