using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite Sprite;
    public Sprite PressedSprite; 
    public KeyCode Key;
    public BaseNote PressedNote;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            PressedNote = GameManager.Instance.ReceiveSignal(this);
            if (PressedNote != null)
                PressedNote.WasPressed = true;
            Debug.Log(PressedNote);
            spriteRenderer.sprite = PressedSprite;
        }
        else if (Input.GetKeyUp(Key))
        {
            spriteRenderer.sprite = Sprite;
        }
    }
}
