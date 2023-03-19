using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [Header("====References====")]
    [SerializeField] CameraController _cameraController;


    [Space(20)]
    [Header("====CanvasStuff====")]
    [SerializeField] GameObject[] _menus;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] CanvasGroup _canvasGroup;


    public delegate void CanvasControllerEvent();
    public static event CanvasControllerEvent Resume;
    public static event CanvasControllerEvent NewGame;
    public static event CanvasControllerEvent Continue;
    public static event CanvasControllerEvent GoToMainMenu;

    public void NewGameButton()
    {
        NewGame();
        _cameraController.MoveToGameplay();
        HideCanvasSmooth();
    }
    public void ContinueButton()
    {
        Continue();
        _cameraController.MoveToGameplay();
        HideCanvasSmooth();
    }


    public void ChangeMenuButton(GameObject choosenMenu)
    {
        foreach(GameObject menu in _menus) menu.SetActive(false);

        choosenMenu.SetActive(true);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void MainMenuButton()
    {
        GoToMainMenu();
        ChangeMenuButton(_menus[0]);
    }
    public void ResumeButton()
    {
        Resume();
    }



    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }
    public void TogglePause(bool enable)
    {
        _pauseMenu.SetActive(enable);
    }

    private void HideCanvasSmooth()
    {
        LeanTween.value(_canvasGroup.alpha, 0, 0.3f).setOnUpdate((float val) =>
        {
            _canvasGroup.alpha = val;
        }).setOnComplete(() =>
        {
            foreach (GameObject menu in _menus) menu.SetActive(false);
            _canvasGroup.alpha = 1;
        });
    }
}
