using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class InputController : MonoBehaviour
{
    [Header("====References====")]
    [SerializeField] Camera _mainCamera;


    [Header("====InputVariables====")]
    [SerializeField] Vector3 _movementVector; public Vector3 MovementVectorInput { get { return _movementVector; } }
    [SerializeField] Vector3 _mousePosition; public Vector3 MousePosition { get { return _mousePosition; } }

    public delegate void InputControllerEvent();
    public static event InputControllerEvent Sprint;
    public static event InputControllerEvent Walk;
    public static event InputControllerEvent Jump;
    public static event InputControllerEvent Slide;
    public static event InputControllerEvent Pause;


    private PlayerInputs _playerInputs;




    private void Awake()
    {
        _playerInputs = new PlayerInputs();
    }
    private void Start()
    {
        GetInputs();
    }
    private void Update()
    {
        GetMovementVector();
        GetMousePosition();
    }



    private void GetInputs()
    {
        _playerInputs.Player.Sprint.started += ctx => Sprint();
        _playerInputs.Player.Sprint.canceled += ctx => Walk();
        _playerInputs.Player.Jump.performed += ctx => Jump();
        _playerInputs.Player.Slide.performed += ctx => Slide();
        _playerInputs.Player.Esc.performed += ctx => Pause();
    }


    private void GetMovementVector()
    {
        Vector2 movementVectorTemp = _playerInputs.Player.Move.ReadValue<Vector2>();
        _movementVector = new Vector3(movementVectorTemp.x, 0f, movementVectorTemp.y);
    }
    private void GetMousePosition()
    {
        RaycastHit mouseRayHit;
        Ray mouseRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out mouseRayHit)) _mousePosition = mouseRayHit.point;
    }



    private void OnEnable()
    {
        _playerInputs.Enable();
    }
    private void OnDisable()
    {
        _playerInputs.Disable();
    }
}
