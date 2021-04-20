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

    public Dictionary<Button, bool> SkipNextNote { get; set; } =
        new Dictionary<Button, bool>();

    public static GameManager Instance { get; private set; }
    
    private int score = 0;

    public GameManager() : base()
    {
        Instance = this;
    }

    void Start()
    {
        foreach (var button in NoteButtons)
        {
            NotesToBePressed[button] = new Queue<BaseNote>();
            SkipNextNote[button] = false;
        }     
    }

    void Update()
    {

    }

    public void NormalHit()
    {
        score += 100;
        Debug.Log("Okay hit");
    }

    public void GoodHit()
    {
        score += 200;
        Debug.Log("Good hit");
    }

    public void PerfectHit()
    {
        score += 300;
        Debug.Log("Perfect hit");
    }

    public void RainbowHit()
    {
        score += 320;
        Debug.Log("Marvelous hit");
    }

    public void RegisterNote(BaseNote note)
    {
        if (SkipNextNote[note.Button] == true)
        {
            SkipNextNote[note.Button] = false;
        }
        else
        {
            NotesToBePressed[note.Button].Enqueue(note);
        }
    }

    public void OutdateNote(BaseNote note)
    {
        if (!NotesToBePressed.TryGetValue(note.Button, out var q) || q.Count == 0 || q.Peek() != note)
            throw new Exception("Note outdating before registering");
        NotesToBePressed[note.Button].Dequeue();
    }

    public BaseNote ReceiveSignal(Button button)
    {
        if (NotesToBePressed[button].Count != 0 && !(NotesToBePressed[button].Peek() is LongNoteEnd))
        {
            return NotesToBePressed[button].Dequeue();
        }
        return null;
    }

}
