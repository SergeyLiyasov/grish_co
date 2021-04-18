using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoteEnd : BaseNote
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
