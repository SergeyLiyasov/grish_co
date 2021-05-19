using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        var text = new StreamReader(path).ReadToEnd().Split(new char[]{ '[' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var section in text.Skip(1))
        {
            var header = section.Substring(0, section.IndexOf(']'));
            var body = section.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Where(s => s != string.Empty).Skip(1);
            switch (header)
            {
                case "TimingPoints":
                    ReadTiming(body.ToArray());
                    break;
                case "HitObjects":
                    ReadNotes(body);
                    break;
            }
        }
    }

    private void ReadMeta(string[] metaSection)
    {
        var songName = metaSection[0];
        var artist = metaSection[2];
        var difficultyName = metaSection[5];
        var songMetadata = new SongMetadata(songName, artist, difficultyName, new string[0]);
    }

    private void ReadTiming(string[] timingSection)
    {
        var firstStr = timingSection[0].Split(',');
        Conductor.Instance.Offset = float.Parse(firstStr[0],
            System.Globalization.CultureInfo.InvariantCulture) / 1000;
        Conductor.Instance.SecPerBeat = float.Parse(firstStr[1]) / 1000;
    }

    private void ReadNotes(IEnumerable<string> notesSection)
    {
        foreach (var str in notesSection)
        {
            var strParts = str.Split(new[] { ',', ':' });
            var column = (int.Parse(strParts[0]) - 64) / 128;
            var destTime = (int.Parse(strParts[2]) / 1000f - Conductor.Instance.Offset) / Conductor.Instance.SecPerBeat;
            if (strParts[3] == "1")
            {
                var descriptor = new NoteDescriptor(NoteType.Note, destTime);
                NoteQueues[column].Enqueue(descriptor);
            }
            else if (strParts[3] == "128")
            {
                var descriptor = new NoteDescriptor(NoteType.Beginning, destTime);
                NoteQueues[column].Enqueue(descriptor);

                destTime = (int.Parse(strParts[5]) / 1000f - Conductor.Instance.Offset) / Conductor.Instance.SecPerBeat;
                descriptor = new NoteDescriptor(NoteType.End, destTime);
                NoteQueues[column].Enqueue(descriptor);
            }
        }
    }

    private NoteType GetNoteTypeByString(string str)
    {
        return str switch
        {
            "beginning" => NoteType.Beginning,
            "end" => NoteType.End,
            "note" => NoteType.Note,
            _ => throw new ArgumentException()
        };
    }

    private string path;
}