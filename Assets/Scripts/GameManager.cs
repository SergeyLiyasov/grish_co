using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Dictionary<ButtonBehaviourScript, Queue<NoteBehaviourScript>> notesToBePressed;

    void Start()
    {
        notesToBePressed = new Dictionary<ButtonBehaviourScript, Queue<NoteBehaviourScript>>();
    }

    void Update()
    {
        
    }

    public void RegisterNote(NoteBehaviourScript note)
    {
        if (!notesToBePressed.ContainsKey(note.Button))
            notesToBePressed[note.Button] = new Queue<NoteBehaviourScript>();
        notesToBePressed[note.Button].Enqueue(note);
    }

    public void OutdateNote(NoteBehaviourScript note)
    {
        if (!notesToBePressed.TryGetValue(note.Button, out var q) || q.Count == 0 || q.Dequeue() != note)
            throw new Exception("Note outdating before registering");
        DecreaseScore();
    }

    public void ReceiveSignal(ButtonBehaviourScript button)
    {
        if (!notesToBePressed.ContainsKey(button) || notesToBePressed[button].Count == 0)
        {
            DecreaseScore();
        }
        else
        {
            var note = notesToBePressed[button].Dequeue();
            var accuracy = note.GetPressed();
            IncreaseScore();
        }
    }

    void IncreaseScore()
    {
        Debug.Log("Score +");
    }

    void DecreaseScore()
    {
        Debug.Log("Score -");
    }
}
