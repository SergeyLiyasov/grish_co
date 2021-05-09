using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public static Conductor Instance { get; private set; }
    public float SongPosition { get; private set; }
    public float Bpm { get; set; }
    public float SecPerBeat { get; set; }
    public float MapOffset { get; set; }
    public float Offset { get; set; }
    public float LastBeat { get; set; }
    public int BeatNumber { get; set; }
    public float TimePassedSinceLastFrame { get; set; }
    public float BeatsShownInAdvance { get; set; }
    public float SongPositionInBeats { get; set; }

    public Conductor() => Instance = this;

    void Start()
    {
        LastBeat = -SecPerBeat;
        BeatNumber = -1;
        BeatsShownInAdvance = 5f;
        Offset = 0.07f;
        Bpm = 200;
        SecPerBeat = 60 / Bpm;
        deltaSongPos = AudioSettings.dspTime;
        audioSource.Play();
        SongPosition = (float)(AudioSettings.dspTime - deltaSongPos - Offset);
        Debug.Log("Song pos on start: " + SongPosition);
    }

    void Update()
    {
        if ((float)(AudioSettings.dspTime - deltaSongPos - Offset) > SongPosition)
        {
            SongPosition = (float)(AudioSettings.dspTime - deltaSongPos - Offset);
        }
        else
        {
            SongPosition += Time.unscaledDeltaTime;
        }
        //Debug.Log("Song pos on update: " + SongPosition);
        SongPositionInBeats = SongPosition / SecPerBeat;
        if (SongPosition > LastBeat + SecPerBeat)
        {
            LastBeat += SecPerBeat;
            BeatNumber++;
        }
    }

    [SerializeField] private AudioSource audioSource;
    private double deltaSongPos;
}