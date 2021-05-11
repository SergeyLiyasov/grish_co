using System.Collections;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastBeat = 0;
        beatNumber = 0;
    }

    void Update()
    {
        if (Conductor.Instance.SongPosition >= lastBeat)
        {
            if (beatNumber % 4 == 0)
            {
                //audioSource.Play();
                Debug.Log($"time: {Conductor.Instance.SongPosition}");
                
                StartCoroutine(Flash());
            }
            lastBeat += Conductor.Instance.SecPerBeat;
            beatNumber++;
            //Debug.Log($"Beat number: {beatNumber}");
            //Debug.Log($"Last beat Metro: {lastBeat}");
            //Debug.Log($"SongPositionInBeats: {Conductor.Instance.SongPositionInBeats}");
        }
    }

    public IEnumerator Flash()
    {
        spriteRenderer.sprite = pressedSprite;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = unpressedSprite;
    }

    [SerializeField] private Sprite unpressedSprite;
    [SerializeField] private Sprite pressedSprite;
    private SpriteRenderer spriteRenderer;
    private float lastBeat;
    private int beatNumber;
    [SerializeField] private AudioSource audioSource;
}