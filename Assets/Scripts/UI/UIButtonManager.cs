using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void StartButtonClick()
    {
        SceneLoader.Instance.LoadScene(MyScenes.Game);
    }
    public void CreditsButtonClick()
    {
        SceneLoader.Instance.LoadScene(MyScenes.Credits);
    }
    public void QuitButtonClick()
    {
#if UNITY_EDITOR
        EditorApplication.Exit(0);
#endif
        Application.Quit();
    }
    public void BackButtonClick()
    {
        SceneLoader.Instance.LoadScene(MyScenes.MainMenu);
    }
}
