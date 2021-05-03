using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public float bpm = 200;
    public float secPerBeat;
    public float songPosition;
    public float deltaSongPos;
    public readonly float offset = 2.435f;
    public float mapOffset;
    public AudioSource audioSource;

    public static Conductor Instance { get; private set; }

    public Conductor() => Instance = this;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        secPerBeat = 60 / bpm;
        deltaSongPos = (float)AudioSettings.dspTime;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - deltaSongPos - offset);
    }
}
