using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        transform.position += new Vector3(0, -1.5f * Time.deltaTime);
    }
}
