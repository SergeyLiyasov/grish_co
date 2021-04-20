using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoteEnd : BaseNote
{
    void Start()
    {
        GameManager.Instance.NoteButtons.Add(Button);
    }
    
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Activator")
        {
            GameManager.Instance.RegisterNote(this);
            CanBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Activator" && !WasPressed)
        {
            Debug.Log(this);
            GameManager.Instance.OutdateNote(this);
            CanBePressed = false;
        }
    }
}
