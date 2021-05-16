using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SongMetadata
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public string DifficultyName { get; set; }
    public string[] Tags { get; set; }

    public SongMetadata(string title, string artist, string difficultyName, string[] tags)
    {
        Title = title;
        Artist = artist;
        DifficultyName = difficultyName;
        Tags = tags;
    }
}
