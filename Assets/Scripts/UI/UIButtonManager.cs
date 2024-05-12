using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private UnityEngine.UIElements.UIDocument document;

    private Button startButton;
    private Button creditsButton;
    private Button quitButton;
    private Button backButton;

    private void Start()
    {
        document = GetComponent<UIDocument>();
        startButton = document.rootVisualElement.Q<Button>("Start");
        if (startButton != null)
            startButton.clicked += StartButtonClick;

        creditsButton = document.rootVisualElement.Q<Button>("Credits");
        if (creditsButton != null)
            creditsButton.clicked += CreditsButtonClick;

        quitButton = document.rootVisualElement.Q<Button>("Quit");
        if (quitButton != null)
            quitButton.clicked += QuitButtonClick;

        backButton = document.rootVisualElement.Q<Button>("Back");
        if (backButton != null)
            backButton.clicked += BackButtonClick;
    }

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
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    public void BackButtonClick()
    {
        SceneLoader.Instance.LoadScene(MyScenes.MainMenu);
    }
}
