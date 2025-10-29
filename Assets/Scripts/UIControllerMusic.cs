using UnityEngine;
using UnityEngine.UI; // For Button component

public class UIControllerMusic : MonoBehaviour
{
    public GameObject musicPanel;      // Reference to the MusicPanel GameObject
    public Button volumeButton;        // Reference to the Volume Button
    public Sprite volumeIcon;          // Default volume icon (before opening the MusicPanel)
    public Sprite closeIcon;           // X icon (for closing the MusicPanel)

    public Button closeButton;         // Reference to the X Button inside the MusicPanel

    private bool isMusicPanelOpen = false; // Track whether the music panel is open

    void Start()
    {
        // Initially set the MusicPanel to inactive
        musicPanel.SetActive(false);
        Debug.Log("MusicPanel is initially hidden.");

        // Add listener for the VolumeButton click event
        volumeButton.onClick.AddListener(ToggleMusicPanel);

        // Add listener for the X button (close the MusicPanel)
        closeButton.onClick.AddListener(CloseMusicPanel);
    }


    // Method to toggle the visibility of the MusicPanel and icon change
    private void ToggleMusicPanel()
    {
        Debug.Log("VolumeButton clicked. Toggling MusicPanel...");

        if (isMusicPanelOpen)
        {
            // Close MusicPanel and change the icon to Volume
            musicPanel.SetActive(false);
            volumeButton.GetComponent<Image>().sprite = volumeIcon;
            Debug.Log("MusicPanel closed. Icon changed to Volume.");
        }
        else
        {
            // Open MusicPanel and change the icon to X
            musicPanel.SetActive(true);
            volumeButton.GetComponent<Image>().sprite = closeIcon;
            Debug.Log("MusicPanel opened. Icon changed to X.");
        }

        // Toggle the panel state
        isMusicPanelOpen = !isMusicPanelOpen;
    }

    // Method to close the MusicPanel when X button is clicked
    private void CloseMusicPanel()
    {
        Debug.Log("X Button clicked. Closing MusicPanel...");

        // Close MusicPanel and change the icon to Volume
        musicPanel.SetActive(false);
        volumeButton.GetComponent<Image>().sprite = volumeIcon;

        // Reset the music panel state
        isMusicPanelOpen = false;
    }

}
