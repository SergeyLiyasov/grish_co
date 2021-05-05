using UnityEngine;

public class LongNoteStart : BaseNote
{
    public override Button Button => button;
    public override float SpawnTime { get; set; }
    public override int Column { get; set; }
    public int PressingScore { get; set; }
    public double? PressingTime { get; set; }

    public override int ReceiveSignal(bool activating)
    {
        if (!activating) return 0;

        var buttonPosition = Button.GetComponent<Transform>().position.y;
        var distance = Mathf.Abs(transform.position.y - buttonPosition);

        PressingScore = GetPressingScore(distance);
        PressingTime = Time.timeAsDouble;

        gameObject.SetActive(false);
        return 0;
    }

    private int GetPressingScore(float distance)
    {
        if (distance > 1)
        {
            Debug.Log("LongNoteStart: Okay hit");
            return 100;
        }
        if (distance > 0.35)
        {
            Debug.Log("LongNoteStart: Good hit");
            return 200;
        }
        if (distance > 0.05)
        {
            Debug.Log("LongNoteStart: Perfect hit");
            return 300;
        }
        Debug.Log("LongNoteStart: Marvelous hit");
        return 320;
    }

    [SerializeField] private Button button;
}