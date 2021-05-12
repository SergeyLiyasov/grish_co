using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Note : BaseNote
{
    public override Button Button => GameManager.Instance.NoteButtons[Column];
    public override float SpawnTime => DestinationTime - Conductor.Instance.BeatsFromSpawnToDestination;
    public override float DestinationTime { get; set; }
    public override int Column { get; set; }

    private void Update()
    {
        Move();
    }

    public override int ReceiveSignal(bool activating)
    {
        if (!activating) return 0;
        var buttonPosition = Button.GetComponent<Transform>().position.y;
        var distance = Mathf.Abs(transform.position.y - buttonPosition);
        int score = GetPressingScore(distance);

        gameObject.SetActive(false);
        return score;
    }

    private int GetPressingScore(float distance)
    {
        if (distance > 1)
        {
            GameManager.Instance.DisplayHitComment("Okay");
            return 100;
        }
        if (distance > 0.35)
        {
            GameManager.Instance.DisplayHitComment("Good");
            return 200;
        }
        if (distance > 0.05)
        {
            GameManager.Instance.DisplayHitComment("Perfect");
            return 300;
        }
        GameManager.Instance.DisplayHitComment("Marvelous");
        return 320;
    }

    [SerializeField] private Button button;
}
