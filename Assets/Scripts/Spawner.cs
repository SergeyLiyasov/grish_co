using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<NoteDescriptor>[] Notes { get; set; }


    void Start()
    {
        Notes = new[] {
            new List<NoteDescriptor> { new NoteDescriptor(NoteType.Note, 0), new NoteDescriptor(NoteType.Note, 4), new NoteDescriptor(NoteType.Note, 8),
                new NoteDescriptor(NoteType.Note, 12), new NoteDescriptor(NoteType.Note, 16), new NoteDescriptor(NoteType.Note, 20),
                new NoteDescriptor(NoteType.Note, 24), new NoteDescriptor(NoteType.Note, 28) },
            new List<NoteDescriptor> { new NoteDescriptor(NoteType.Note, 0), new NoteDescriptor(NoteType.Note, 4), new NoteDescriptor(NoteType.Note, 8) },
            new List<NoteDescriptor> { new NoteDescriptor(NoteType.Note, 0), new NoteDescriptor(NoteType.Note, 4), new NoteDescriptor(NoteType.Note, 8) },
            new List<NoteDescriptor> { new NoteDescriptor(NoteType.Note, 0), new NoteDescriptor(NoteType.Note, 4), new NoteDescriptor(NoteType.Note, 8) } };
    }

    void Update()
    {
        for (var column = 0; column < nextIndexes.Length; column++)
        {
            if (nextIndexes[column] >= Notes[column].Count) continue;
            var noteDescriptor = Notes[column][nextIndexes[column]];
            if (noteDescriptor.SpawnTime >= Conductor.Instance.SongPositionInBeats) continue;
            //var type = GetNotePrefabByType(noteDescriptor.NoteType);

            var position = GameManager.Instance.GetColumnPosition(column);

            //var noteObject = Instantiate(type, position, Quaternion.identity, notesContainer.transform);
            //var note = noteObject.GetComponent<BaseNote>();
            //note.DestinationTime = noteDescriptor.DestinationTime;
            //note.Column = column;
            var noteObject = BuildNote(noteDescriptor.NoteType, noteDescriptor.DestinationTime, column);

            Debug.Log($"Spawned note ¹{nextIndexes[column]} in {column} column");
            //Debug.Log(Conductor.Instance.BeatNumber + Conductor.Instance.BeatsShownInAdvance);
            nextIndexes[column]++;
        }
    }

    private GameObject BuildNote(NoteType type, float destinationTime, int column)
    {
        var noteObject = Instantiate(GetNotePrefabByType(type), notesContainer.transform);
        var note = noteObject.GetComponent<BaseNote>();
        note.DestinationTime = destinationTime;
        note.Column = column;
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
}