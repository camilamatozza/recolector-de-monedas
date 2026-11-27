using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryCommand : ICommand
{
    public void Execute()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


public class BackToMenuCommand : ICommand
{
    public void Execute()
    {
        SceneLoader.Load(SceneLoader.MainMenu);
    }
}

public class ConfirmExitCommand : ICommand
{
    public void Execute()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}