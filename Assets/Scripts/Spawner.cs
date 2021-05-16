using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<NoteDescriptor>[] Notes { get; set; }
    public NoteReader Reader { get; set; }
    public string text = "Assets/Descriptors/1.txt";


    void Start()
    {
        Reader = new NoteReader(GameManager.PathToDifficulty);
        Debug.Log(GameManager.PathToDifficulty);
    }

    void Update()
    {
        for (var column = 0; column < Reader.NoteQueues.Length; column++)
        {
            if (Reader.NoteQueues[column].Count == 0) continue;
            var noteDescriptor = Reader.NoteQueues[column].Peek();
            if (noteDescriptor.SpawnTime >= Conductor.Instance.SongPositionInBeats) continue;
            Reader.NoteQueues[column].Dequeue();
            BuildNote(noteDescriptor.NoteType, noteDescriptor.DestinationTime, column);

            //Debug.Log($"Spawned note ¹{nextIndexes[column]} in {column} column");
            //Debug.Log(Conductor.Instance.BeatNumber + Conductor.Instance.BeatsShownInAdvance);
        }
    }

    private GameObject BuildNote(NoteType type, float destinationTime, int column)
    {
        var noteObject = Instantiate(GetNotePrefabByType(type), notesContainer.transform);
        var note = noteObject.GetComponent<BaseNote>();
        if (note is LongNoteStart start)
        {
            note = AddLongNoteTail(start, destinationTime, column);
            Debug.Log(start.Tail.LengthInBeats);
        }
        if (note is LongNoteEnd end)
            end.Start = lastNotes[column] as LongNoteStart;
        note.DestinationTime = destinationTime;
        note.Column = column;
        note.Sprite = GetSpriteByColumnNumber(column);
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

    private Sprite GetSpriteByColumnNumber(int column)
    {
        return new[] { leftSprite, downSprite, upSprite, rightSprite }[column];
    }

    private LongNoteStart AddLongNoteTail(LongNoteStart start, float destinationTime, int column)
    {
        var longNoteEndSpawn = Reader.NoteQueues[column].Peek().SpawnTime;
        var tailObject = Instantiate(tailPrefab);
        start.Tail = tailObject.GetComponent<LongNoteTail>();
        start.Tail.Column = column;
        start.Tail.DestinationTime = destinationTime;
        start.Tail.LengthInBeats = start.Tail.SpawnTime - longNoteEndSpawn;
        return start;
    }

    [SerializeField] private GameObject notesContainer;

    [SerializeField] private GameObject notePrefab;
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject endPrefab;
    [SerializeField] private GameObject tailPrefab;

    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private Sprite tailSprite;



    private BaseNote[] lastNotes = { null, null, null, null };
}