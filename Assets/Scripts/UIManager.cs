using UnityEngine;
using UnityEngine.UI;
using TMPro; // Only if you're using TextMeshPro

public class UIManager : MonoBehaviour
{
    public Button dayNightButton;       // Day/Night button
    public Button volumeButton;         // Volume button
    public Button locationButton;       // Location button
    public GameObject locationList;     // The panel that contains the list of locations
    public Button[] locationButtons;    // Buttons for the locations
    public Sprite xGraphic;             // The "X" graphic for    the Location button
    public Sprite defaultGraphic;       // Default graphic for the Location button
    public Button closeLocationButton;  // Close button to close the locationList panel
    public GameObject musicPanel;       // The music panel to toggle visibility

    public Transform player;            // The player's transform (the object to teleport)
    public Camera mainCamera;           // Reference to the main camera

    // Locations and their coordinates
    private Vector3 bedroomPosition = new Vector3(-27.80501f, -22.14f, 245.411f);
    private Vector3 kitchenPosition = new Vector3(-27.966f, -24.4f, 256.6f);
    private Vector3 livingRoomPosition = new Vector3(-27.1f, -24.7f, 249.01f);
    private Vector3 meadowPosition = new Vector3(-751.505981f, -13.33f, 480.959991f);
    private Vector3 forestPosition = new Vector3(-830.1067f, -20.05f, 371.21f);
    private Vector3 riversidePosition = new Vector3(-325.5f, -36.9f, 450f);

    // Camera rotations for each location
    private Vector3 bedroomCameraRotation = new Vector3(0f, -16.24f, 0f);
    private Vector3 kitchenCameraRotation = new Vector3(0f, 0f, 0f);
    private Vector3 livingRoomCameraRotation = new Vector3(0f, -124.9f, 0f);
    private Vector3 meadowCameraRotation = new Vector3(0f, 20.72f, 0f);
    private Vector3 forestCameraRotation = new Vector3(15f, 45f, 0f);
    private Vector3 riversideCameraRotation = new Vector3(0f, -198.53f, 0f);

    private bool isLocationListVisible = false; // Track if the location list is currently visible
    private bool isMusicPanelVisible = false;   // Track if the music panel is currently visible

    void Start()
    {
        // Hide the location list and music panel initially
        if (locationList != null)
        {
            locationList.SetActive(false);
        }

        if (musicPanel != null)
        {
            musicPanel.SetActive(false);
        }

        // Assign listeners to buttons
        if (locationButton != null) locationButton.onClick.AddListener(ToggleLocationList);
        if (dayNightButton != null) dayNightButton.onClick.AddListener(ToggleDayNight);
        if (volumeButton != null) volumeButton.onClick.AddListener(ToggleVolume);

        // Assign listeners to location buttons
        if (locationButtons != null)
        {
            foreach (Button locationBtn in locationButtons)
            {
                if (locationBtn != null)
                {
                    locationBtn.onClick.AddListener(() => OnLocationSelected(locationBtn));
                }
            }
        }

        // Assign listener to the CloseLocation button to close the locationList panel
        if (closeLocationButton != null)
        {
            closeLocationButton.onClick.AddListener(CloseLocationList);
        }
    }

    // Toggle the location list visibility
    void ToggleLocationList()
    {
        isLocationListVisible = !isLocationListVisible;

        // Show or hide the location list based on the state
        if (locationList != null)
        {
            locationList.SetActive(isLocationListVisible);
        }

        // Change the button graphic to "X" when the list is shown, otherwise reset
        if (locationButton != null)
        {
            locationButton.image.sprite = isLocationListVisible ? xGraphic : defaultGraphic;
        }

        // Hide the main buttons when the location list is visible
        ToggleMainButtonsVisibility(false);
    }

    // Close the location list when the CloseLocation button is clicked
    void CloseLocationList()
    {
        // Hide the location list panel
        if (locationList != null)
        {
            locationList.SetActive(false);
        }

        // Reset the Location button graphic back to default
        if (locationButton != null)
        {
            locationButton.image.sprite = defaultGraphic;
        }

        // Update the state
        isLocationListVisible = false;

        // Show the main buttons when the location list is closed
        ToggleMainButtonsVisibility(true);
    }

