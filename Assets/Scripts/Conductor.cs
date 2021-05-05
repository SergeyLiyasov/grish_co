using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public float SongPosition { get; private set; }
    public static Conductor Instance { get; private set; }
    public float Bpm { get; set; }
    public float SecPerBeat { get; set; }
    public float MapOffset { get; set; }
    public float Offset { get; set; }
    public float LastBeat { get; set; }
    public int BeatNumber { get; set; }
    public int BeatsShownInAdvance { get; set; }
    public float SongPositionInBeats { get; set; } // Нужна только для интерполяции

    public Conductor() => Instance = this;

    void Start()
    {
        LastBeat = -SecPerBeat;
        BeatNumber = 0;
        BeatsShownInAdvance = 2;
        audioSource = GetComponent<AudioSource>();
        Offset = 2.435f;
        Bpm = 200;
        SecPerBeat = 60 / Bpm;
        deltaSongPos = AudioSettings.dspTime;
        audioSource.Play();
    }

    void Update()
    {
        SongPosition = (float)(AudioSettings.dspTime - deltaSongPos - Offset);
        SongPositionInBeats = SongPosition / SecPerBeat;
    }

    [SerializeField] private AudioSource audioSource;
    private double deltaSongPos;
}