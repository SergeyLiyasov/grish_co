using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("scrollSpeed"))
        {
            scrollSlider.value = 1 / PlayerPrefs.GetFloat("scrollSpeed");
            Debug.Log(scrollSlider.value);
        }     
        else
            PlayerPrefs.SetFloat("scrollSpeed", 2f);
        if (PlayerPrefs.HasKey("volume"))
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
        else
            PlayerPrefs.SetFloat("volume", 0.09f);
        Conductor.Volume = PlayerPrefs.GetFloat("volume");
        Conductor.BeatsShownInAdvance = PlayerPrefs.GetFloat("scrollSpeed");
        
    }

    public void SongSelection()
    {
        SceneManager.LoadScene("Song Selection");
    }

    public void ChangeVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        volumeSlider.value = volume;
    }

    public void ChangeScrollSpeed(float scrollSpeed)
    {
        var convertedSpeed = 1 / scrollSpeed;
        PlayerPrefs.SetFloat("scrollSpeed", convertedSpeed);
        //Conductor.BeatsShownInAdvance = convertedSpeed;
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
