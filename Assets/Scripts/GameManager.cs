using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //private Dictionary<ButtonBehaviourScript, Queue<NoteBehaviourScript>> notesToBePressed;
    //public HashSet<ButtonBehaviourScript> noteButtons = new HashSet<ButtonBehaviourScript>();
    public static GameManager instance;
    private int score = 0;

    void Start()
    {
        //notesToBePressed = new Dictionary<ButtonBehaviourScript, Queue<NoteBehaviourScript>>();
        //foreach (var button in noteButtons)
        //{
        //    Debug.Log(button + "initiated");
        //    notesToBePressed[button] = new Queue<NoteBehaviourScript>();
        //}
        instance = this;    
    }

    void Update()
    {

    }

    public void NormalHit()
    {
        RegisterNote();
        score += 100;
        Debug.Log("Okay hit");
    }

    public void GoodHit()
    {
        RegisterNote();
        score += 200;
        Debug.Log("Good hit");
    }

    public void PerfectHit()
    {
        RegisterNote();
        score += 300;
        Debug.Log("Perfect hit");
    }

    public void RainbowHit()
    {
        RegisterNote();
        score += 350;
        Debug.Log("Marvelous hit");
    }

    public void RegisterNote()
    {
        //notesToBePressed[note.Button].Enqueue(note);
        //Debug.Log("Note hit");
    }

    //public void OutdateNote(NoteBehaviourScript note)
    //{
    //    if (!notesToBePressed.TryGetValue(note.Button, out var q) || q.Count == 0 || q.Dequeue() != note)
    //        throw new Exception("Note outdating before registering");
    //    DecreaseScore();
    //}

    //public void ReceiveSignal(ButtonBehaviourScript button)
    //{
    //    if (notesToBePressed[button].Count != 0)
    //    {
    //        Debug.Log(button + "pressed");
    //        //var note = notesToBePressed[button].Dequeue();
    //        var accuracy = note.GetPressed();
    //        IncreaseScore();
    //    }
    //}

    void IncreaseScore()
    {
        Debug.Log("Score +");
    }

    void DecreaseScore()
    {
        Debug.Log("Score -");
    }
}
