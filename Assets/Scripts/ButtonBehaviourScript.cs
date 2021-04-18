﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviourScript : MonoBehaviour
{
    public GameManager GM;
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
            PressedNote = GM.ReceiveSignal(this);
            if (PressedNote != null)
                PressedNote.wasPressed = true;
            Debug.Log(PressedNote);
            spriteRenderer.sprite = PressedSprite;
        }
        else if (Input.GetKeyUp(Key))
        {
            spriteRenderer.sprite = Sprite;
        }
    }
}
