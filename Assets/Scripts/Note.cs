using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Note : BaseNote
{
    void Start()
    {
        GameManager.Instance.NoteButtons.Add(Button);
    }
    
    void Update()
    {
        if (CanBePressed && Button.PressedNote == this)
        {
            var buttonPosition = Button.GetComponent<Transform>().position.y;

            if (Mathf.Abs(transform.position.y - buttonPosition) > 1)
                GameManager.Instance.NormalHit();
            else if (Mathf.Abs(transform.position.y - buttonPosition) > 0.35)
                GameManager.Instance.GoodHit();
            else if (Mathf.Abs(transform.position.y - buttonPosition) > 0.05)
                GameManager.Instance.PerfectHit();
            else
                GameManager.Instance.RainbowHit();

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Activator")
        {
            Debug.Log("Note registered " + this);
            GameManager.Instance.RegisterNote(this);
            CanBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Activator" && !WasPressed)
        {
            Debug.Log("Note exited " + this);
            GameManager.Instance.OutdateNote(this);
            CanBePressed = false;
        }
    }
}
