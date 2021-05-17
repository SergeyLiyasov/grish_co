using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Conductor.Volume = volume;
        Conductor.BeatsShownInAdvance = scrollSpeed;
    }

    public void SongSelection()
    {
        SceneManager.LoadScene("Song Selection");
    }

    public void ChangeVolume(float volume)
    {
        Conductor.Volume = volume;
        Debug.Log(Conductor.Volume);
    }

    public void ChangeScrollSpeed(float scrollSpeed)
    {
        Conductor.BeatsShownInAdvance = scrollSpeed;
        Debug.Log(Conductor.BeatsShownInAdvance);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private float volume = 0.09f;
    private float scrollSpeed = 2f;
}
