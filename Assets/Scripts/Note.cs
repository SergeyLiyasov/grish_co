using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Note : BaseNote
{
    [SerializeField] private Button button;
    public override Button Button => button;

    public override bool WasPressed { get; set; }

    public override int ReceiveSignal(bool activating)
    {
        if (!activating) return 0;
        var buttonPosition = Button.GetComponent<Transform>().position.y;
        var distance = Mathf.Abs(transform.position.y - buttonPosition);
        var score =
            distance > 1 ? NormalHit()
            : distance > 0.35 ? GoodHit()
            : distance > 0.05 ? PerfectHit()
            : RainbowHit();

        gameObject.SetActive(false);
        return score;
    }

    private int NormalHit()
    {
        Debug.Log("Okay hit");
        return 100;
    }

    private int GoodHit()
    {
        Debug.Log("Good hit");
        return 200;
    }

    private int PerfectHit()
    {
        Debug.Log("Perfect hit");
        return 300;
    }

    private int RainbowHit()
    {
        Debug.Log("Marvelous hit");
        return 320;
    }
}
