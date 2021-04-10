using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviourScript : MonoBehaviour
{
    private SpriteRenderer Renderer;
    public Sprite Sprite;
    public Sprite PressedSprite; 
    public KeyCode Key;

    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(Key))
            Renderer.sprite = PressedSprite;
        else if (Input.GetKeyUp(Key))
            Renderer.sprite = Sprite;
    }
}
