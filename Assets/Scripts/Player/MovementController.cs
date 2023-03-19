using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("====References====")]
    [SerializeField] InputController _inputController;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Transform _groundCheck;
    [SerializeField] PlayerStateMachine _playerStateMachine;
    [SerializeField] CinemachineVirtualCamera _cineCamera;
    [SerializeField] ParticleSystem _groundParticle;
    [SerializeField] Material _groundParticleMaterial;

    [Space(20)]
    [Header("====Debug====")]
    [SerializeField] float _speed;
    [SerializeField] bool _isGrounded;
    [SerializeField] bool _isIce;

    [Space(20)]
    [Header("====Settings====")]
    [SerializeField] LayerMask _groundMask;
    [Range(0, 1)]
    [SerializeField] float _groundCheckRadius;
    [Range(0, 20)]
    [SerializeField] float _walkSpeed;
    [Range(0, 20)]
    [SerializeField] float _sprintSpeed;
    [Range(0, 20)]
    [SerializeField] float _slideSpeed;
    [Range(0, 20)]
    [SerializeField] float _accelarationSpeed;
    [Space(5)]
    [Range(0, 20)]
    [SerializeField] float _jumpForce;
    [Space(5)]
    [Range(0, 200)]
    [SerializeField] float _walkFov;
    [Range(0, 200)]
    [SerializeField] float _sprintFov;
    [Range(0, 200)]
    [SerializeField] float _slideFov;
    [Space(5)]
    [SerializeField] LayerMask _iceMask;


    private Vector3 _movementVectorLerped;
    private Vector3 _slideVector;
    private float _onGroundSpeed;
    private float _onGroundFov;
    private CinemachinePOV _cinePOV;
    private Material _groundMaterial;


    private void Awake()
    {
        _cinePOV = _cineCamera.GetCinemachineComponent<CinemachinePOV>();
    }
    private void Start()
    {
        _onGroundSpeed = _walkSpeed;
        _onGroundFov = _walkFov;
    }
    private void Update()
    {
        CheckGround();
        CheckIce();
        CheckGroundMaterial();
    }





    public void Movement()
    {
        Vector3 movementVector = (transform.forward * _inputController.MovementVectorInput.z + transform.right * _inputController.MovementVectorInput.x) * _speed;

        _movementVectorLerped = Vector3.Lerp(_movementVectorLerped, movementVector, _accelarationSpeed * Time.deltaTime);

        Vector3 correctedMovementVector = new Vector3(_movementVectorLerped.x, _rigidbody.velocity.y, _movementVectorLerped.z);

        if (_isGrounded && _movementVectorLerped.magnitude > 0) Instantiate(_groundParticle, transform.position, Quaternion.identity);

        _rigidbody.velocity = correctedMovementVector;
    }



    private void CheckGround()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundMask);
    }
    public void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }



    public void GetSlideVector()
    {
        _speed = _slideSpeed;
        Vector3 slideVectorRaw = (transform.forward * _inputController.MovementVectorInput.z + transform.right * _inputController.MovementVectorInput.x) * _speed;
        _slideVector = new Vector3(slideVectorRaw.x, _rigidbody.velocity.y, slideVectorRaw.z);

        LeanTween.value(_cineCamera.m_Lens.FieldOfView, _slideFov, 0.1f).setOnUpdate((float val) =>
        {
            _cineCamera.m_Lens.FieldOfView = val;
        });
    }

    public void Slide()
    {
        _rigidbody.velocity = _slideVector;
    }
    public void RestoreSpeed()
    {
        LeanTween.value(_speed, _onGroundSpeed, 2f).setOnUpdate((float val) =>
        {
            _speed = val;
        });
        LeanTween.value(_cineCamera.m_Lens.FieldOfView, _onGroundFov, 0.1f).setOnUpdate((float val) =>
        {
            _cineCamera.m_Lens.FieldOfView = val;
        });
    }



    public bool GetIsGrounded()
    {
        return _isGrounded;
    }
    private void CheckIce()
    {
        _isIce = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _iceMask);
        _accelarationSpeed = _isIce ? 1 : 7;
    }
    private void CheckGroundMaterial()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, _groundMask))
        {
            _groundMaterial = hit.transform.gameObject.GetComponent<Renderer>().sharedMaterial;
            _groundParticleMaterial.color = _groundMaterial.color - new Color(0.2f, 0.2f, 0.2f);
        }
    }


    private void SetSprint()
    {
        _onGroundSpeed = _sprintSpeed;
        _onGroundFov = _sprintFov;

        if (_playerStateMachine.Switch.Slide) return;

        if (!_isGrounded) return;

        _speed = _sprintSpeed;
        LeanTween.value(_cineCamera.m_Lens.FieldOfView, _sprintFov, 0.1f).setOnUpdate((float val) =>
        {
            _cineCamera.m_Lens.FieldOfView = val;
        });
    }
    private void SetWalk()
    {
        _onGroundSpeed = _walkSpeed;
        _onGroundFov = _walkFov;

        if (_playerStateMachine.Switch.Slide) return;

        if (!_isGrounded) return;

        _speed = _walkSpeed;
        LeanTween.value(_cineCamera.m_Lens.FieldOfView, _walkFov, 0.1f).setOnUpdate((float val) =>
        {
            _cineCamera.m_Lens.FieldOfView = val;
        });
    }




    private void OnEnable()
    {
        InputController.Sprint += SetSprint;
        InputController.Walk += SetWalk;
    }
    private void OnDisable()
    {
        InputController.Sprint -= SetSprint;
        InputController.Walk -= SetWalk;
    }
}
