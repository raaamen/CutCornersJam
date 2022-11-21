using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutIndicator : MonoBehaviour
{
    public void Construct(float rotationDeg)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotationDeg);
    }
}

public class PizzaCuttingManager : MonoBehaviour
{
    public BeatMap Map { get; set; }
    [SerializeField]
    private GameObject cutIndicatorPrefab;
    [SerializeField]
    private Transform cutIndicatorHolder;
    [SerializeField]
    private Transform cutMarkerTransform;
    private List<CutIndicator> cutIndicators = new List<CutIndicator>();

    private float currTime = 0;
    private int currBeat = 0;
    [SerializeField]
    private AudioSource songAudioSource;
    private Cut currCut;

    public class Cut
    {
        public int BeginningCut { get; set; }
        public int EndCut { get; set; }
        public Ingredient Ingredients { get; set; }
    }

    public KeyCode[] KeyBindings { get; private set; }
    public bool[] PrevKeyState { get; private set; }
    public bool[] KeyState { get; private set; }
    private Coroutine gameCoroutine;

    private bool HasKeyStateChanged()
    {
        for (int i = 0; i < KeyState.Length; i++)
        {
            if (PrevKeyState[i] != KeyState[i])
                return true;
        }
        return false;
    }

    public void StartGame(BeatMap map)
    {
        Map = map;
        if (gameCoroutine != null)
        {
            StopCoroutine(gameCoroutine);
            gameCoroutine = null;
        }
        gameCoroutine = StartCoroutine(GameEnum());
    }

    private IEnumerator GameEnum()
    {
        KeyBindings = new KeyCode[] { KeyCode.D, KeyCode.F, KeyCode.J, KeyCode.K };
        PrevKeyState = new bool[KeyBindings.Length];
        KeyState = new bool[KeyBindings.Length];

        songAudioSource.clip = Map.SongClip;
        songAudioSource.time = Map.StartOffset;
        songAudioSource.Play();

        foreach (CutIndicator inst in cutIndicators)
            Destroy(inst.gameObject);
        cutIndicators.Clear();

        foreach (Beat beat in Map.Beats)
        {
            CutIndicator inst = Instantiate(cutIndicatorPrefab, cutIndicatorHolder).GetComponent<CutIndicator>();
            inst.Construct(Map.BeatToDeg(beat.BeatNumber));
            cutIndicators.Add(inst);
        }

        yield return new WaitForSeconds(Map.StartOffset);

        currTime = 0;
        while (currTime < Map.SongLength)
        {
            yield return null;
            currTime += Time.deltaTime;
            currBeat = Map.TimeToBeat(currTime);
            for (int i = 0; i < KeyState.Length; i++)
                KeyState[i] = Input.GetKeyDown(KeyBindings[i]);
            if (HasKeyStateChanged())
            {
                for (int i = 0; i < KeyState.Length; i++)
                    PrevKeyState[i] = KeyState[i];
            }
        }
        gameCoroutine = null;
    }
}
