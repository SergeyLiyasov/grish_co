using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoteBeginning : BaseNote
{
    public override Button Button => GameManager.Instance.NoteButtons[Column];
    public override float SpawnTime => DestinationTime - Conductor.Instance.BeatsFromSpawnToDestination;
    public override float DestinationTime { get; set; }
    public override int Column { get; set; }
    public int PressingScore { get; set; }
    public double? PressingTime { get; set; }
    public bool ShouldSpawnTails { get; set; } = true;

    new public void Start()
    {
        base.Start();
        StartCoroutine(SpawnTail());
    }

    private void Update()
    {
        Move();
    }

    private IEnumerator SpawnTail()
    {
        yield return new WaitForSeconds(5f / 9 * LongNoteTail.Scale * Conductor.Instance.SecPerBeat / -Velocity.y);
        while (ShouldSpawnTails)
        {
            Spawner.Instance.BuildLongNoteTail(this,
                Conductor.Instance.SongPositionInBeats +
                Conductor.Instance.BeatsFromSpawnToDestination);
            yield return new WaitForSeconds(5f / 9 * LongNoteTail.Scale * Conductor.Instance.SecPerBeat / -Velocity.y);
        }
    }

    public override int ReceiveSignal(bool activating)
    {
        if (!activating) return 0;

        var buttonPosition = Button.GetComponent<Transform>().position.y;
        var distance = Mathf.Abs(transform.position.y - buttonPosition);

        PressingScore = GetPressingScore(distance);
        Debug.Log(PressingScore);
        PressingTime = Conductor.Instance.SongPosition;

        SpriteRenderer.enabled = false;
        return 0;
    }

    private int GetPressingScore(float distance)
    {
        if (distance > 2)
        {
            GameManager.Instance.DisplayHitComment("LStart: Okay");
            return 100;
        }
        if (distance > 0.7)
        {
            GameManager.Instance.DisplayHitComment("LStart: Good");
            return 200;
        }
        if (distance > 0.2)
        {
            GameManager.Instance.DisplayHitComment("LStart: Perfect");
            return 300;
        }
        GameManager.Instance.DisplayHitComment("LStart: Marvelous");
        return 320;
    }
}