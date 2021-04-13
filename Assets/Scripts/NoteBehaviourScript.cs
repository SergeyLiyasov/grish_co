using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NoteBehaviourScript : MonoBehaviour
{
    //public GameManager GM;
    public ButtonBehaviourScript Button;
    private bool wasPressed;
    public bool canBePressed;

    void Start()
    {
        //GM.noteButtons.Add(Button);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(Button.Key) && canBePressed)
        {
            gameObject.SetActive(false);

            var buttonPosition = Button.GetComponent<Transform>().position.y;

            if (Mathf.Abs(transform.position.y - buttonPosition) > 1)
            {
                GameManager.instance.NormalHit();
            }
            else if (Mathf.Abs(transform.position.y - buttonPosition) > 0.35)
            {
                GameManager.instance.GoodHit();
            }
            else if (Mathf.Abs(transform.position.y - buttonPosition) > 0.05)
                GameManager.instance.PerfectHit();
            else
                GameManager.instance.RainbowHit();
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Activator")
        {
            //GM.RegisterNote(this);
            canBePressed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Activator" && !wasPressed)
        {
            //GM.OutdateNote(this);
            canBePressed = false;
        }
    }

    //public double GetPressed()
    //{
    //    wasPressed = true;
    //    return 1;
    //}
}
