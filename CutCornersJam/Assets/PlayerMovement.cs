using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public bool turnFinished;

    [SerializeField] PlayerInput input;
    public void ChangeActionMap(string map){
        input.SwitchCurrentActionMap(map);
    }

    private void Update() {

        
    }

}
