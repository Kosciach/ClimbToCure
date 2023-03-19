using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] string _currentStateName; public string CurrentStateName { get { return _currentStateName; } set { _currentStateName = value; } }
    private PlayerBaseState _currentState; public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }


    private PlayerStateFactory _stateFactory; public PlayerStateFactory StateFactory { get { return _stateFactory; } }





    [Space(20)]
    [Header("====PlayerScripts====")]
    [SerializeField] MovementController _movementController; public MovementController MovementController { get { return _movementController; } }
    [SerializeField] InputController _inputController; public InputController InputController { get { return _inputController;} }
    [SerializeField] CameraController _cameraController; public CameraController CameraController { get { return _cameraController; } }
    [SerializeField] HealthController _healthController; public HealthController HealthController { get { return _healthController; } }




    [Space(20)]
    [Header("====References====")]
    [SerializeField] Rigidbody _rigidbody; public Rigidbody Rigidbody { get { return _rigidbody; } }
    [SerializeField] CinemachineInputProvider _cineInput; public CinemachineInputProvider CineInput { get { return _cineInput; } set { _cineInput = value; } }
    [SerializeField] CinemachineVirtualCamera _cineCamera; public CinemachineVirtualCamera CineCamera { get { return _cineCamera; } set { _cineCamera = value; } }
    [SerializeField] PathController _pathController; public PathController PathController { get { return _pathController; } }
    [SerializeField] CanvasController _canvasController; public CanvasController CanvasController { get { return _canvasController; } }
    [SerializeField] UIController _UIController; public UIController UIController { get { return _UIController; } }




    [Space(20)]
    [Header("====Settings====")]
    [Range(0, 1)]
    [SerializeField] float _slideSize; public float SlideSize { get { return _slideSize; } }
    [Range(0, 100)]
    [SerializeField] float _slideTime; public float SlideTime { get { return _slideTime; } set { _slideTime = value; } }




    [Space(20)]
    [Header("====Debugs====")]
    [SerializeField] SwitchClass _switch; public SwitchClass Switch { get { return _switch; } set { _switch = value; } }
    [SerializeField] int _jumpCount; public int JumpCount { get { return _jumpCount; } set { _jumpCount = value; } }






    [System.Serializable]
    public class SwitchClass
    {
        public bool InAir;
        public bool Slide;
        public bool Medicine;
        public bool MainMenu;
        public bool Pause;
        public bool Fall;
    }

    private bool _isPause;



    private void Awake()
    {
        _stateFactory = new PlayerStateFactory(this);
        _currentState = _stateFactory.MainMenu();
        _currentState.StateEnter();
    }


    private void Update()
    {
        _currentState.StateUpdate();
        _currentState.StateCheckChange();
    }
    private void FixedUpdate()
    {
        _currentState.StateFixedUpdate();
    }









    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Medicine"))
        {
            Destroy(other.gameObject);
            _switch.Medicine = true;
            _rigidbody.velocity = Vector3.zero;
        }
        else if(other.CompareTag("Fall"))
        {
            _switch.Fall = true;
        }
    }










    private void SetJump()
    {
        if (_jumpCount >= 2) return;

        _jumpCount++;
        _switch.InAir = true;
        _switch.Slide = false;
        _movementController.Jump();
    }
    private void SetSlide()
    {
        _switch.Slide = true;
    }
    private void StartGame()
    {
        _switch.MainMenu = false;
    }

    private void Pause()
    {
        _isPause = !_isPause;

        Cursor.lockState = _isPause ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = _isPause;

        Time.timeScale = _isPause ? 0 : 1;

        _cineInput.enabled = !_isPause;
        _canvasController.TogglePause(_isPause);
    }
    private void Resume()
    {
        _isPause = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1;

        _cineInput.enabled = true;
        _canvasController.TogglePause(false);
    }
    private void GoToMainMenu()
    {
        _switch.MainMenu = true;
    }
    


    private void OnEnable()
    {
        InputController.Jump += SetJump;
        InputController.Slide += SetSlide;
        CameraController.GameStart += StartGame;
        InputController.Pause += Pause;
        CanvasController.Resume += Resume;
        CanvasController.GoToMainMenu += GoToMainMenu;
    }
    private void OnDisable()
    {
        InputController.Jump -= SetJump;
        InputController.Slide -= SetSlide;
        CameraController.GameStart -= StartGame;
        InputController.Pause -= Pause;
        CanvasController.Resume -= Resume;
        CanvasController.GoToMainMenu -= GoToMainMenu;
    }
}
