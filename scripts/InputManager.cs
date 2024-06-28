using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerActions playerActions;
    PlayerMovement playerMovement;
    playerLook Look;

    void Awake()
    {
       playerInput = new PlayerInput();
       playerActions = playerInput.Player;
       playerMovement =GetComponent<PlayerMovement>();
       Look = GetComponent<playerLook>();
       playerActions.Jump.performed += ctx => playerMovement.Jump();
       playerActions.Sprint.performed += ctx => playerMovement.Sprint();
       playerActions.Crouch.performed += ctx => playerMovement.Crouch();
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }
    
    private void OnDisable()
    {
        playerActions.Disable();
    }
    void Update()
    {
        playerMovement.ProcessMove(playerActions.Move.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        Look.ProcessLook(playerActions.Look.ReadValue<Vector2>());  
    }
}
