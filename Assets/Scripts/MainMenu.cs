using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Conductor.Volume = volume;
        Conductor.BeatsShownInAdvance = scrollSpeed;
        Debug.Log(scrollSlider.value);
    }

    public void SongSelection()
    {
        SceneManager.LoadScene("Song Selection");
    }

    public void ChangeVolume(float volume)
    {
        Conductor.Volume = volume;
        volumeSlider.value = volume;
    }

    public void ChangeScrollSpeed(float scrollSpeed)
    {
        Conductor.BeatsShownInAdvance = scrollSpeed;
        scrollSlider.value = scrollSpeed;
        Debug.Log(scrollSlider.value);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ActivateBinarySearch(bool isOn)
    {
        if (isOn)
            SongSelectionMenu.UseBinarySearch = true;
        else
            SongSelectionMenu.UseBinarySearch = false;
    }

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider scrollSlider;
    [SerializeField] private Toggle useBinarySearchCheckbox;
    private static float volume = 0.09f;
    private static float scrollSpeed = 2f;
}
