using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndingScreen : MonoBehaviour
{
    public static int Score { get; set; }
    public static int MaxCombo { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        scoreDisplayer.text = Score.ToString();
        comboDisplayer.text = MaxCombo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Song Selection");
    }

    [SerializeField] private TMP_Text scoreDisplayer;
    [SerializeField] private TMP_Text comboDisplayer;
}
