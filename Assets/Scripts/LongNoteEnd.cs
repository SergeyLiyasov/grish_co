using System;
using UnityEditor;
using UnityEngine;

public class LongNoteEnd : BaseNote
{
    public override Button Button => Beginning.Button;
    public override float SpawnTime => DestinationTime - Conductor.Instance.BeatsFromSpawnToDestination;
    public override float DestinationTime { get; set; }
    public override int Column { get => Beginning.Column; set => Beginning.Column = value; }
    public LongNoteBeginning Beginning { get; set; }

    public new void Start()
    {
        base.Start();
        Beginning.ShouldSpawnTails = false;
    }

    private void Update()
    {
        Move();
    }

    public override int ReceiveSignal(bool activating)
    {
        if (activating ||
            !Beginning.PressingTime.HasValue ||
            Button.PressingTime > Beginning.PressingTime)
            return 0;

        var buttonPosition = Button.GetComponent<Transform>().position.y;
        var distance = Mathf.Abs(transform.position.y - buttonPosition);

        var endPressingScore = GetPressingScore(distance);

        gameObject.SetActive(false);
        return (Beginning.PressingScore + endPressingScore) / 2;
    }

    private int GetPressingScore(float distance)
    {
        if (distance > 2)
        {
            GameManager.Instance.DisplayHitComment("LEnd: Okay");
            return 100;
        }
        if (distance > 0.7)
        {
            GameManager.Instance.DisplayHitComment("LEnd: Good");
            return 200;
        }
        if (distance > 0.2)
        {
            GameManager.Instance.DisplayHitComment("LEnd: Perfect");
            return 300;
        }
        GameManager.Instance.DisplayHitComment("LEnd: Marvelous");
        return 320;
    }
}