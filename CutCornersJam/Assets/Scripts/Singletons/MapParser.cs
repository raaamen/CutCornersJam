using System.Collections.Generic;
using System.IO;

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

    public static void ParseMap(BeatMap map)
    {
        Stream stream = GenerateStreamFromString(map.BeatTabsText);
        StreamReader reader = new StreamReader(stream);

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
    }
}
