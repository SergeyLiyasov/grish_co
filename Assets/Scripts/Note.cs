using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Note : BaseNote
{
    public override Button Button => button;
    public override float SpawnTime { get; set; }
    public override int Column { get; set; }

    void Update()
    {
        var columnPosX = GameManager.Instance.GetColumnPosition(Column).x;
        transform.position = Vector3.Lerp(
            new Vector2(columnPosX, 7.2f),
            new Vector2(columnPosX, -5f),
            (Conductor.Instance.SongPositionInBeats - SpawnTime) / (1 * Conductor.Instance.BeatsShownInAdvance)
        );
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
            Debug.Log("Okay hit");
            return 100;
        }
        if (distance > 0.35)
        {
            Debug.Log("Good hit");
            return 200;
        }
        if (distance > 0.05)
        {
            Debug.Log("Perfect hit");
            return 300;
        }
        Debug.Log("Marvelous hit");
        return 320;
    }

    [SerializeField] private Button button;
}
