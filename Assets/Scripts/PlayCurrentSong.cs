using UnityEngine;

public class PlayCurrentSong : MonoBehaviour
{
    // Declare the current song index (public if you want to set it from another script or UI)
    private int currentSongIndex = 0;

    private string[] songs = { "Passacaglia - Handel_ Halvorsen (Relaxing Piano Music) - Yannick Lowack", "Randy Rogers Band - In My Arms Instead (2008) - Danu Romero" }; // Song list
    // This function can now use currentSongIndex correctly
    void PlaySong()  // Renamed method
    {
        string songPath = "Inside/Songs/" + songs[currentSongIndex];  // Path for the song
        AudioClip clip = Resources.Load<AudioClip>(songPath);

        if (clip != null)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();
            Debug.Log($"Now playing: {songs[currentSongIndex]}");
        }
        else
        {
            Debug.LogWarning("Audio clip not found!");
        }
    }
}
