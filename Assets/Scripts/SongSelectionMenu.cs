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

    private void Start()
    {
        songButtonNames = new Dictionary<GameObject, string>();
        FillSongList();
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
        //Debug.Log("ye");
        var button = Instantiate(template);
        button.SetActive(true);
        songButtonNames.Add(button, buttonText);
        var textObject = button.transform.Find("Text");
        var text = textObject.GetComponent<TextMeshProUGUI>();
        text.SetText(buttonText);
        button.transform.SetParent(parent.transform, false);
        textObject.transform.SetParent(button.transform, false);
    }

    public void SearchSong()
    {
        var inputText = "";
        if (inputField.text != "")
            inputText = (inputField.text.First().ToString().ToUpper() + inputField.text.Substring(1)).Trim();
        foreach (var button in songButtonNames)
            button.Key.SetActive(true);
        if (songButtonNames.ContainsValue(inputText))
        {
            var searchResults = songButtonNames.Where(x => x.Value == inputText).Select(x => x.Value);
            foreach(var button in songButtonNames)
            {
                if (!searchResults.Contains(button.Value))
                    button.Key.SetActive(false);
            }
        }
        else if (inputText != "")
        {
            foreach (var button in songButtonNames)
                button.Key.SetActive(false);
        }
    }

    public void LoadSong() 
    {
        SceneManager.LoadScene("Game");
    }

    [SerializeField] private GameObject songTemplate;
    [SerializeField] private GameObject songContainer;
    [SerializeField] private TMP_InputField inputField;
    private Dictionary<GameObject, string> songButtonNames;
}
