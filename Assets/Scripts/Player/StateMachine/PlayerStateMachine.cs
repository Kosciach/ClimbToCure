using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerBaseState _currentState; public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    private PlayerStateFactory _stateFactory; public PlayerStateFactory StateFactory { get { return _stateFactory; } }

    [SerializeField] string _currentStateName; public string CurrentStateName { get { return _currentStateName; } set { _currentStateName = value; } }



    [Space(20)]
    [Header("====PlayerScripts====")]
    [SerializeField] MovementController _movementController; public MovementController MovementController { get { return _movementController; } }
    [SerializeField] InputController _inputController; public InputController InputController { get { return _inputController;} }
    [SerializeField] CameraController _cameraController; public CameraController CameraController { get { return _cameraController; } }


    //[Space(20)]
    //[Header("====References====")]



    private void Awake()
    {
        _stateFactory = new PlayerStateFactory(this);
        _currentState = _stateFactory.Combat();
        _currentState.StateEnter();
    }


    private void Update()
    {
        _currentState.StateUpdate();
    }
    private void FixedUpdate()
    {
        _currentState.StateFixedUpdate();
    }
}
