using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NoteBehaviourScript : MonoBehaviour
{
    public GameManager GM;
    public ButtonBehaviourScript Button;
    private bool wasPressed;

    void Start()
    {
        
    }
    
    void Update()
    {
        transform.position += new Vector3(0, -1.5f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Activator")
        {
            GM.RegisterNote(this);
        }
    }
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Activator" && !wasPressed)
        {
            GM.OutdateNote(this);
        }
    }

    public double GetPressed()
    {
        wasPressed = true;
        return 1;
    }
}
