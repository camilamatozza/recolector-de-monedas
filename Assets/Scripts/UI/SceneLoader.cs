using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public const string Splash = "Splash";
    public const string MainMenu = "MainMenu";
    public const string Gameplay = "Gameplay";

    public static void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
