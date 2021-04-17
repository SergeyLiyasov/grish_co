using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public Dictionary<ButtonBehaviourScript, Queue<NoteBehaviourScript>> notesToBePressed = new Dictionary<ButtonBehaviourScript, Queue<NoteBehaviourScript>>();
    //public Dictionary<ButtonBehaviourScript, Queue<NoteBehaviourScript>> pressedNotes = new Dictionary<ButtonBehaviourScript, Queue<NoteBehaviourScript>>();
    public HashSet<ButtonBehaviourScript> noteButtons = new HashSet<ButtonBehaviourScript>();
    public static GameManager instance;
    private int score = 0;

    void Start()
    {
        instance = this;
        foreach (var button in noteButtons)
        {
            Debug.Log(button + "initiated");
            notesToBePressed[button] = new Queue<NoteBehaviourScript>();
            //pressedNotes[button] = new Queue<NoteBehaviourScript>();
        }     
    }

    void Update()
    {

    }

    public void NormalHit()
    {
        //RegisterNote();
        score += 100;
        Debug.Log("Okay hit");
    }

    public void GoodHit()
    {
        //RegisterNote();
        score += 200;
        Debug.Log("Good hit");
    }

    public void PerfectHit()
    {
        //RegisterNote();
        score += 300;
        Debug.Log("Perfect hit");
    }

    public void RainbowHit()
    {
        //RegisterNote();
        score += 320;
        Debug.Log("Marvelous hit");
    }

    public void RegisterNote(NoteBehaviourScript note)
    {
        notesToBePressed[note.Button].Enqueue(note);
        Debug.Log("Note hit");
    }

    public void OutdateNote(NoteBehaviourScript note)
    {
        if (!notesToBePressed.TryGetValue(note.Button, out var q) || q.Count == 0 || q.Dequeue() != note)
            throw new Exception("Note outdating before registering");
        //DecreaseScore();
    }

    public NoteBehaviourScript ReceiveSignal(ButtonBehaviourScript button)
    {
        if (notesToBePressed[button].Count != 0)
        {
            Debug.Log(button + "pressed");
            return notesToBePressed[button].Dequeue();
            //var accuracy = note.GetPressed();
            //IncreaseScore();
        }
        return null;
    }

}
