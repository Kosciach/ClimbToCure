using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] RectTransform _health;
    [SerializeField] GameObject _healthMenu;




    public void UpdateHealth(float health)
    {
        _health.localScale = new Vector3(health/100, 1f, 1f);
    }

    public void ToggleHealth(bool enable)
    {
        _healthMenu.SetActive(enable);
    }
}
