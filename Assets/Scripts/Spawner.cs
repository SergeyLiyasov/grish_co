using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<NoteDescriptor>[] Notes { get; set; }


    void Start()
    {
        reader = new NoteReader();
    }

    void Update()
    {
        for (var column = 0; column < reader.NoteQueues.Length; column++)
        {
            if (reader.NoteQueues[column].Count == 0) continue;
            var noteDescriptor = reader.NoteQueues[column].Peek();
            if (noteDescriptor.SpawnTime >= Conductor.Instance.SongPositionInBeats) continue;
            reader.NoteQueues[column].Dequeue();
            BuildNote(noteDescriptor.NoteType, noteDescriptor.DestinationTime, column);

            //Debug.Log($"Spawned note ¹{nextIndexes[column]} in {column} column");
            //Debug.Log(Conductor.Instance.BeatNumber + Conductor.Instance.BeatsShownInAdvance);
        }
    }

    private GameObject BuildNote(NoteType type, float destinationTime, int column)
    {
        var noteObject = Instantiate(GetNotePrefabByType(type), notesContainer.transform);
        var note = noteObject.GetComponent<BaseNote>();
        if (note is LongNoteEnd end)
            end.Start = lastNotes[column] as LongNoteStart;
        note.DestinationTime = destinationTime;
        note.Column = column;
        lastNotes[column] = note;
        return noteObject;
    }

    private GameObject GetNotePrefabByType(NoteType noteType)
    {
        return noteType switch
        {
            NoteType.Start => startPrefab,
            NoteType.End => endPrefab,
            NoteType.Note => notePrefab,
            _ => throw new ArgumentException()
        };
    }

    [SerializeField] private GameObject notesContainer;

    [SerializeField] private GameObject notePrefab;
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject endPrefab;

    private NoteReader reader;
    private BaseNote[] lastNotes = { null, null, null, null };
}