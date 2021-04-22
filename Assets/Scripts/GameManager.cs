using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public Dictionary<Button, Queue<BaseNote>> NotesToBePressed { get; set; } =
        new Dictionary<Button, Queue<BaseNote>>();

    public HashSet<Button> NoteButtons { get; set; } =
        new HashSet<Button>();

    public static GameManager Instance { get; private set; }
    
    private int score;

    public GameManager() => Instance = this;

    void Start()
    {
        foreach (var button in NoteButtons)
        {
            NotesToBePressed[button] = new Queue<BaseNote>();
        }
    }

    void Update()
    {

    }

    public void RegisterNote(BaseNote note)
    {
        NotesToBePressed[note.Button].Enqueue(note);
    }

    public void OutdateNote(BaseNote note)
    {
        if (!TryOutdateNote(note)) throw new Exception("Note outdating before registering");
    }

    public bool TryOutdateNote(BaseNote note)
    {
        if (!NotesToBePressed.TryGetValue(note.Button, out var q) || q.Count == 0 || q.Peek() != note)
            return false;
        NotesToBePressed[note.Button].Dequeue();
        return true;
    }

    public void ReceiveSignal(Button button, bool activating)
    {
        if (NotesToBePressed[button].Count != 0)
        {
            var note = NotesToBePressed[button].Peek();
            score += note.ReceiveSignal(activating);
        }
    }
}
