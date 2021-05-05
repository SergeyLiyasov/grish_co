using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NoteDescriptor
{
    public NoteType NoteType;
    public float SpawnTime;

    public NoteDescriptor(NoteType noteType, float spawnTime)
    {
        NoteType = noteType;
        SpawnTime = spawnTime;
    }
}
