using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public abstract class BaseNote : MonoBehaviour
{
    public abstract Button Button { get; }
    public abstract int ReceiveSignal(bool activating);

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Activator")
        {
            GameManager.Instance.RegisterNote(this);
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Activator")
        {
            GameManager.Instance.OutdateNote(this);
        }
    }
}
