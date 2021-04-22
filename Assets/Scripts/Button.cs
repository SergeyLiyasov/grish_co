using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Sprite unpressedSprite;
    [SerializeField] private Sprite pressedSprite;
    private SpriteRenderer spriteRenderer;
    public KeyCode Key;
    public double PressedTime { get; set; }

    void Start()
    {
        GameManager.Instance.NoteButtons.Add(this);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            GameManager.Instance.ReceiveSignal(this, true);
            PressedTime = Time.timeAsDouble;
            spriteRenderer.sprite = pressedSprite;
        }
        else if (Input.GetKeyUp(Key))
        {
            GameManager.Instance.ReceiveSignal(this, false);
            spriteRenderer.sprite = unpressedSprite;
        }
    }
}