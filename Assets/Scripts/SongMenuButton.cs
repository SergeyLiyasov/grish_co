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
    private const string pathToDescriptors = "Assets/Resources/Descriptors/";
    private const string descriptorExtension = ".osu";
    public SongSelectionMenu songMenu;

    public void GetDifficultiesFromSongName()
    {
        parsedDifficultyNames = new Dictionary<string, string>();
        var textObject = transform.Find("Text");
        var songName = textObject.GetComponent<TextMeshProUGUI>().text;
        currentSongName = songName;
        var path = pathToDescriptors + songName.First().ToString().ToUpper() + songName.Substring(1);
        var directory = new DirectoryInfo(path);
        var difficulties = directory.GetFiles("*.osu");
        foreach (Transform child in difficultyContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var difficulty in difficulties)
        {
            var parsedString = difficulty.Name.Split(new char[] { '[', ']' }, System.StringSplitOptions.RemoveEmptyEntries);
            var parsedDifficultyName = parsedString[parsedString.Length - 2];
            parsedDifficultyNames.Add(difficulty.Name, parsedDifficultyName);
            songMenu.BuildButtonFromData(difficultyTemplate, parsedDifficultyName, difficultyContainer);
        }
    }

    public void LoadSongReader()
    {
        var textObject = transform.Find("Text");
        var difficultyName = parsedDifficultyNames.FirstOrDefault(x => x.Value == textObject.GetComponent<TextMeshProUGUI>().text).Key;
        var clip = Resources.Load<AudioClip>("Descriptors/" + currentSongName + '/' + currentSongName);
        Conductor.Music = clip;
        GameManager.PathToDifficulty = pathToDescriptors + currentSongName + '/' + difficultyName;
        SceneManager.LoadScene("Game");
    }

    [SerializeField] private GameObject difficultyTemplate;
    [SerializeField] private GameObject difficultyContainer;

    private static string currentSongName;
    private static Dictionary<string, string> parsedDifficultyNames;
}
