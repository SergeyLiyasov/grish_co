using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongMetadata : MonoBehaviour
{
    public string Title { get; set; }
    public string Artist { get; set; }
    //public string DifficultyName { get; set; }
    public int Id { get; set; }
    //public string[] Tags { get; set; }

    public SongMetadata(string title, string artist, int id)
    {
        Title = title;
        Artist = artist;
        Id = id;
    }
}
