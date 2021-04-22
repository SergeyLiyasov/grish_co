using UnityEngine;

public class LongNoteStart : BaseNote
{
    [SerializeField] private Button button;
    public override Button Button => button;
    public int PressingScore { get; set; }
    public double? PressingTime { get; set; }
    public override bool WasPressed { get; set; }

    public override int ReceiveSignal(bool activating)
    {
        if (!activating) return 0;

        var buttonPosition = Button.GetComponent<Transform>().position.y;
        var distance = Mathf.Abs(transform.position.y - buttonPosition);

        PressingScore =
            distance > 1 ? NormalHit()
            : distance > 0.35 ? GoodHit()
            : distance > 0.05 ? PerfectHit()
            : RainbowHit();
        PressingTime = Time.timeAsDouble;

        gameObject.SetActive(false);
        return 0;
    }

    private int NormalHit()
    {
        Debug.Log("LongNoteStart: Okay hit");
        return 100;
    }

    private int GoodHit()
    {
        Debug.Log("LongNoteStart: Good hit");
        return 200;
    }

    private int PerfectHit()
    {
        Debug.Log("LongNoteStart: Perfect hit");
        return 300;
    }

    private int RainbowHit()
    {
        Debug.Log("LongNoteStart: Marvelous hit");
        return 320;
    }
}