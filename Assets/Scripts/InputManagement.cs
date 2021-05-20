using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManagement : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (!Conductor.paused)
                Conductor.paused = true;
            else
            {
                //Conductor.paused = false;
                Conductor.Instance.Resume();
            }
        }
    }

    public void Resume()
    {
        Conductor.Instance.Resume();
    }

    public void Retry()
    {
        SceneManager.LoadScene("Game");
    }

    public void BackToMenu()
    {
        Conductor.paused = false;
        Conductor.Instance.musicStarted = false;
        SceneManager.LoadScene("Song Selection");
    }

    [SerializeField] private KeyCode key;
}
