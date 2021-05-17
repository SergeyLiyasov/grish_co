using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongMenuButton : MonoBehaviour
{
    public SongSelectionMenu songMenu;

    public void GetDifficultiesFromSongName()
    {
        var textObject = transform.Find("Text");
        var songName = textObject.GetComponent<TextMeshProUGUI>().text;
        currentSongName = songName;
        Debug.Log(songName);
        var path = "Assets/Resources/Descriptors/" + songName.First().ToString().ToUpper() + songName.Substring(1);
        var directory = new DirectoryInfo(path);
        var difficulties = directory.GetFiles("*.txt"); //изменить на .osu
        foreach (Transform child in difficultyContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var difficulty in difficulties)
        {
            songMenu.BuildButtonFromData(difficultyTemplate, difficulty.Name.Substring(0, difficulty.Name.Length - 4), difficultyContainer);
        }
    }

    public void LoadSongReader()
    {
        var textObject = transform.Find("Text");
        var difficultyName = textObject.GetComponent<TextMeshProUGUI>().text;
        var clip = Resources.Load<AudioClip>("Descriptors/" + currentSongName + '/' + currentSongName);
        Conductor.Music = clip;
        //Debug.Log("Descriptors/" + currentSongName);
        GameManager.PathToDifficulty = "Assets/Resources/Descriptors/" + currentSongName + '/' + difficultyName + ".txt"; //изменить на .osu
        SceneManager.LoadScene("Game");
    }

    [SerializeField] private GameObject difficultyTemplate;
    [SerializeField] private GameObject difficultyContainer;

    private static string currentSongName;
}
