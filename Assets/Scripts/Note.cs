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

    public new void Start()
    {
        base.Start();
        particles = GameManager.Instance.Particles[Column];
    }

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
        if (distance > 2)
        {
            GameManager.Instance.DisplayHitComment("Okay");
            particles.Emit(1);
            return 100;
        }
        if (distance > 0.7)
        {
            GameManager.Instance.DisplayHitComment("Good");
            particles.Emit(2);
            return 200;
        }
        if (distance > 0.2)
        {
            GameManager.Instance.DisplayHitComment("Perfect");
            particles.Emit(3);
            return 300;
        }
        GameManager.Instance.DisplayHitComment("Marvelous");
        particles.Emit(4);
        return 320;
    }

    private ParticleSystem particles;
}
