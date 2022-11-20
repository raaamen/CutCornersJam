using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public enum CurrentPlayer{
        Player1,
        Player2
    }
    private CurrentPlayer _currentPlayer;
    private CurrentPlayer CurrentActivePlayer{
        get {return _currentPlayer;}
        set {
            _currentPlayer = value;
            switch (_currentPlayer)
            {
                case CurrentPlayer.Player1:
                ChangeActionMap("Player1");
                break;

                case CurrentPlayer.Player2:
                ChangeActionMap("Player2");
                break;
            }
        }
    }

    public bool turnFinished;

    [SerializeField] PlayerInput input;
    public void ChangeActionMap(string map){
        input.SwitchCurrentActionMap(map);
    }

    private void Update() {
        
        if (GameManager.Instance.CurrentGameState == GameManager.CurrentState.Player1 || 
            GameManager.Instance.CurrentGameState == GameManager.CurrentState.Player2)
        {
            
        }

        
    }

}
