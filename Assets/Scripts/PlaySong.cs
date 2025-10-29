using UnityEngine;

public class PlaySong : MonoBehaviour
{
    // Reference to the MusicPlayer script
    public MusicPlayer musicPlayer;

    // This method will be called to toggle play/pause

    

    // This method will be called to go to the next song
    public void NextSongButton()
    {
        if (musicPlayer != null)
        {
            musicPlayer.NextSong();
        }
    }

    // This method will be called to go to the previous song
    public void PreviousSongButton()
    {
        if (musicPlayer != null)
        {
            musicPlayer.PreviousSong();
        }
    }
}
