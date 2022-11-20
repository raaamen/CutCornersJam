using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float actionOffset;
    [SerializeField] private PlayerMovement playerMovementScript;
    //keeping track of what state the game iss in
    public enum CurrentState {
        Init,
        Tutorial,
        Example,
        GameStart,
        Player1,
        Player2,
        End
    }
    private CurrentState _state;
    public CurrentState CurrentGameState{
        get {return _state;}
        set {
            _state = value;
            switch (_state)
            {
                case CurrentState.Init:
                //stuff to initialize game goes here
                InitializeGame();
                playerMovementScript.ChangeActionMap("Menu");
                break;

                case CurrentState.Tutorial:
                StartCoroutine("Tutorial");
                break;

                case CurrentState.GameStart:
                InitializeGame();
                break;

                case CurrentState.Player1:
                StartCoroutine("Player1Turn");
                break;

                case CurrentState.Player2:
                StartCoroutine("Player2Turn");
                break;
            }
        }
    }

    private void Awake() {
        CurrentGameState = CurrentState.Init;
    }

    void InitializeGame(){

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
        CurrentGameState = CurrentState.Tutorial;
        while (!UIManager.Instance.tutorialStateMachine.AtLast)
        {
            if (Input.GetMouseButtonDown(0))
            {
                UIManager.Instance.tutorialStateMachine.Next();
            }
        }
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        CurrentGameState = CurrentState.GameStart;
    }

    //player1 turn
    public IEnumerator Player1Turn(){
        //game stuff goes here
        //most of the actual action will be put in playermovement.cs
        //here is where stuff like timers and score goes
        //switches to player 2 and starts player2turn
        yield return new WaitUntil(() => playerMovementScript.turnFinished);
        CurrentGameState = CurrentState.Player2;
        yield return null;
    }

    //player1 turn
    public IEnumerator Player2Turn(){
        //game stuff goes here such as on screen prompts
        yield return new WaitUntil(() => playerMovementScript.turnFinished);
        CurrentGameState = CurrentState.End;
        yield return null;
    }
    

    //loading scene asynchronously for a cool loading screen
    

}
