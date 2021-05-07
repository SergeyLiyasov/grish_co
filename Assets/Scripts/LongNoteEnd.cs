using System;
using UnityEditor;
using UnityEngine;

public class LongNoteEnd : BaseNote
{
    public override Button Button => start.Button;
    public override float SpawnTime => DestinationTime - Conductor.Instance.BeatsShownInAdvance;
    public override float DestinationTime { get; set; }
    public override int Column { get => start.Column; set => start.Column = value; }

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
            Debug.Log("LongNoteEnd: Okay hit");
            return 100;
        }
        if (distance > 0.35)
        {
            Debug.Log("LongNoteEnd: Good hit");
            return 200;
        }
        if (distance > 0.05)
        {
            Debug.Log("LongNoteEnd: Perfect hit");
            return 300;
        }
        Debug.Log("LongNoteEnd: Marvelous hit");
        return 320;
    }

    [SerializeField] private LongNoteStart start;
}