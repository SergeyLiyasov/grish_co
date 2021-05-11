using System;
using UnityEditor;
using UnityEngine;

public class LongNoteEnd : BaseNote
{
    public override Button Button => start.Button;
    public override float SpawnTime => DestinationTime - Conductor.Instance.BeatsShownInAdvance;
    public override float DestinationTime { get; set; }
    public override int Column { get => start.Column; set => start.Column = value; }
    public LongNoteStart Start { get => start; set => start = value; }

    private void Update()
    {
        Move();
    }

    public override int ReceiveSignal(bool activating)
    {
        if (activating || !start.PressingTime.HasValue || Button.PressedTime > start.PressingTime) return 0;

        var buttonPosition = Button.GetComponent<Transform>().position.y;
        var distance = Mathf.Abs(transform.position.y - buttonPosition);

        int endPressingScore = GetPressingScore(distance);

        gameObject.SetActive(false);
        return (start.PressingScore + endPressingScore) / 2;
    }

    private int GetPressingScore(float distance)
    {
        if (distance > 1)
        {
            GameManager.Instance.DisplayHitComment("LEnd: Okay");
            return 100;
        }
        if (distance > 0.35)
        {
            GameManager.Instance.DisplayHitComment("LEnd: Good");
            return 200;
        }
        if (distance > 0.05)
        {
            GameManager.Instance.DisplayHitComment("LEnd: Perfect");
            return 300;
        }
        GameManager.Instance.DisplayHitComment("LEnd: Marvelous");
        return 320;
    }

    [SerializeField] private LongNoteStart start;
}