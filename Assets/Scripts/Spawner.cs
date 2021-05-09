using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<NoteDescriptor>[] Notes { get; set; }


    void Start()
    {
        Notes = new[]
        {
            new List<NoteDescriptor> { new NoteDescriptor(NoteType.Note, 12), new NoteDescriptor(NoteType.Note, 16), new NoteDescriptor(NoteType.Note, 20) },
            new List<NoteDescriptor> { new NoteDescriptor(NoteType.Start, 12), new NoteDescriptor(NoteType.End, 16) },
            new List<NoteDescriptor> { new NoteDescriptor(NoteType.Note, 12), new NoteDescriptor(NoteType.Note, 16), new NoteDescriptor(NoteType.Note, 20) },
            new List<NoteDescriptor> { new NoteDescriptor(NoteType.Note, 12), new NoteDescriptor(NoteType.Note, 16), new NoteDescriptor(NoteType.Note, 20) },
        };
    }

    void Update()
    {
        for (var column = 0; column < nextIndexes.Length; column++)
        {
            if (nextIndexes[column] >= Notes[column].Count) continue;
            var noteDescriptor = Notes[column][nextIndexes[column]];
            if (noteDescriptor.SpawnTime >= Conductor.Instance.SongPositionInBeats) continue;

            //var position = GameManager.Instance.GetColumnPosition(column);
            //var noteObject = 
            BuildNote(noteDescriptor.NoteType, noteDescriptor.DestinationTime, column);

            //Debug.Log($"Spawned note ¹{nextIndexes[column]} in {column} column");
            //Debug.Log(Conductor.Instance.BeatNumber + Conductor.Instance.BeatsShownInAdvance);
            nextIndexes[column]++;
        }
    }

    private GameObject BuildNote(NoteType type, float destinationTime, int column)
    {
        var noteObject = Instantiate(GetNotePrefabByType(type), notesContainer.transform);
        var note = noteObject.GetComponent<BaseNote>();
        if (note is LongNoteEnd end)
            end.Start = lastNote[column] as LongNoteStart;
        note.DestinationTime = destinationTime;
        note.Column = column;
        lastNote[column] = note;
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

    [SerializeField] private INotePrefabsReader reader;

    [SerializeField] private GameObject notesContainer;

    [SerializeField] private GameObject notePrefab;
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject endPrefab;

    private int[] nextIndexes = { 0, 0, 0, 0 };
    private BaseNote[] lastNote = { null, null, null, null };
}