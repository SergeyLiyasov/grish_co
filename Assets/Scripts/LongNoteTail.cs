using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoteTail : MonoBehaviour
{
    public Sprite Sprite { set => GetComponent<SpriteRenderer>().sprite = value; }
    public float SpawnTime => DestinationTime - Conductor.Instance.BeatsFromSpawnToDestination;
    public float DestinationTime { get; set; }
    public int Column { get; set; }
    public float LengthInBeats { get; set; }

    void Start()
    {
        spawnPoint = GameManager.Instance.GetColumnPosition(Column);
        destinationPoint = new Vector2(spawnPoint.x, -2.5f);
    }

    void Update()
    {
        Move();
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
