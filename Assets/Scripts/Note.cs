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

    void Start()
    {
        startPosition = new Vector2(GameManager.Instance.GetColumnPosition(Column).x, 6.5f);
        buttonPosition = new Vector2(GameManager.Instance.GetColumnPosition(Column).x, -2.5f);
        outOfRangePosition = new Vector2(GameManager.Instance.GetColumnPosition(Column).x, -11.5f);
    }

    void Update()
    {
        transform.position = Vector2.Lerp(
        startPosition,
        outOfRangePosition,
        (Conductor.Instance.BeatsShownInAdvance - (SpawnTime - Conductor.Instance.SongPositionInBeats)) / (2 * Conductor.Instance.BeatsShownInAdvance));
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
    private Vector2 startPosition;
    private Vector2 buttonPosition;
    private Vector2 outOfRangePosition;
}
