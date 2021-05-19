using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public static Conductor Instance { get; private set; }
    public float SongPosition { get; private set; }
    //public float Bpm => 60 / SecPerBeat;
    public float MapOffset { get; set; }
    public float Offset { get; set; }
    public float BeatsFromSpawnToDestination { get; set; }
    public static float BeatsShownInAdvance { get; set; }
    public double SixteenthNoteSize { get; private set; }
    public static float Volume { get; set; }
    public static AudioClip Music { get; set; }
    public float SongPositionInBeats => SongPosition / SecPerBeat;
    public float SecPerBeat { get; set; }

    public Conductor() => Instance = this;

    void Start()
    {   
        SixteenthNoteSize = 0.1;
        BeatsFromSpawnToDestination = BeatsShownInAdvance * 2;
        globalOffset = 0.1f;
        Offset = 2.435f + globalOffset;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Music;
        audioSource.volume = Volume;
    }

    void Update()
    {
        if (firstTimeCalculation)
        {
            deltaSongPos = (float)AudioSettings.dspTime;
            audioSource.Play();
            SongPosition = (float)(AudioSettings.dspTime - deltaSongPos - Offset);
            firstTimeCalculation = false;
        }
        else if ((float)(AudioSettings.dspTime - deltaSongPos - Offset) > SongPosition)
        {
            SongPosition = (float)(AudioSettings.dspTime - deltaSongPos - Offset);
        }
        else
        {
            SongPosition += Time.unscaledDeltaTime;
        }
    }
    
    private AudioSource audioSource;
    private float deltaSongPos;
    private float globalOffset;
    private bool firstTimeCalculation = true;
}