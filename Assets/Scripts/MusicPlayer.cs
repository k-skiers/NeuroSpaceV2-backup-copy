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

    // Song list (updated with .ogg or .wav extensions removed)
    private string[] songs = {
        "Black Noise Dreams",    // Song names without .ogg or .wav extension
        "Coverless Lofi",
        "Night Lofi",
        "Cosmos",
        "Lost in Time",
        "Nature sounds",
        "Kingdom is searching for New King",
        "Pink Noise for Studying",
        "Resonant",
    };

    private int currentSongIndex = 0;       // Current song index

    void Start()
    {
        // Ensure there is an AudioSource attached
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource attached to MusicPlayer!");
        }

        // Initialize button actions
        nextSongButton.onClick.AddListener(NextSong);              // Next Song button
        previousSongButton.onClick.AddListener(PreviousSong);      // Previous Song button
        volumeSlider.onValueChanged.AddListener(AdjustVolume);    // Volume slider

        // Set the initial song text
        songText.text = songs[currentSongIndex];

        // Play the first song immediately when the game starts
        PlaySong();
    }

    // Play the current song
    public void PlaySong()
    {
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource attached to MusicPlayer!");
            return;
        }

        // Log the current song being played
        Debug.Log("Playing song: " + songs[currentSongIndex]);

        // Load the current song from the Resources folder (without file extension)
        AudioClip song = Resources.Load<AudioClip>("Songs/" + songs[currentSongIndex]);

        // Check if the song is correctly loaded
        if (song == null)
        {
            Debug.LogError("Song not found: " + songs[currentSongIndex]);
            return;
        }

        // Set the audio clip and play the song
        audioSource.clip = song;
        audioSource.Play();
        songText.text = songs[currentSongIndex]; // Update song name
    }

    // Go to the next song
    public void NextSong()
    {
        // Update song index to the next song, looping back to the beginning if at the end
        currentSongIndex = (currentSongIndex + 1) % songs.Length;

        // Log the song index for debugging
        Debug.Log("Next song index: " + currentSongIndex);

        // Play the next song
        PlaySong();
    }

    // Go to the previous song
    public void PreviousSong()
    {
        // Update song index to the previous song, looping to the last song if at the start
        currentSongIndex = (currentSongIndex - 1 + songs.Length) % songs.Length;

        // Log the song index for debugging
        Debug.Log("Previous song index: " + currentSongIndex);

        // Play the previous song
        PlaySong();
    }

    // Adjust the volume based on the slider value
    public void AdjustVolume(float volume)
    {
        audioSource.volume = volume;  // Set the audio source volume
    }
}
