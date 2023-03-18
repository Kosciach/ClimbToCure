using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("====References====")]
    [SerializeField] InputController _inputController;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Transform _groundCheck;
    [SerializeField] PlayerStateMachine _playerStateMachine;

    [Space(20)]
    [Header("====Debug====")]
    [SerializeField] float _speed;
    [SerializeField] bool _isGrounded;

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
    [Range(0, 20)]
    [SerializeField] float _jumpForce;


    private Vector3 _movementVectorLerped;
    private Vector3 _slideVector;
    private float _onGroundSpeed;


    private void Update()
    {
        CheckGround();
    }





    public void Movement()
    {
        Vector3 movementVector = (transform.forward * _inputController.MovementVectorInput.z + transform.right * _inputController.MovementVectorInput.x) * _speed;

        _movementVectorLerped = Vector3.Lerp(_movementVectorLerped, movementVector, _accelarationSpeed * Time.deltaTime);

        Vector3 correctedMovementVector = new Vector3(_movementVectorLerped.x, _rigidbody.velocity.y, _movementVectorLerped.z);


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
        _slideVector = (transform.forward * _inputController.MovementVectorInput.z + transform.right * _inputController.MovementVectorInput.x) * _speed;
    }

    public void Slide()
    {
        _rigidbody.velocity = _slideVector;
    }
    public void RestoreSpeed()
    {
        _speed = _onGroundSpeed;
    }



    public bool GetIsGrounded()
    {
        return _isGrounded;
    }


    private void SetSprint()
    {
        _onGroundSpeed = _sprintSpeed;
        if (_playerStateMachine.Switch.Slide) return;
        _speed = _sprintSpeed;
    }
    private void SetWalk()
    {
        _onGroundSpeed = _walkSpeed;
        if (_playerStateMachine.Switch.Slide) return;
        _speed = _walkSpeed;
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
