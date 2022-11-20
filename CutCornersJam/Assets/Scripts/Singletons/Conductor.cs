using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

//keeps track of the rhythm and sync
public class Conductor : Singleton<Conductor>
{

    public Song currentSong;

    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    //in case there is offset before beat
    public float firstBeatOffset;

    public GameObject lineToTrackBeat;

    // Start is called before the first frame update
    void Start()
    {
        songBpm = currentSong.BPM;
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = currentSong.songFile;
        secPerBeat = 60f / songBpm;
        dspSongTime = (float)AudioSettings.dspTime;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        songPositionInBeats = songPosition / secPerBeat;
        Debug.Log("current song position: "+songPosition);
        Debug.Log("current song position in beats: "+songPositionInBeats);
    }
}
