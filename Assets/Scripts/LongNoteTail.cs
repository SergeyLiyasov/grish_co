using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoteTail : BaseNote
{
    public static float Scale => 0.8f;
    public float LengthInBeats { get; set; }
    public override Button Button => Beginning.Button;
    public override float SpawnTime => DestinationTime - Conductor.Instance.BeatsFromSpawnToDestination;
    public override float DestinationTime { get; set; }
    public override int Column { get => Beginning.Column; set => Beginning.Column = value; }
    public LongNoteBeginning Beginning { get; set; }

    new public void Start()
    {
        base.Start();
        transform.localScale = new Vector2(transform.localScale.x, Scale);
    }

    void Update()
    {
        Move();
        if (inCollider &&
            Beginning.PressingTime.HasValue &&
            Button.ReleasingTime < Beginning.PressingTime)
        {
            SpriteRenderer.sortingLayerName = "Activated note tails";
            SpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }
    }
    //public void Move()
    //{
    //    var timeDelta = Conductor.Instance.SongPositionInBeats - SpawnTime;
    //    var velocity = (destinationPoint - spawnPoint) / Conductor.Instance.BeatsFromSpawnToDestination;
    //    transform.position = spawnPoint + velocity * timeDelta;
    //}

    public override int ReceiveSignal(bool activating) => 0;

    new public void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("ButtonsInteractionCollider"))
            inCollider = true;
    }
    new public void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("ButtonsInteractionCollider"))
            inCollider = false;
    }

    private bool inCollider;
    //gameObject.SetActive(false);
    //private Vector2 spawnPoint;
    //private Vector2 destinationPoint;
}
