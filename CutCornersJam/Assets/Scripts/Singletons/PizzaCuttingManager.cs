using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PizzaCuttingManager : MonoBehaviour
{
    public UnityEvent<float> OnGameFinished;
    public UnityEvent OnHitMiss;

    public BeatMap Map { get; set; }
    public int HitBeats { get; private set; }

    [SerializeField]
    private GameObject accuracyIndicatorPrefab;
    [SerializeField]
    private GameObject cutIndicatorPrefab;
    [SerializeField]
    private Transform cutIndicatorHolder;
    [SerializeField]
    private Transform cutMarkerTransform;
    [SerializeField]
    private Transform accuracySpawnerTransform;
    [SerializeField]
    private Transform accuracyIndicatorHolder;
    private Dictionary<Beat, CutIndicator> beatToCutIndicatorsDict = new Dictionary<Beat, CutIndicator>();

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
    private Coroutine gameCoroutine;

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

        songAudioSource.clip = Map.SongClip;
        songAudioSource.time = Map.StartOffset;
        songAudioSource.Play();

        foreach (CutIndicator inst in beatToCutIndicatorsDict.Values)
            Destroy(inst.gameObject);
        beatToCutIndicatorsDict.Clear();
        HitBeats = 0;

        foreach (Beat beat in Map.Beats)
        {
            CutIndicator inst = Instantiate(cutIndicatorPrefab, cutIndicatorHolder).GetComponent<CutIndicator>();
            inst.Construct(Map.BeatToDeg(beat.BeatNumber));
            beatToCutIndicatorsDict.Add(beat, inst);
        }

        yield return new WaitForSeconds(Map.StartOffset);

        currTime = 0;
        while (currTime < Map.SongLength)
        {
            yield return null;
            currTime += Time.deltaTime;
            cutMarkerTransform.eulerAngles = new Vector3(cutMarkerTransform.eulerAngles.x, cutMarkerTransform.eulerAngles.y, (currTime / Map.SongLength) * 360f);
            currBeat = Map.TimeToBeat(currTime);
            bool pressed = false;
            for (int i = 0; i < KeyBindings.Length; i++)
                if (Input.GetKeyUp(KeyBindings[i]))
                {
                    pressed = true;
                    break;
                }
            if (pressed)
            {
                (Beat closestBeat, float accuracy) = Map.GetClosestBeatWithAccuracy(currTime);
                if (closestBeat != null && beatToCutIndicatorsDict.ContainsKey(closestBeat))
                {
                    beatToCutIndicatorsDict[closestBeat].Hit();
                    beatToCutIndicatorsDict.Remove(closestBeat);
                    HitBeats++;
                    Instantiate(accuracyIndicatorPrefab, accuracyIndicatorHolder).GetComponent<AccuracyIndicator>().Construct(accuracy, Color.white, accuracySpawnerTransform.position);
                }
                else
                {
                    OnHitMiss.Invoke();
                }
            }
        }
        OnGameFinished.Invoke(HitBeats / Map.TotalBeats);
        UIManager.Instance.score = HitBeats;
        UIManager.Instance.EndGameScreenIn();
        gameCoroutine = null;
    }
}
