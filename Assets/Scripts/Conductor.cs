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
    public float SongPositionInBeats { get; set; }
    public float BeatsFromSpawnToDestination { get; set; }
    public float BeatsShownInAdvance { get; set; }
    public double SixteenthNoteSize { get; private set; }
    public AudioSource AudioSource;

    public Conductor() => Instance = this;

    void Start()
    {
        SixteenthNoteSize = 0.1;
        BeatsShownInAdvance = 2f;
        BeatsFromSpawnToDestination = BeatsShownInAdvance * 2;
        Offset = 2.435f;
        Bpm = 200;
        SecPerBeat = 60 / Bpm;
        AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (firstTimeCalculation)
        {
            //deltaSongPos = (float)AudioSettings.dspTime;
            AudioSource.Play();
            Debug.Log("Song pos on start: " + deltaSongPos);
            SongPosition = (float)((float)(AudioSource.timeSamples) / AudioSettings.outputSampleRate - Offset);
            firstTimeCalculation = false;
        }
        else if ((float)((float)(AudioSource.timeSamples) / AudioSettings.outputSampleRate - Offset) > SongPosition)
        {
            SongPosition = (float)((float)(AudioSource.timeSamples) / AudioSettings.outputSampleRate - Offset);
            //Debug.Log("Song pos on update: " + SongPositionInBeats);
        }
        else
        {
            SongPosition += Time.unscaledDeltaTime;
        }
        //Debug.Log("Song pos on update: " + SongPosition);
        SongPositionInBeats = SongPosition / SecPerBeat;
    }

    public IEnumerator Flash()
    {
        yield return new WaitForSeconds(0.1f);
    }
    
    private float deltaSongPos;
    private float globalOffset;
    private bool firstTimeCalculation = true;
}