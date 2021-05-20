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

    public new void Start()
    {
        base.Start();
        transform.localScale = new Vector2(transform.localScale.x, Scale);
        particles = GameManager.Instance.Particles[Column];
    }

    void Update()
    {
        Move();
        if (inCollider &&
            Beginning.PressingTime.HasValue &&
            Button.ReleasingTime < Beginning.PressingTime)
        {
            if (Random.value <= 0.03f) particles.Emit(1);
            SpriteRenderer.sortingLayerName = "Activated note tails";
            SpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }
    }

    public override int ReceiveSignal(bool activating) => 0;

    new public void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("ButtonsInteractionCollider"))
            inCollider = true;
        else if (otherCollider.CompareTag("NotesDeactivatorCollider"))
            Destroy(gameObject);
    }
    new public void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("ButtonsInteractionCollider"))
            inCollider = false;
    }

    private ParticleSystem particles;
    private bool inCollider;
}
