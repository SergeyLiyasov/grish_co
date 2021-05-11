using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NoteReader
{
    public Queue<NoteDescriptor>[] NoteQueues { get; private set; }

    public NoteReader()
    {
        NoteQueues = new[]
        {
            new Queue<NoteDescriptor>(new[] { new NoteDescriptor(NoteType.Note, 12), new NoteDescriptor(NoteType.Note, 16), new NoteDescriptor(NoteType.Note, 20) }),
            new Queue<NoteDescriptor>(new[] { new NoteDescriptor(NoteType.Note, 0), new NoteDescriptor(NoteType.Note, 4), new NoteDescriptor(NoteType.Note, 8), new NoteDescriptor(NoteType.Start, 12), new NoteDescriptor(NoteType.End, 16) }),
            new Queue<NoteDescriptor>(new[] { new NoteDescriptor(NoteType.Note, 12), new NoteDescriptor(NoteType.Note, 16), new NoteDescriptor(NoteType.Note, 20) }),
            new Queue<NoteDescriptor>(new[] { new NoteDescriptor(NoteType.Note, 12), new NoteDescriptor(NoteType.Note, 16), new NoteDescriptor(NoteType.Note, 20) })
        };
    }
}