using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ISynchronizer synchronizer;
    [SerializeField] private INotePrefabsReader reader;

    [SerializeField] private GameObject scroller;

    [SerializeField] private GameObject notePrefab;
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject endPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        if (reader.Current == null)
        {
            reader.MoveNext();
        }
        else if (reader.Current.Time == synchronizer.Time)
        {
            var type = reader.Current.Type == "start" ? startPrefab
                : reader.Current.Type == "end" ? endPrefab
                : reader.Current.Type == "note" ? notePrefab
                : throw new ArgumentException();

            var position = GameManager.Instance.GetColumnPosition(reader.Current.Column);

            Instantiate(type, position, Quaternion.identity, scroller.transform);
            reader.MoveNext();
        }
    }
}