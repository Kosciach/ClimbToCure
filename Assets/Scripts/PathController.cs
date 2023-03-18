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
    [Header("====Debugs====")]
    [SerializeField] Transform _player;



    [Space(20)]
    [Header("====Debugs====")]
    [SerializeField] int _pathIndex;



    public void SwitchPath()
    {
        if (_pathIndex == _paths.Length - 1) return;

        //Spawn new path
        _pathIndex++;
        _oldPath = _currentPath;
        _currentPath = Instantiate(_paths[_pathIndex], Vector3.zero, Quaternion.identity);

        //Teleport player to starting platform of new path
        _player.transform.position = _currentPath.transform.GetChild(0).GetChild(0).position;

        //Remove old path
        Destroy(_oldPath);
    }
}
