using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
    public static int CurrentLevel = 1;
    private const string gameSceneName = "GameScene";
    private const string grandsonSceneName = "GrandsonScene";

    public static void StartLevel()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public static void GoToDialogue()
    {
        SceneManager.LoadScene(grandsonSceneName);
    }

    public static void NextLevel()
    {
        CurrentLevel++;
    }
}
