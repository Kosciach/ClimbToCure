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
    [SerializeField] float _accelarationSpeed;
    [Range(0, 20)]
    [SerializeField] float _jumpForce;


    private Vector3 _movementVectorLerped;



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


    private void Jump()
    {
        if (!_isGrounded) return;

        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }


    private void SetSprint()
    {
        _speed = _sprintSpeed;
    }
    private void SetWalk()
    {
        _speed = _walkSpeed;
    }




    private void OnEnable()
    {
        InputController.Sprint += SetSprint;
        InputController.Walk += SetWalk;
        InputController.Jump += Jump;
    }
    private void OnDisable()
    {
        InputController.Sprint -= SetSprint;
        InputController.Walk -= SetWalk;
        InputController.Jump -= Jump;
    }
}
