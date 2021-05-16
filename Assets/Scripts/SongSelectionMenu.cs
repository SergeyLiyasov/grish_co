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
        Debug.Log("ye");
        var button = Instantiate(template);
        button.SetActive(true);
        var textObject = button.transform.Find("Text");
        var text = textObject.GetComponent<TextMeshProUGUI>();
        text.SetText(buttonText);
        button.transform.SetParent(parent.transform, false);
        textObject.transform.SetParent(button.transform, false);
    }

    public void LoadSong() 
    {
        SceneManager.LoadScene("Game");
    }

    [SerializeField] private GameObject songTemplate;
    [SerializeField] private GameObject songContainer;
}
