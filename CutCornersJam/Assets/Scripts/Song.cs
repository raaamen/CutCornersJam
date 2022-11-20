using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Song")]
public class Song : ScriptableObject
{
    //Stores information + file of a song
    public AudioClip songFile;
    public string name;
    public float BPM;
    public float lengthInSeconds;
    public float offset;
}
