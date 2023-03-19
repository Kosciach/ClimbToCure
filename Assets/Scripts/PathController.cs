using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [Header("====Path====")]
    [SerializeField] GameObject[] _paths;
    [SerializeField] GameObject _currentPath;
    [SerializeField] GameObject _oldPath;


    [Space(20)]
    [Header("====References====")]
    [SerializeField] Transform _player;
    [SerializeField] GameObject _startingPlatformPrefab;
    [SerializeField] GameObject _startingPlatform;
    [SerializeField] HealthController _healthController;


    [Space(20)]
    [Header("====Debugs====")]
    [SerializeField] int _pathIndex;


    [Space(20)]
    [Header("====Settings====")]
    [SerializeField] string _pathKey;
    [SerializeField] float[] _poisonSpeeds;



    private void NewGame()
    {
        PlayerPrefs.SetInt(_pathKey, 0);
        _pathIndex = 0;
        _currentPath = Instantiate(_paths[_pathIndex], Vector3.zero, Quaternion.identity);
        _healthController.SetPoisonSpeed(_poisonSpeeds[_pathIndex]);
    }
    private void Continue()
    {
        if (!PlayerPrefs.HasKey(_pathKey)) PlayerPrefs.SetInt(_pathKey, 0);

        _pathIndex = PlayerPrefs.GetInt(_pathKey);

        _currentPath = Instantiate(_paths[_pathIndex], Vector3.zero, Quaternion.identity);
        _healthController.SetPoisonSpeed(_poisonSpeeds[_pathIndex]);
    }


    public void SwitchPath()
    {
        if (_pathIndex == _paths.Length - 1) return;

        if (_startingPlatform != null) Destroy(_startingPlatform);
        //Spawn new path
        _pathIndex++;
        PlayerPrefs.SetInt(_pathKey, _pathIndex);

        _oldPath = _currentPath;
        _currentPath = Instantiate(_paths[_pathIndex], _oldPath.transform.GetChild(1).position, Quaternion.identity);
        _healthController.SetPoisonSpeed(_poisonSpeeds[_pathIndex]);

        //Remove old path
        Destroy(_oldPath);
    }

    public void Fall()
    {
        if (_pathIndex == 0) return;

        if (_pathIndex == 1) _startingPlatform = Instantiate(_startingPlatformPrefab, Vector3.zero, Quaternion.identity);

        _pathIndex--;
        PlayerPrefs.SetInt(_pathKey, _pathIndex);

        _oldPath = _currentPath;
        _currentPath = Instantiate(_paths[_pathIndex], Vector3.zero, Quaternion.identity);
        _healthController.SetPoisonSpeed(_poisonSpeeds[_pathIndex]);

        Destroy(_oldPath);
    }


    private void OnEnable()
    {
        CanvasController.NewGame += NewGame;
        CanvasController.Continue += Continue;
    }
    private void OnDisable()
    {
        CanvasController.NewGame -= NewGame;
        CanvasController.Continue -= Continue;
    }
}
