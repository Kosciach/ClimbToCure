using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("====Reference====")]
    [SerializeField] Camera _playerCamera;




    public void RotatePlayer()
    {
        Vector3 cameraRotationVector = _playerCamera.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, cameraRotationVector.y, 0f);
    }
}
