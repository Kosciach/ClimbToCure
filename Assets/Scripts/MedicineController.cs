using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineController : MonoBehaviour
{
    [Header("====Settings====")]
    [Range(0, 100)]
    [SerializeField] float _rotationSpeed;






    private void Update()
    {
        Animate();
    }




    private void Animate()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }
}
