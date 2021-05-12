using UnityEngine;

public class LongNoteStart : BaseNote
{
    public override Button Button => GameManager.Instance.NoteButtons[Column];
    public override float SpawnTime => DestinationTime - Conductor.Instance.BeatsFromSpawnToDestination;
    public override float DestinationTime { get; set; }
    public override int Column { get; set; }
    public int PressingScore { get; set; }
    public double? PressingTime { get; set; }
    public LongNoteTail Tail { get; set; }

    private void Update()
    {
        Move();
        Tail.transform.position = transform.position;
    }

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
            GameManager.Instance.DisplayHitComment("LStart: Okay");
            return 100;
        }
        if (distance > 0.35)
        {
            GameManager.Instance.DisplayHitComment("LStart: Good");
            return 200;
        }
        if (distance > 0.05)
        {
            GameManager.Instance.DisplayHitComment("LStart: Perfect");
            return 300;
        }
        GameManager.Instance.DisplayHitComment("LStart: Marvelous");
        return 320;
    }

}