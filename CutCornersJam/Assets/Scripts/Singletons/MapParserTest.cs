using UnityEngine;

public class MapParserTest : MonoBehaviour
{
    private void Awake()
    {
        BeatMap map = MapParser.ParseMap(
@"
| ---c | --i- | ---- | ---- |
| ---p | -p-- | ---- | ---- |
| ---- | ---- | ---- | ---- |
| ---- | ---- | ---- | ---- |
| ---- | ---- | ---- | ---- |
"
        );
        Debug.Log("Map");
        Debug.Log(map);
    }
}
