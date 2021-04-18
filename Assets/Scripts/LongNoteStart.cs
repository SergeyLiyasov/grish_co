using System;
using UnityEngine;

public class LongNoteStart : BaseNote
{
    // Start is called before the first frame update
    void Start()
    {
        GM.noteButtons.Add(Button);
    }

    // Update is called once per frame
    void Update()
    {
        if (canBePressed)
        {
            if (Button.PressedNote == this)
            {
                wasPressed = true;
            }
        }
        if (wasPressed)
        {
            var buttonPosition = Button.GetComponent<Transform>().position;
            if (Input.GetKey(Button.Key))
            {
                transform.position = new Vector3(transform.position.x, buttonPosition.y);
            }
            else if (GM.notesToBePressed[Button].Count == 0)
            {
                Debug.Log(GM.notesToBePressed[Button].Count);
                Debug.Log("Long note pressed badly");
                gameObject.SetActive(false);
                GM.skipNextNote[Button] = true;
            }
            else if (GM.notesToBePressed[Button].Peek() is LongNoteEnd)
            {
                Debug.Log(GM.notesToBePressed[Button].Peek());
                var noteEnd = GM.notesToBePressed[Button].Dequeue();
                noteEnd.wasPressed = true;
                noteEnd.gameObject.SetActive(false);
                gameObject.SetActive(false);
                Debug.Log("Long note pressed correctly");
            }
            else
            {
                throw new Exception("Next object in queue is not LongNoteEnd");
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Activator")
        {
            GameManager.instance.RegisterNote(this);
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Activator" && !wasPressed)
        {
            Debug.Log(this);
            GameManager.instance.OutdateNote(this);
            canBePressed = false;
        }
    }
}
