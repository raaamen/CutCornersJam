using UnityEngine;

public class PizzaCuttingManagerTests : MonoBehaviour
{
    [SerializeField]
    private PizzaCuttingManager cuttingManager;
    private void Awake()
    {
        BeatMap map = MapParser.ParseMap(
@"BPM: 150
Offset: 0:24
Length: 3:14
| ---c | --i- | ---- | ---- |
| ---p | -p-- | ---- | ---- |
| ---- | ---- | ---- | ---- |
| ---- | ---- | ---- | ---- |
| ---- | ---- | ---- | ---- |
"
        );
        Debug.Log($"Map: {map}");
        cuttingManager.StartGame(map);
    }
}
