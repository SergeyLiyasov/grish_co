using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Conductor : MonoBehaviour
{
    public static Conductor Instance { get; private set; }
    public float SongPosition { get; private set; }
    //public float Bpm => 60 / SecPerBeat;
    public float MapOffset { get; set; }
    public float Offset { get; set; }
    public float GlobalOffset { get { return 0.11f; } private set { } }
    public float BeatsFromSpawnToDestination { get; set; }
    public static float BeatsShownInAdvance { get; set; }
    public double SixteenthNoteSize { get; private set; }
    public static float Volume { get; set; }
    public static AudioClip Music { get; set; }
    public float SongPositionInBeats => SongPosition / SecPerBeat;
    public float SecPerBeat { get; set; }
    public bool musicStarted = false;
    public static bool paused = false;
    public static float pauseTimeStamp = -1f;
    public static float pausedTime = 0;

    public Conductor() => Instance = this;

    void Start()
    {
        paused = false;
        SixteenthNoteSize = 0.1;
        BeatsFromSpawnToDestination = BeatsShownInAdvance * 2;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Music;
        audioSource.volume = Volume;
        musicLength = Music.length;
        StartCoroutine(CountDown());
    }

    void Update()
    {
        Debug.Log(paused);
        if (!musicStarted)
            return;
        //Debug.Log(deltaSongPos);
        if (paused)
        {
            if (pauseTimeStamp < 0f)
            {
                pauseTimeStamp = (float)AudioSettings.dspTime;
                AudioListener.pause = true;
                pauseCanvas.SetActive(true);
            }
            return;
        }
        else if (pauseTimeStamp > 0f)
        {
            AudioListener.pause = false;
            pauseTimeStamp = -1f;
        }
        if (firstTimeCalculation)
        {
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
        
        if (SongPosition > musicLength + 1)
        {
            EndingScreen.Score = GameManager.Instance.score;
            EndingScreen.MaxCombo = GameManager.Instance.MaxCombo;
            SceneManager.LoadScene("Ending Screen");
        }
    }

    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 1; i++)
        {
            //Debug.Log("Countdown " + i.ToString());
            yield return new WaitForSeconds(1f);
        }
        StartMusic();
    }

    void StartMusic()
    {
        Debug.Log("start");
        audioSource.Play();
        deltaSongPos = (float)AudioSettings.dspTime;
        SongPosition = (float)(AudioSettings.dspTime - deltaSongPos - Offset);
        Debug.Log(SongPosition);
        musicStarted = true;
    }

    public void Resume()
    {
        pauseCanvas.SetActive(false);
        paused = false;
    }


    [SerializeField] private GameObject pauseCanvas;
    private float musicLength;
    private AudioSource audioSource;
    private float deltaSongPos;
    private bool firstTimeCalculation = true;
}