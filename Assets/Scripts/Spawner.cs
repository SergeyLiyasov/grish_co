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
            new List<NoteDescriptor> { new NoteDescriptor(NoteType.Note, 0), new NoteDescriptor(NoteType.Note, 4), new NoteDescriptor(NoteType.Note, 8) },
            new List<NoteDescriptor> { new NoteDescriptor(NoteType.Note, 0), new NoteDescriptor(NoteType.Note, 4), new NoteDescriptor(NoteType.Note, 8) },
            new List<NoteDescriptor> { new NoteDescriptor(NoteType.Note, 0), new NoteDescriptor(NoteType.Note, 4), new NoteDescriptor(NoteType.Note, 8) },
            new List<NoteDescriptor> { new NoteDescriptor(NoteType.Note, 0), new NoteDescriptor(NoteType.Note, 4), new NoteDescriptor(NoteType.Note, 8) } };
    }

    void Update()
    {
        for (var column = 0; column < nextIndexes.Length; column++)
        {
            var noteDescriptor = Notes[column][nextIndexes[column]];
            if (nextIndexes[column] < Notes[column].Count &&
                noteDescriptor.SpawnTime < Conductor.Instance.SongPositionInBeats + Conductor.Instance.BeatsShownInAdvance)
            {
                var type = GetNotePrefabByType(noteDescriptor.NoteType);

                var position = GameManager.Instance.GetColumnPosition(column);

                var noteObject = Instantiate(type, position, Quaternion.identity);
                var note = noteObject.GetComponent<BaseNote>();
                note.SpawnTime = noteDescriptor.SpawnTime;
                note.Column = column;

                Debug.Log($"Spawned note ¹{nextIndexes[column]} in {column} column");
                //Debug.Log(Conductor.Instance.BeatNumber + Conductor.Instance.BeatsShownInAdvance);
                nextIndexes[column]++;
            }
        }
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

    [SerializeField] private GameObject scroller;

    [SerializeField] private GameObject notePrefab;
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject endPrefab;

    private int[] nextIndexes = { 0, 0, 0, 0 };
    
}