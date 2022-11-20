using UnityEngine;

public class PizzaCuttingManagerTests : MonoBehaviour
{
    [SerializeField]
    private PizzaCuttingManager cuttingManager;
    [SerializeField]
    private BeatMap map;

    private void Awake()
    {
        map.Initialize();
        Debug.Log($"Map: {map}");
        cuttingManager.StartGame(map);
    }
}
