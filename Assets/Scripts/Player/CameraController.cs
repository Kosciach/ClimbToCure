using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [Header("====Reference====")]
    [SerializeField] Camera _playerCamera;
    [SerializeField] CinemachineVirtualCamera _cineCamera;
    [SerializeField] Transform _follow;


    [Space(20)]
    [Header("====Settings====")]
    [SerializeField] Vector3 _mainMenuPosition;
    [SerializeField] Vector3 _gameplayPosition;
    [Space(5)]
    [Range(0, 180)]
    [SerializeField] float _mainMenuAngle;
    [Range(0, 180)]
    [SerializeField] float _gameplayAngle;
    [Range(0, 1)]
    [SerializeField] float _cameraMoveSpeed;


    private CinemachinePOV _cinePOV;
    public delegate void CameraControllerEvent();
    public static event CameraControllerEvent GameStart;


    private void Awake()
    {
        _cinePOV = _cineCamera.GetCinemachineComponent<CinemachinePOV>();
    }



    public void MoveToGameplay()
    {
        _follow.LeanMoveLocal(_gameplayPosition, _cameraMoveSpeed).setEaseOutBounce().setOnComplete(() =>
        {
            _cinePOV.m_VerticalAxis.m_Wrap = false;
            _cinePOV.m_VerticalAxis.m_MinValue = -70;
            _cinePOV.m_VerticalAxis.m_MaxValue = 70;

            GameStart();
        });
        LeanTween.value(_cinePOV.m_VerticalAxis.Value, _gameplayAngle, _cameraMoveSpeed).setEaseOutBack().setOnUpdate((float val) =>
        {
            _cinePOV.m_VerticalAxis.Value = val;
        });
    }
    public void MoveToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void RotatePlayer()
    {
        Vector3 cameraRotationVector = _playerCamera.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, cameraRotationVector.y, 0f);
    }
}
