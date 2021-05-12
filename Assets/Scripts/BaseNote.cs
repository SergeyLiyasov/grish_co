using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public abstract class BaseNote : MonoBehaviour
{
    public Sprite Sprite { set => GetComponent<SpriteRenderer>().sprite = value; }
    public abstract Button Button { get; }
    public abstract float SpawnTime { get; }
    public abstract float DestinationTime { get; set; }
    public abstract int Column { get; set; }

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

    private void Start()
    {
        spawnPoint = GameManager.Instance.GetColumnPosition(Column);
        destinationPoint = new Vector2(spawnPoint.x, -2.5f);
    }

    public void Move()
    {
        var timeDelta = Conductor.Instance.SongPositionInBeats - SpawnTime;
        var velocity = (destinationPoint - spawnPoint) / Conductor.Instance.BeatsFromSpawnToDestination;
        transform.position = spawnPoint + velocity * timeDelta;
    }

    private Vector2 spawnPoint;
    private Vector2 destinationPoint;
}
