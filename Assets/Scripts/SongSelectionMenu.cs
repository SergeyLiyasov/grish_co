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
    public static bool UseBinarySearch { get; set; }

    private void Start()
    {
        songButtonNames = new Dictionary<string, GameObject>();
        FillSongList();
        Debug.Log(UseBinarySearch);
    }

    public void FillSongList()
    {
        var path = "Assets/Resources/Descriptors";
        foreach (var song in Directory.EnumerateDirectories(path))
        {
            var name = song.Split('\\').Last();
            BuildButtonFromData(songTemplate, name, songContainer);
        }
    }

    public void BuildButtonFromData(GameObject template, string buttonText, GameObject parent)
    {
        var button = Instantiate(template);
        button.SetActive(true);
        songButtonNames.Add(buttonText, button);
        var textObject = button.transform.Find("Text");
        var text = textObject.GetComponent<TextMeshProUGUI>();
        text.SetText(buttonText);
        button.transform.SetParent(parent.transform, false);
        textObject.transform.SetParent(button.transform, false);
    }

    public void SearchSong()
    {
        var inputText = string.Empty;
        if (inputField.text != string.Empty)
            inputText = inputField.text.ToLower().Trim();
        foreach (var button in songButtonNames)
            button.Value.SetActive(true);
        if (songButtonNames.Any(x => x.Key.ToLower().Contains(inputText)))
        {
            var searchResults = new List<GameObject>();
            if (UseBinarySearch)
            {
                var loweredDict = songButtonNames.ToDictionary(k => k.Key.ToLower(), k => k.Value);
                searchResults = PrefixSearch(loweredDict, inputText);
            }
            else    
                searchResults = songButtonNames.Where(x => x.Key.ToLower().Contains(inputText)).Select(x => x.Value).ToList();
            foreach (var button in songButtonNames)
            {
                if (!searchResults.Contains(button.Value))
                    button.Value.SetActive(false);
            }
        }
        else if (inputText != string.Empty)
        {
            foreach (var button in songButtonNames)
                button.Value.SetActive(false);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadSong() 
    {
        SceneManager.LoadScene("Game");
    }

    private int BinarySearch(List<string> list, string str)
    {
        str = str.Substring(0, str.Length);
        var left = 0;
        var right = list.Count - 1;
        var index = -1;
        var found = false;
        while (!found && left <= right)
        {
            var middle = left + (right - left) / 2;
            var current = list[middle].Substring(0, str.Length);
            var result = str.CompareTo(current);

            if (result == 0)
            {
                found = true;
                index = middle;
            }
            else if (result > 0)
                left = middle + 1;
            else
                right = middle - 1;
        }
        return index;
    }

    private List<GameObject> PrefixSearch(Dictionary<string, GameObject> dict, string prefix)
    {
        var keyList = dict.Select(x => x.Key.ToLower()).ToList();
        keyList.Sort();
        var index = BinarySearch(keyList, prefix);
        if (index == -1)
            return new List<GameObject>();
        var rangeStart = index;
        var rangeEnd = index;
        while (rangeStart > -1 && keyList[rangeStart].ToLower().StartsWith(prefix.ToLower()))
            rangeStart--;

        while (rangeEnd < keyList.Count && keyList[rangeEnd].ToLower().StartsWith(prefix.ToLower()))
            rangeEnd++;

        var result = new List<GameObject>();
        foreach(var key in keyList.GetRange(rangeStart + 1, rangeEnd - rangeStart - 1))
        {
            result.Add(dict[key]);
        }
        return result;
    }

    [SerializeField] private GameObject songTemplate;
    [SerializeField] private GameObject songContainer;
    [SerializeField] private TMP_InputField inputField;
    private Dictionary<string, GameObject> songButtonNames;
}
