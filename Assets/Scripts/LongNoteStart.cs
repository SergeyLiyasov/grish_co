using System;
using UnityEngine;

public class LongNoteStart : BaseNote
{
    void Start()
    {
        GameManager.Instance.NoteButtons.Add(Button);
    }
    
    void Update()
    {
        if (WasPressed)
        {
            var buttonPosition = Button.GetComponent<Transform>().position;
            if (Input.GetKey(Button.Key))
            {
                transform.position = new Vector3(transform.position.x, buttonPosition.y);
            }
            else if (GameManager.Instance.NotesToBePressed[Button].Count == 0)
            {
                Debug.Log(GameManager.Instance.NotesToBePressed[Button].Count);
                Debug.Log("Long note pressed badly");
                gameObject.SetActive(false);
                GameManager.Instance.SkipNextNote[Button] = true;
            }
            else if (GameManager.Instance.NotesToBePressed[Button].Peek() is LongNoteEnd)
            {
                Debug.Log(GameManager.Instance.NotesToBePressed[Button].Peek());
                var noteEnd = GameManager.Instance.NotesToBePressed[Button].Dequeue();
                noteEnd.WasPressed = true;
                noteEnd.gameObject.SetActive(false);
                gameObject.SetActive(false);
                Debug.Log("Long note pressed correctly");
            }
            else
            {
                throw new Exception("Next object in queue is not LongNoteEnd");
            }
        }
        else if (CanBePressed && Button.PressedNote == this)
        {
            WasPressed = true;
        }
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
