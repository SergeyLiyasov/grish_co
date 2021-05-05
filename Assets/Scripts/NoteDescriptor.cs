using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NoteDescriptor
{
    public NoteType NoteType;
    public int SpawnTime;

    public NoteDescriptor(NoteType noteType, int spawnTime)
    {
        NoteType = noteType;
        SpawnTime = spawnTime;
    }
}
