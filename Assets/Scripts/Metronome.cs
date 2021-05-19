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
                StartCoroutine(Flash());
            }
            lastBeat += Conductor.Instance.SecPerBeat;
            beatNumber++;
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
    [SerializeField] private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;
    private float lastBeat;
    private int beatNumber;
}