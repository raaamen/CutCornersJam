using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Flags]
public enum Ingredient
{
    None = 0,
    Pineapples = 1 << 1,
    Pepperoni = 1 << 2,
    Mushroom = 1 << 3,
    Cheese = 1 << 4,
}

public class Beat
{
    public int BeatNumber { get; set; }
    public Ingredient Ingredients { get; set; }

    public Beat(int beatNumber, Ingredient ingredients)
    {
        BeatNumber = beatNumber;
        Ingredients = ingredients;
    }

    public override string ToString()
    {
        return $"[{BeatNumber}, [{string.Join(", ", Ingredients.GetIndividualFlags())}]]";
    }
}

[CreateAssetMenu(fileName = "Beat Map")]
public class BeatMap : ScriptableObject
{
    // Saved variables
    [field: SerializeField]
    public AudioClip SongClip { get; set; }
    [SerializeField]
    private string songLength;
    public float SongLength { get; set; }
    [SerializeField]
    private string startOffset;
    public float StartOffset { get; set; }
    [field: SerializeField]
    public int BPM { get; set; }
    [TextArea(5, int.MaxValue)]
    public string BeatTabsText;

    // Runtime variables
    public List<Beat> Beats { get; set; } = new List<Beat>();

    public void Initialize()
    {
        SongLength = (float)TimeSpan.Parse(songLength).TotalSeconds;
        StartOffset = (float)TimeSpan.Parse(startOffset).TotalSeconds;
        MapParser.ParseMap(this);
    }

    public float BeatToDeg(int beat)
    {
        return (BeatToTime(beat) / SongLength) * 360;
    }

    public float BeatToTime(int beat)
    {
        return (float)beat * 60 / BPM;
    }

    public int TimeToBeat(float time)
    {
        return (int)(time / 60 * BPM);
    }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.Append(
@$"BPM: {BPM}
Offset: {StartOffset}
Length: {SongLength}
[
"
        );
        foreach (Beat beat in Beats)
        {
            result.Append("\t");
            result.Append(beat.ToString());
            result.Append("\n");
        }
        result.Append("]");
        return result.ToString();
    }
}
