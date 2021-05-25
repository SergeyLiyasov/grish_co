using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static string PathToDifficulty { get; set; }

    public ParticleSystem[] Particles => particles;
    public List<Button> NoteButtons { get; } = new List<Button>();
    public Queue<BaseNote>[] NotesToBePressed { get; private set; }
    public int Combo { get; set; }
    public int MaxCombo { get; private set; }

    public GameManager() => Instance = this;

    void Start()
    {
        Application.targetFrameRate = 3000;
        NotesToBePressed = new Queue<BaseNote>[NoteButtons.Count];
        for (int i = 0; i < NoteButtons.Count; i++)
        {
            NotesToBePressed[i] = new Queue<BaseNote>();
        }
    }

    private void Update()
    {
        DisplayCombo(Combo);
        GetMaxCombo();
    }

    public void RegisterNote(BaseNote note)
    {
        NotesToBePressed[note.Column].Enqueue(note);
    }

    public void OutdateNote(BaseNote note)
    {
        if (!TryOutdateNote(note)) throw new Exception("Note outdating before registering");
    }

    public bool TryOutdateNote(BaseNote note)
    {
        var q = NotesToBePressed[note.Column];
        if (q.Count == 0 || q.Peek() != note)
            return false;
        NotesToBePressed[note.Column].Dequeue();
        return true;
    }

    public void ReceiveSignal(Button button, bool activating)
    {
        if (NotesToBePressed[button.Column].Count != 0)
        {
            var note = NotesToBePressed[button.Column].Peek();
            score += note.ReceiveSignal(activating);
            Combo++;
        }
    }

    public Vector2 GetColumnPosition(int index)
    {
        return index < columnsNumber
            ? columnsPosition + new Vector2(columnWidth * index, 9)
            : throw new ArgumentException();
    }

    private void GetMaxCombo()
    {
        if (Combo > MaxCombo)
            MaxCombo = Combo;
    }

    public void DisplayHitComment(string str) => hitCommentDisplayer.text = $"Last hit: {str}";
    public void DisplayCombo(int combo) => comboDisplayer.text = $"Combo: {combo}";

    [SerializeField] private Text scoreDisplayer;
    [SerializeField] private Text hitCommentDisplayer;
    [SerializeField] private Text comboDisplayer;
    [SerializeField] private ParticleSystem[] particles;

    public int score
    {
        get => _score;
        set
        {
            _score = value;
            scoreDisplayer.text = $"Score: {value}";
        }
    }

    
    private int _score;
    private int columnsNumber = 4;
    private Vector2 columnsPosition = new Vector2(-2.5f, 6.5f);
    private float columnWidth = 1.5f;
}
