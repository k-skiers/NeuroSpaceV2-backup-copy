using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicPlayer : MonoBehaviour
{
    // References to the UI elements in the scene
    public AudioSource audioSource;
    public TextMeshProUGUI songText;        // Song Text to display current song
    public Slider volumeSlider;             // Volume Slider

    public Button nextSongButton;           // Next Song Button
    public Button previousSongButton;       // Previous Song Button

    // Song list (without file extensions)
    private string[] songs = {
        "Naturgeräusche",
        "Nacht Lofi",
        "Regenbogen",
        "Resonant",
        "Rosa Geräusches",
        "Sanftes Klavier",
        "Schwarze Geräusche",
        "Tagsüber Lo-Fi",
        "Verloren in der Zeit",
        "Wiegenlied",
    };

    private int currentSongIndex = 0;       // Current song index
    private bool musicStarted = false;      // Track if music has started

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource attached to MusicPlayer!");
        }

        // Initialize button actions
        nextSongButton.onClick.AddListener(NextSong);              
        previousSongButton.onClick.AddListener(PreviousSong);      
        volumeSlider.onValueChanged.AddListener(AdjustVolume);    

        // Set the initial song text but do not play
        songText.text = songs[currentSongIndex];
    }

    // Play the current song
    public void PlaySong()
    {
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource attached to MusicPlayer!");
            return;
        }

        // Load the current song from Resources folder
        AudioClip song = Resources.Load<AudioClip>("Songs/" + songs[currentSongIndex]);
        if (song == null)
        {
            Debug.LogError("Song not found: " + songs[currentSongIndex]);
            return;
        }

        audioSource.clip = song;
        audioSource.Play();
        songText.text = songs[currentSongIndex];

        musicStarted = true; // Mark that music has started
        Debug.Log("Playing song: " + songs[currentSongIndex]);
    }

    // Go to the next song
    public void NextSong()
    {
        // If music hasn't started yet, start playing current song
        if (!musicStarted)
        {
            PlaySong();
            return;
        }

        currentSongIndex = (currentSongIndex + 1) % songs.Length;
        Debug.Log("Next song index: " + currentSongIndex);
        PlaySong();
    }

    // Go to the previous song
    public void PreviousSong()
    {
        if (!musicStarted) return; // Do nothing if music hasn't started

        currentSongIndex = (currentSongIndex - 1 + songs.Length) % songs.Length;
        Debug.Log("Previous song index: " + currentSongIndex);
        PlaySong();
    }

    // Adjust the volume based on the slider value
    public void AdjustVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}