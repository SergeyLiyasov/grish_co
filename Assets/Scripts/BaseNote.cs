using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public abstract class BaseNote : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
            return spriteRenderer;
        }
    }

    public Vector2 Velocity => (destinationPoint - spawnPoint) / Conductor.Instance.BeatsFromSpawnToDestination;

    public abstract Button Button { get; }
    public abstract float SpawnTime { get; }
    public abstract float DestinationTime { get; set; }
    public abstract int Column { get; set; }

    public abstract int ReceiveSignal(bool activating);

    public void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("ButtonsInteractionCollider"))
        {
            GameManager.Instance.RegisterNote(this);
        }
        else if (otherCollider.CompareTag("NotesDeactivatorCollider"))
        {
            GameManager.Instance.Combo = 0;//может возникнуть некоторый баг
            Destroy(gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("ButtonsInteractionCollider"))
        {
            GameManager.Instance.OutdateNote(this);
        }
    }

    public void Start()
    {
        spawnPoint = GameManager.Instance.GetColumnPosition(Column);
        destinationPoint = new Vector2(spawnPoint.x, -2.5f);
    }

    public void Move()
    {
        var timeDelta = Conductor.Instance.SongPositionInBeats - SpawnTime;
        transform.position = spawnPoint + Velocity * timeDelta;
    }

    private SpriteRenderer spriteRenderer;
    private Vector2 spawnPoint;
    private Vector2 destinationPoint;
}