using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : Singleton<UIManager>
{

    public AnimationCurve endGameCurve;

    //start button in main menu
    public Button startButton;

    //DisplayObjects from Surge API
    public DisplayObject tutorialPrompt;
    public DisplayObject loadingScreen;
    public DisplayObject gameEndScreen;
    public GameObject gameEndPanel;

    public StateMachine tutorialStateMachine;
    // Start is called before the first frame update
    
    private void Awake() {
        
    }
    
    void Start()
    {
        startButton.gameObject.SetActive(true);
        
    }

    void OnSelectOption(){
        RunStartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadSceneAsynch(){
        //in case we want to do a dynamic load screen, saving as asynch op
        SceneManager.LoadSceneAsync("sarah-test-scene");
        Debug.Log("loading scene async");
        loadingScreen.Solo();
        Tween.LocalRotation(loadingScreen.transform, loadingScreen.GetComponent<RectTransform>().transform.localEulerAngles, Vector3.negativeInfinity, 1, 0, Tween.EaseLinear);
        yield return null;
    }

    public void RunStartGame(){
        StartCoroutine("LoadSceneAsynch");
    }

    public void EndGameScreenIn(){
        gameEndScreen.Solo();
        Tween.LocalPosition(gameEndPanel.GetComponent<RectTransform>().transform, gameEndPanel.GetComponent<RectTransform>().transform.localPosition, Vector3.zero, 0.5f, 0, Tween.EaseIn);
        Tween.LocalPosition(gameEndScreen.GetComponent<RectTransform>().transform, gameEndScreen.GetComponent<RectTransform>().transform.localPosition, Vector3.zero, 1.3f, 0, Tween.EaseBounce);

    }

    public void StartGameScreenIn(){
        
    }
    
}
