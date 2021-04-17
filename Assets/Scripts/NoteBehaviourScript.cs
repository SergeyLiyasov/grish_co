using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NoteBehaviourScript : MonoBehaviour
{
    public ButtonBehaviourScript Button;
    private bool wasPressed;
    public bool canBePressed;
    public bool isLongNote;
    public bool registered = false;
    public GameManager GM;

    void Start()
    {
        //GameManager.instance.NormalHit();
        GM.noteButtons.Add(Button);
    }
    
    void Update()
    {
        if (canBePressed)
        {            
            if (!registered)
            {
                GM.RegisterNote(this);
                registered = true;
            }
                
            if (Button.PressedNote == this)
            {
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
            GameManager.instance.RegisterNote(this);
            canBePressed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Activator" && !wasPressed)
        {
            GameManager.instance.OutdateNote(this);
            canBePressed = false;
        }
    }

    //public double GetPressed()
    //{
    //    wasPressed = true;
    //    return 1;
    //}
}
