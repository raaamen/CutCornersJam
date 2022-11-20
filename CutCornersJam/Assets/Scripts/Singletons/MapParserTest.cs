using UnityEngine;

public class MapParserTest : MonoBehaviour
{
    [SerializeField]
    public BeatMap map;

    private void Awake()
    {
        Debug.Log("Map:");
        Debug.Log(map);
        map.Initialize(); ;
        Debug.Log("Map Init:");
        Debug.Log(map);
    }
}
