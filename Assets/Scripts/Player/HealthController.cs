using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("====References====")]
    [SerializeField] UIController _UIController;
    [SerializeField] PlayerStateMachine _playerStateMachine;


    [Space(20)]
    [Header("====Debugs====")]
    [Range(0, 100)]
    [SerializeField] float _health;


    [Space(20)]
    [Header("====Settings====")]
    [Range(0, 100)]
    [SerializeField] float _poisonSpeed;



    public void ResetHealth()
    {
        _health = 100;
        _UIController.UpdateHealth(_health);
    }
    public void Poisoning()
    {
        _health -= _poisonSpeed;
        _health = Mathf.Clamp(_health, 0, 100);

        _UIController.UpdateHealth(_health);

        if (_health <= 0) _playerStateMachine.Switch.Fall = true;
    }
}
