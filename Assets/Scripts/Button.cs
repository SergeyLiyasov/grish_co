using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Button : MonoBehaviour
{
    public double PressingTime { get; private set; }
    public double ReleasingTime { get; private set; }
    public int Column => column;

    void Start()
    {
        GameManager.Instance.NoteButtons.InsertWithResize(Column, this);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            GameManager.Instance.ReceiveSignal(this, true);
            PressingTime = Conductor.Instance.SongPosition;
            spriteRenderer.sprite = pressedSprite;
        }
        else if (Input.GetKeyUp(key))
        {
            GameManager.Instance.ReceiveSignal(this, false);
            ReleasingTime = Conductor.Instance.SongPosition;
            spriteRenderer.sprite = unpressedSprite;
        }
    }

    [SerializeField] private KeyCode key;
    [SerializeField] private Sprite unpressedSprite;
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private int column;
    private SpriteRenderer spriteRenderer;
}