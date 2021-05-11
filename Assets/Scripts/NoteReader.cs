using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

public class NoteReader
{
    public Queue<NoteDescriptor>[] NoteQueues { get; } =
    {
        new Queue<NoteDescriptor>(),
        new Queue<NoteDescriptor>(),
        new Queue<NoteDescriptor>(),
        new Queue<NoteDescriptor>()
    };

    public NoteReader(string path)
    {
        this.path = path;
        var readingThread = new Thread(Read);
        readingThread.Start();
        readingThread.Join();
    }

    private void Read()
    {
        using var reader = new StreamReader(path);
        while (!reader.EndOfStream)
        {
            var str = reader.ReadLine();
            var match = Regex.Match(str, pattern);

            if (!match.Success)
                throw new FormatException($"Invalid string in file {path}: {str}. Expected pattern {pattern}");

            var descriptor = new NoteDescriptor(
                GetNoteTypeByString(match.Groups["type"].Value),
                float.Parse(match.Groups["destTime"].Value)
                );

            var column = int.Parse(match.Groups["column"].Value);

            NoteQueues[column].Enqueue(descriptor);
        }
    }

    private NoteType GetNoteTypeByString(string str)
    {
        return str switch
        {
            "start" => NoteType.Start,
            "end" => NoteType.End,
            "note" => NoteType.Note,
            _ => throw new ArgumentException()
        };
    }

    private string path;
    private string pattern = @"\s*(?<type>note|start|end)\s+(?<column>\d+)\s+(?<destTime>\d+)[\s.]*";
}