using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public double SongPosition { get; private set; }
    public static Conductor Instance { get; private set; }
    public float Bpm { get; set; }
    public float SecPerBeat { get; set; }
    public float MapOffset { get; set; }
    public float Offset { get; set; }

    public Conductor() => Instance = this;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Offset = 2.435f;
        Bpm = 200;
        SecPerBeat = 60 / Bpm;
        deltaSongPos = AudioSettings.dspTime;
        audioSource.Play();
    }

    void Update()
    {
        SongPosition = AudioSettings.dspTime - deltaSongPos - Offset;
    }

    private AudioSource audioSource;
    private double deltaSongPos;
}