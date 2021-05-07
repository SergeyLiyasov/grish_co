using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NoteDescriptor
{
    public NoteType NoteType { get; }
    public float DestinationTime { get; }
    public float SpawnTime => DestinationTime - Conductor.Instance.BeatsShownInAdvance;

    public NoteDescriptor(NoteType noteType, float destinationTime)
    {
        NoteType = noteType;
        DestinationTime = destinationTime;
    }
}
