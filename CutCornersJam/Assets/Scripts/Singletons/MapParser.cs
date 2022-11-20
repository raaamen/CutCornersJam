using System;
using System.Collections.Generic;
using System.IO;
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

public class BeatMap
{
    public AudioClip SongClip { get; set; }
    public float SongLength { get; set; }
    public float StartOffset { get; set; }
    public int BPM { get; set; }
    public List<Beat> Beats { get; set; } = new List<Beat>();

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

public static class MapParser
{
    public readonly static Dictionary<char, Ingredient> charToIngredient = new Dictionary<char, Ingredient>
    {
        ['c'] = Ingredient.Cheese,
        ['p'] = Ingredient.Pepperoni,
        ['i'] = Ingredient.Pineapples,
        ['m'] = Ingredient.Mushroom,
    };

    private static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    public static BeatMap ParseMap(string text)
    {
        Stream stream = GenerateStreamFromString(text);
        StreamReader reader = new StreamReader(stream);

        BeatMap map = new BeatMap();
        do
        {
            int currBeat = 0;
            while (!reader.EndOfStream)
            {
                char nextChar = (char)reader.Read();

                if (nextChar == '\n')
                    break;
                else if (nextChar == '-')
                    currBeat++;
                else if (charToIngredient.TryGetValue(nextChar, out Ingredient ingredient))
                {
                    Beat existingBeat = map.Beats.Find(x => x.BeatNumber == currBeat);
                    if (existingBeat == null)
                    {
                        existingBeat = new Beat(currBeat, Ingredient.None);
                        map.Beats.Add(existingBeat);
                    }
                    existingBeat.Ingredients |= ingredient;
                }
            }
        } while (!reader.EndOfStream);

        stream.Close();
        map.Beats.Sort((Beat a, Beat b) => a.BeatNumber - b.BeatNumber);
        return map;
    }

    public static BeatMap ParseMap(TextAsset file)
    {
        return ParseMap(file.text);
    }
}
