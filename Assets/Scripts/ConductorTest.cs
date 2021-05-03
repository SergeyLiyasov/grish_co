using System.Collections;
using UnityEngine;

public class ConductorTest : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite unpressedSprite;
    [SerializeField] private Sprite pressedSprite;
    float lastBeat;
    int beatNumber;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastBeat = -Conductor.Instance.secPerBeat;
        beatNumber = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Conductor.Instance.songPosition > lastBeat + Conductor.Instance.secPerBeat)
        {
            if ((beatNumber + 1) % 4 == 0)
                StartCoroutine(Flash());
            lastBeat += Conductor.Instance.secPerBeat;
            beatNumber++;
            Debug.Log(beatNumber);
            Debug.Log(lastBeat);
            Debug.Log(Conductor.Instance.offset);
        }
    }

    public IEnumerator Flash()
    {
        spriteRenderer.sprite = pressedSprite;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = unpressedSprite;
    }
}
