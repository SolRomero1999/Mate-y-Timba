using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
    public static int CurrentLevel = 1;

    public static bool reglasEliminacionActivas = true;
    public static bool dialogoPostTutorial = false;

    private const string gameSceneTuto = "Gameplay_Tutorial_Abuelo";
    private const string grandsonSceneName = "GrandsonScene";
    private const string gameSceneName = "GameScene";

    public static void StartLevelTuto()
    {
        reglasEliminacionActivas = false;
        SceneManager.LoadScene(gameSceneTuto);
    }

    public static void GoToDialogue()
    {
        dialogoPostTutorial = false;
        SceneManager.LoadScene(grandsonSceneName);
    }

    public static void GoToDialogue_PostTutorial()
    {
        dialogoPostTutorial = true;
        SceneManager.LoadScene(grandsonSceneName);
    }

    public static void StartLevelNormal()
    {
        reglasEliminacionActivas = true;
        SceneManager.LoadScene(gameSceneName);
    }

    public static void NextLevel()
    {
        CurrentLevel++;
    }
}
