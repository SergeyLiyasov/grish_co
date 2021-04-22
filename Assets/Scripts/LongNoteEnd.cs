using System;
using UnityEditor;
using UnityEngine;

public class LongNoteEnd : BaseNote
{
    [SerializeField] private LongNoteStart start;
    public override Button Button => start.Button;
    public override bool WasPressed { get; set; }

    public override int ReceiveSignal(bool activating)
    {
        if (activating || !start.PressingTime.HasValue || Button.PressedTime > start.PressingTime) return 0;

        var buttonPosition = Button.GetComponent<Transform>().position.y;
        var distance = Mathf.Abs(transform.position.y - buttonPosition);

        var endPressingScore =
            distance > 1 ? NormalHit()
            : distance > 0.35 ? GoodHit()
            : distance > 0.05 ? PerfectHit()
            : RainbowHit();

        gameObject.SetActive(false);
        return (start.PressingScore + endPressingScore) / 2;
    }

    private int NormalHit()
    {
        Debug.Log("LongNoteEnd: Okay hit");
        return 100;
    }

    private int GoodHit()
    {
        Debug.Log("LongNoteEnd: Good hit");
        return 200;
    }

    private int PerfectHit()
    {
        Debug.Log("LongNoteEnd: Perfect hit");
        return 300;
    }

    private int RainbowHit()
    {
        Debug.Log("LongNoteEnd: Marvelous hit");
        return 320;
    }
}