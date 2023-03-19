using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [Header("====References====")]
    [SerializeField] CameraController _cameraController;
    [SerializeField] UIController _UIController;


    [Space(20)]
    [Header("====CanvasStuff====")]
    [SerializeField] GameObject[] _menus;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _healthMenu;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] GameObject _fakeGround;


    public delegate void CanvasControllerEvent();
    public static event CanvasControllerEvent Resume;
    public static event CanvasControllerEvent NewGame;
    public static event CanvasControllerEvent Continue;
    public static event CanvasControllerEvent GoToMainMenu;

    public void NewGameButton()
    {
        _fakeGround.SetActive(false);
        Time.timeScale = 1;
        NewGame();
        _cameraController.MoveToGameplay();
        HideCanvasSmooth();
    }
    public void ContinueButton()
    {
        _fakeGround.SetActive(false);
        Time.timeScale = 1;
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
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        _canvasGroup.alpha = 0;

        _cameraController.MoveToMainMenu();
        _fakeGround.SetActive(true);
        LeanTween.value(_canvasGroup.alpha, 1, 0.3f).setOnUpdate((float val) =>
        {
            _canvasGroup.alpha = val;
        }).setOnComplete(() =>
        {
            ChangeMenuButton(_menus[0]);
            _canvasGroup.alpha = 1;
        });
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
