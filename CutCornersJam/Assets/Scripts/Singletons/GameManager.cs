using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //keeping track of what state the game iss in
    public enum CurrentState{
        Init,
        Tutorial,
        Example,
        Player1,
        Player2,
        End
    }
    CurrentState state;

    private void Awake() {
        state = CurrentState.Init;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //will prompt the players for a tutorial. they can select no
        UIManager.Instance.tutorialPrompt.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this will be the tutorial, the UI will be controlled by a UI state machine from surge
    public IEnumerator Tutorial(){
        state = CurrentState.Tutorial;
        while (!UIManager.Instance.tutorialStateMachine.AtLast)
        {
            if (Input.GetMouseButtonDown(0))
            {
                UIManager.Instance.tutorialStateMachine.Next();
            }
        }
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        
    }

    

    //loading scene asynchronously for a cool loading screen
    public IEnumerator LoadSceneAsynch(string gameSceneName){
        //in case we want to do a dynamic load screen, saving as asynch op
        AsyncOperation op = SceneManager.LoadSceneAsync(gameSceneName);
        UIManager.Instance.loadingScreen.Solo();
        yield return null;
    }

}
