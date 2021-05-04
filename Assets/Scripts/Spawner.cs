using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    void Update()
    {
        if (reader.Current == null)
        {
            reader.MoveNext();
        }
        else if (reader.Current.Time == Conductor.Instance.SongPosition)
        {
            GameObject type = GetNotePrefabByType();

            var position = GameManager.Instance.GetColumnPosition(reader.Current.Column);

            Instantiate(type, position, Quaternion.identity, scroller.transform);
            reader.MoveNext();
        }
    }

    private GameObject GetNotePrefabByType()
    {
        return reader.Current.Type switch
        {
            NoteType.Start => startPrefab,
            NoteType.End => endPrefab,
            NoteType.Note => notePrefab,
            _ => throw new ArgumentException()
        };
    }

    [SerializeField] private INotePrefabsReader reader;

    [SerializeField] private GameObject scroller;

    [SerializeField] private GameObject notePrefab;
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject endPrefab;
}