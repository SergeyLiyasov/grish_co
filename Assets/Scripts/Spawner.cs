using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<NoteDescriptor>[] Notes { get; set; }
    public static Spawner Instance { get; private set; }
    public NoteReader Reader { get; set; }

    void Start()
    {
        Instance = this;
        Reader = new NoteReader(GameManager.PathToDifficulty);
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
        }
    }

    private GameObject BuildNote(NoteType type, float destinationTime, int column)
    {
        var noteObject = Instantiate(GetNotePrefabByType(type), notesContainer.transform);
        var note = noteObject.GetComponent<BaseNote>();
        if (note is LongNoteEnd end)
            end.Beginning = lastNotes[column] as LongNoteBeginning;
        note.DestinationTime = destinationTime;
        note.Column = column;
        note.SpriteRenderer.sprite = GetSpriteByColumnNumber(column);
        lastNotes[column] = note;
        return noteObject;     
    }

    private GameObject GetNotePrefabByType(NoteType noteType)
    {
        return noteType switch
        {
            NoteType.Beginning => beginningPrefab,
            NoteType.End => endPrefab,
            NoteType.Note => notePrefab,
            _ => throw new ArgumentException()
        };
    }

    private Sprite GetSpriteByColumnNumber(int column)
    {
        return new[] { leftSprite, downSprite, upSprite, rightSprite }[column];
    }

    public void BuildLongNoteTail(LongNoteBeginning beginning, float destinationTime)
    {
        //var longNoteEndSpawn = reader.NoteQueues[column].Peek().SpawnTime;
        var tailObject = Instantiate(tailPrefab, notesContainer.transform);
        var tail = tailObject.GetComponent<LongNoteTail>();
        tail.Beginning = beginning;
        tail.DestinationTime = destinationTime;
        tail.SpriteRenderer.sprite = tailSprite;
        //tail.LengthInBeats = tail.SpawnTime - longNoteEndSpawn;
    }

    [SerializeField] private GameObject notesContainer;

    [SerializeField] private GameObject notePrefab;
    [SerializeField] private GameObject beginningPrefab;
    [SerializeField] private GameObject endPrefab;
    [SerializeField] private GameObject tailPrefab;

    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private Sprite tailSprite;

    private BaseNote[] lastNotes = { null, null, null, null };
}