using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SongSelection()
    {
        SceneManager.LoadScene("Song Selection");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
