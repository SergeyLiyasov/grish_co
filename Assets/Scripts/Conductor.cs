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
    public float SongPositionInBeats => SongPosition / SecPerBeat;
    public float BeatsFromSpawnToDestination { get; set; }
    public static float BeatsShownInAdvance { get; set; }
    public double SixteenthNoteSize { get; private set; }
    public static float Volume { get; set; }
    public AudioSource AudioSource;
    public static AudioClip Music { get; set; }

    public Conductor() => Instance = this;

    void Start()
    {   
        SixteenthNoteSize = 0.1;
        //BeatsShownInAdvance = 2f;
        BeatsFromSpawnToDestination = BeatsShownInAdvance * 2;
        globalOffset = 0.1f;
        Offset = 2.435f + globalOffset;
        Bpm = 200;
        SecPerBeat = 60 / Bpm;
        AudioSource = GetComponent<AudioSource>();
        AudioSource.clip = Music;
        AudioSource.volume = Volume;
    }

    void Update()
    {
        if (firstTimeCalculation)
        {
            deltaSongPos = (float)AudioSettings.dspTime;
            AudioSource.Play();
            Debug.Log("Song pos on start: " + Offset);
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
        Debug.Log("Song pos on update: " + SongPosition);
    }
    
    private float deltaSongPos;
    private float globalOffset;
    private bool firstTimeCalculation = true;
}