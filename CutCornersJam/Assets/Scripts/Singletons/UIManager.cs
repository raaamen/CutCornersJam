using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.UI;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    
}
