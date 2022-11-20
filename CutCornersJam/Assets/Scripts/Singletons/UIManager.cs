using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    //start button in main menu
    public Button startButton;

    //DisplayObjects from Surge API
    public DisplayObject tutorialPrompt;
    public DisplayObject loadingScreen;

    public StateMachine tutorialStateMachine;
    // Start is called before the first frame update
    
    private void Awake() {
        
    }
    
    void Start()
    {
        startButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadSceneAsynch(){
        //in case we want to do a dynamic load screen, saving as asynch op
        SceneManager.LoadSceneAsync("sarah-test-scene");
        Debug.Log("loading scene async");
        UIManager.Instance.loadingScreen.Solo();
        yield return null;
    }

    public void RunStartGame(){
        StartCoroutine("LoadSceneAsynch");
    }
    
}
