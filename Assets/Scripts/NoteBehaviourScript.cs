using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NoteBehaviourScript : BaseNote
{
    void Start()
    {
        GM.noteButtons.Add(Button);
    }
    
    void Update()
    {
        if (canBePressed)
        {
            if (Button.PressedNote == this)
            {
                //wasPressed = true;

                var buttonPosition = Button.GetComponent<Transform>().position.y;

                if (Mathf.Abs(transform.position.y - buttonPosition) > 1)
                    GameManager.instance.NormalHit();
                else if (Mathf.Abs(transform.position.y - buttonPosition) > 0.35)
                    GameManager.instance.GoodHit();
                else if (Mathf.Abs(transform.position.y - buttonPosition) > 0.05)
                    GameManager.instance.PerfectHit();
                else
                    GameManager.instance.RainbowHit();

                gameObject.SetActive(false);
                
            }
        }              
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Activator")
        {
            Debug.Log("Note registered " + this);
            GameManager.instance.RegisterNote(this);
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Activator" && !wasPressed)
        {
            Debug.Log("Note exited " + this);
            GameManager.instance.OutdateNote(this);
            canBePressed = false;
        }
    }
}
