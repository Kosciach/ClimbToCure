using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScripts : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] float _bounceStrength; public float BounceStrength { get { return _bounceStrength; } }
}
