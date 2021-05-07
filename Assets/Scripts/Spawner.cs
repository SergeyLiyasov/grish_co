using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<NoteDescriptor>[] Notes { get; set; }


    void Start()
    {
        Notes = new List<NoteDescriptor>[4] {
            new List<NoteDescriptor> { new NoteDescriptor(NoteType.Note, 0), new NoteDescriptor(NoteType.Note, 4), new NoteDescriptor(NoteType.Note, 8), new NoteDescriptor(NoteType.Note, 12), new NoteDescriptor(NoteType.Note, 16), new NoteDescriptor(NoteType.Note, 20), new NoteDescriptor(NoteType.Note, 24), new NoteDescriptor(NoteType.Note, 28) },
            new List<NoteDescriptor> { new NoteDescriptor(NoteType.Note, 0), new NoteDescriptor(NoteType.Note, 4), new NoteDescriptor(NoteType.Note, 8) },
            new List<NoteDescriptor> { new NoteDescriptor(NoteType.Note, 0), new NoteDescriptor(NoteType.Note, 4), new NoteDescriptor(NoteType.Note, 8) },
            new List<NoteDescriptor> { new NoteDescriptor(NoteType.Note, 0), new NoteDescriptor(NoteType.Note, 4), new NoteDescriptor(NoteType.Note, 8) } };
    }

    void Update()
    {
        for (var column = 0; column < nextIndexes.Length; column++)
        {
            if (nextIndexes[column] < Notes[column].Count && Notes[column][nextIndexes[column]].SpawnTime <= Conductor.Instance.SongPositionInBeats + Conductor.Instance.BeatsShownInAdvance)
            {
                GameObject type = GetNotePrefabByType(Notes[column][nextIndexes[column]].NoteType);

                var position = GameManager.Instance.GetColumnPosition(column);

                var currentNote = Instantiate(type, position, Quaternion.identity);
                currentNote.GetComponent<Note>().SpawnTime = Notes[column][nextIndexes[column]].SpawnTime;
                currentNote.GetComponent<Note>().Column = column;

                //Debug.Log(nextIndexes[column]);
                //Debug.Log(Conductor.Instance.SongPositionInBeats);
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

    private int[] nextIndexes = new int[4] {0,0,0,0};
    
}