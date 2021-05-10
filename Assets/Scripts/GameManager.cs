using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public Queue<BaseNote>[] NotesToBePressed { get; set; }

    public List<Button> NoteButtons { get; set; } = new List<Button>();

    public static GameManager Instance { get; private set; }

    public GameManager() => Instance = this;

    void Start()
    {
        Application.targetFrameRate = 3000;
        NotesToBePressed = new Queue<BaseNote>[NoteButtons.Count];
        for (int i = 0; i < NoteButtons.Count; i++)
        {
            NotesToBePressed[i] = new Queue<BaseNote>();
        }
    }

    public void RegisterNote(BaseNote note)
    {
        NotesToBePressed[note.Column].Enqueue(note);
    }

    public void OutdateNote(BaseNote note)
    {
        if (!TryOutdateNote(note)) throw new Exception("Note outdating before registering");
    }

    public bool TryOutdateNote(BaseNote note)
    {
        var q = NotesToBePressed[note.Column];
        if (q.Count == 0 || q.Peek() != note)
            return false;
        NotesToBePressed[note.Column].Dequeue();
        return true;
    }

    public void ReceiveSignal(Button button, bool activating)
    {
        if (NotesToBePressed[button.Column].Count != 0)
        {
            var note = NotesToBePressed[button.Column].Peek();
            score += note.ReceiveSignal(activating);
        }
    }

    public Vector2 GetColumnPosition(int index)
    {
        return index < columnsNumber
            ? columnsPosition + new Vector2(columnWidth * index, 7.2f)
            : throw new ArgumentException();
    }

    [SerializeField] private int score;
    private int columnsNumber = 4;
    private Vector2 columnsPosition = new Vector2(-2.5f, 7.2f);
    private float columnWidth = 2;
}
