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


    [Space(20)]
    [Header("====Debugs====")]
    [SerializeField] int _pathIndex;


    [Space(20)]
    [Header("====Settings====")]
    [SerializeField] string _pathKey;



    private void NewGame()
    {
        PlayerPrefs.SetInt(_pathKey, 0);
        _pathIndex = 0;
        _currentPath = Instantiate(_paths[_pathIndex], Vector3.zero, Quaternion.identity);
    }
    private void Continue()
    {
        if (!PlayerPrefs.HasKey(_pathKey)) PlayerPrefs.SetInt(_pathKey, 0);

        _pathIndex = PlayerPrefs.GetInt(_pathKey);

        _currentPath = Instantiate(_paths[_pathIndex], Vector3.zero, Quaternion.identity);
    }


    public void SwitchPath()
    {
        if (_pathIndex == _paths.Length - 1) return;

        //Spawn new path
        _pathIndex++;
        PlayerPrefs.SetInt(_pathKey, _pathIndex);

        _oldPath = _currentPath;
        _currentPath = Instantiate(_paths[_pathIndex], Vector3.zero, Quaternion.identity);

        //Teleport player to starting platform of new path
        _player.transform.position = new Vector3(0f, 0.5f, 0f);

        //Remove old path
        Destroy(_oldPath);
    }

    public void Fall()
    {
        if (_pathIndex == 0) return;


        _pathIndex--;
        PlayerPrefs.SetInt(_pathKey, _pathIndex);

        _oldPath = _currentPath;
        _currentPath = Instantiate(_paths[_pathIndex], Vector3.zero, Quaternion.identity);

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
