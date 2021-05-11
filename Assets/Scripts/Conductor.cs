using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public static Conductor Instance { get; private set; }
    public float SongPosition { get; private set; }
    [SerializeField] public float Bpm { get; set; }
    public float SecPerBeat { get; set; }
    public float MapOffset { get; set; }
    [SerializeField] public float Offset { get; set; }
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
        BeatsShownInAdvance = 2.5f;
        globalOffset = 0.1f;
        Offset = 2.435f + globalOffset;
        Bpm = 200;
        SecPerBeat = 60 / Bpm;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (firstTimeCalculation)
        {
            deltaSongPos = (float)AudioSettings.dspTime;
            audioSource.Play();
            Debug.Log("Song pos on start: " + deltaSongPos);
            SongPosition = (float)(AudioSettings.dspTime - deltaSongPos - Offset);
            firstTimeCalculation = false;
        }
        else if ((float)(AudioSettings.dspTime - deltaSongPos - Offset) > SongPosition)
        {
            SongPosition = (float)(AudioSettings.dspTime - deltaSongPos - Offset);
            //Debug.Log("Song pos on update: " + SongPositionInBeats);
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
    private float deltaSongPos;
    private float globalOffset;
    private bool firstTimeCalculation = true;
    private GameObject Sound;
}