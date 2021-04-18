using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public Dictionary<ButtonBehaviourScript, Queue<BaseNote>> notesToBePressed = new Dictionary<ButtonBehaviourScript, Queue<BaseNote>>();
    public HashSet<ButtonBehaviourScript> noteButtons = new HashSet<ButtonBehaviourScript>();
    public Dictionary<ButtonBehaviourScript, bool> skipNextNote = new Dictionary<ButtonBehaviourScript, bool>();
    public static GameManager instance;
    private int score = 0;

    void Start()
    {
        instance = this;
        foreach (var button in noteButtons)
        {
            //Debug.Log(button + "initiated");
            notesToBePressed[button] = new Queue<BaseNote>();
            skipNextNote[button] = false;
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

    public void RegisterNote(BaseNote note)
    {
        if (skipNextNote[note.Button] == true)
        {
            //Debug.Log(skipNextNote[note.Button]);
            skipNextNote[note.Button] = false;
        }
        else
        {
            notesToBePressed[note.Button].Enqueue(note);
            Debug.Log("Note registered " + note);
        }
    }

    public void OutdateNote(BaseNote note)
    {
        if (!notesToBePressed.TryGetValue(note.Button, out var q) || q.Count == 0 || q.Peek() != note)
            throw new Exception("Note outdating before registering");
        notesToBePressed[note.Button].Dequeue();
        Debug.Log("Note out " + note);
        //DecreaseScore();
    }

    public BaseNote ReceiveSignal(ButtonBehaviourScript button)
    {
        if (notesToBePressed[button].Count != 0 && !(notesToBePressed[button].Peek() is LongNoteEnd))
        { 
            Debug.Log(button + "pressed");
            return notesToBePressed[button].Dequeue();
            //var accuracy = note.GetPressed();
            //IncreaseScore();
        }
        return null;
    }

}
