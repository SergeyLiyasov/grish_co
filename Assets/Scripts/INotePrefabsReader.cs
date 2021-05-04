using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INotePrefabsReader
{
    public NotePrefabDescriptor Current { get; }

    public bool MoveNext();

    class NotePrefabDescriptor
    {
        public double Time { get; }
        public NoteType Type { get; }
        public int Column { get; }
    }
}