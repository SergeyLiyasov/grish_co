using System.Collections;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastBeat = -Conductor.Instance.SecPerBeat;
        beatNumber = -1;
    }

    void Update()
    {
        if (Conductor.Instance.SongPosition > lastBeat + Conductor.Instance.SecPerBeat)
        {
            if ((beatNumber + 1) % 4 == 0)
                StartCoroutine(Flash());
            lastBeat += Conductor.Instance.SecPerBeat;
            beatNumber++;
            //Debug.Log($"Beat number: {beatNumber}");
            //Debug.Log($"Last beat Metro: {lastBeat}");
            Debug.Log($"SongPositionInBeats: {Conductor.Instance.SongPositionInBeats}");
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
}