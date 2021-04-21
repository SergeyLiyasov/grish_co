using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public abstract class BaseNote : MonoBehaviour
{
    public Button Button;
    public bool WasPressed { get; set; }
    public bool CanBePressed { get; set; }
}