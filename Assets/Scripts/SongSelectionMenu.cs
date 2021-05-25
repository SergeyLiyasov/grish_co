using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class SongSelectionMenu : MonoBehaviour
{
    public List<SongMetadata> SongMetadatas { get; set; }
    public static bool UsePrefixSearch { get; set; }

    private void Start()
    {
        songButtonNames = new SortedList<string, GameObject>();
        FillSongList();
        //Debug.Log(UsePrefixSearch);
    }

    public void FillSongList()
    {
        var path = "Assets/Resources/Descriptors";
        foreach (var song in Directory.EnumerateDirectories(path))
        {
            var name = song.Split('\\').Last();
            var strArray = name.Split('-');
            var firstPart = strArray[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var songId = int.Parse(firstPart[0]);
            var artistParsed = string.Join(" ", firstPart, 1, firstPart.Length - 1);
            var songName = strArray[1].Trim();
            var parsedName = artistParsed + " - " + songName;
            var button = BuildButtonFromData(songTemplate, parsedName, songContainer);
            var textObject = button.transform.Find("Text");
            var buttonText = textObject.GetComponent<TextMeshProUGUI>().text;
            var meta = button.GetComponent<SongMetadata>();
            meta.Artist = artistParsed;
            meta.Title = songName;
            meta.Id = songId;
            songButtonNames.Add(buttonText, button);
        }
    }

    public GameObject BuildButtonFromData(GameObject template, string buttonText, GameObject parent)
    {
        var button = Instantiate(template);
        button.SetActive(true);
        var textObject = button.transform.Find("Text");
        var text = textObject.GetComponent<TextMeshProUGUI>();
        text.SetText(buttonText);
        button.transform.SetParent(parent.transform, false);
        textObject.transform.SetParent(button.transform, false);
        return button;
    }

    public void SearchSong()
    {
        var inputText = inputField.text.ToLower().Trim();

        foreach (var button in songButtonNames.Values)
            button.SetActive(false);

        var searchResults = UsePrefixSearch
                ? PrefixSearch(songButtonNames, inputText)
                : songButtonNames.Where(x => x.Key.ToLower().Contains(inputText)).Select(x => x.Value);

        foreach (var button in searchResults)
            button.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadSong() 
    {
        SceneManager.LoadScene("Game");
    }

    private static int BinSearchLeftBorder(IList<string> list, string prefix)
    {
        var left = -1;
        var right = list.Count;
        while (left + 1 != right)
        {
            var middle = (left + right) / 2;
            if (string.Compare(list[middle], prefix, StringComparison.OrdinalIgnoreCase) < 0)
                left = middle;
            else
                right = middle;
        }
        return left;
    }

    private static int BinSearchRightBorder(IList<string> list, string prefix)
    {
        var left = -1;
        var right = list.Count;
        while (left + 1 != right)
        {
            var middle = left + (right - left) / 2;
            if (string.Compare(list[middle], prefix, StringComparison.OrdinalIgnoreCase) <= 0 ||
                list[middle].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                left = middle;
            else
                right = middle;
        }
        return right;
    }

    private IEnumerable<GameObject> PrefixSearch(SortedList<string, GameObject> buttons, string prefix)
    {
        var left = BinSearchLeftBorder(buttons.Keys, prefix);
        var right = BinSearchRightBorder(buttons.Keys, prefix);
        return buttons.Values.Skip(left + 1).Take(right - left - 1);
    }

    [SerializeField] private GameObject songTemplate;
    [SerializeField] private GameObject songContainer;
    [SerializeField] private TMP_InputField inputField;
    private SortedList<string, GameObject> songButtonNames;
}