    // Toggle the Music Panel visibility
    void ToggleMusicPanel()
    {
        isMusicPanelVisible = !isMusicPanelVisible;

        // Show or hide the music panel based on the state
        if (musicPanel != null)
        {
            musicPanel.SetActive(isMusicPanelVisible);
        }

        // Hide the main buttons when the music panel is visible
        ToggleMainButtonsVisibility(false);
    }

    // Close the Music Panel when the close button is clicked
    void CloseMusicPanel()
    {
        if (musicPanel != null)
        {
            musicPanel.SetActive(false);
        }

        // Update the state
        isMusicPanelVisible = false;

        // Show the main buttons when the music panel is closed
        ToggleMainButtonsVisibility(true);
    }

    // Placeholder function for the DayNight button
    void ToggleDayNight()
    {
        Debug.Log("DayNight button clicked!");
    }

    // Placeholder function for the Volume button
    void ToggleVolume()
    {
        Debug.Log("Volume button clicked!");
    }

    // Location button click handler (called when a location is selected)
    void OnLocationSelected(Button selectedButton)
    {
        if (selectedButton == null)
        {
            Debug.LogError("Selected button is null!");
            return;
        }

        // Get the location name from the button's TextMeshPro component
        string locationName = selectedButton.GetComponentInChildren<TextMeshProUGUI>()?.text;

        if (string.IsNullOrEmpty(locationName))
        {
            Debug.LogError("Location button text is empty or null!");
            return;
        }

        // Debug log the selected location
        Debug.Log($"Selected Location: {locationName}");

        // Teleport player and rotate camera based on the location
        switch (locationName)
        {
            case "Bedroom":
                TeleportToLocation(bedroomPosition, bedroomCameraRotation);
                break;
            case "Kitchen":
                TeleportToLocation(kitchenPosition, kitchenCameraRotation);
                break;
            case "Living Room":
                TeleportToLocation(livingRoomPosition, livingRoomCameraRotation);
                break;
            case "Meadow":
                TeleportToLocation(meadowPosition, meadowCameraRotation);
                break;
            case "Forest":
                TeleportToLocation(forestPosition, forestCameraRotation);
                break;
            case "Riverside":
                TeleportToLocation(riversidePosition, riversideCameraRotation);
                break;
            default:
                Debug.LogWarning("Unknown location selected");
                break;
        }

        // Hide the location list after selection
        if (locationList != null)
        {
            locationList.SetActive(false);
        }
        if (locationButton != null)
        {
            locationButton.image.sprite = defaultGraphic; // Reset button graphic
        }
        isLocationListVisible = false; // Update state

        // Show the main buttons when the location list is closed
        ToggleMainButtonsVisibility(true);
    }

    // Teleport the player to the given location and rotate the camera
    void TeleportToLocation(Vector3 position, Vector3 cameraRotation)
    {
        if (player != null)
        {
            player.position = position;  // Set player's position to the specified location
            Debug.Log($"Player teleported to: {position}");

            if (mainCamera != null)
            {
                mainCamera.transform.rotation = Quaternion.Euler(cameraRotation);  // Set the camera's rotation
                Debug.Log($"Camera rotated to: {cameraRotation}");
            }
            else
            {
                Debug.LogError("Main Camera is not assigned!");
            }
        }
        else
        {
            Debug.LogError("Player Transform is not assigned!");
        }
    }

    // Helper function to toggle the visibility of the main buttons
    void ToggleMainButtonsVisibility(bool isVisible)
    {
        if (dayNightButton != null)
            dayNightButton.gameObject.SetActive(isVisible);
        if (volumeButton != null)
            volumeButton.gameObject.SetActive(isVisible);
        if (locationButton != null)
            locationButton.gameObject.SetActive(isVisible);
    }
}
