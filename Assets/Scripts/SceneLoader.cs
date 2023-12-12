using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int levelToLoad;

    public void LevelCompleted()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int levelToUnlock = PlayerPrefs.GetInt("LevelToUnlock");

        if(levelToUnlock < currentSceneIndex)
        {
            PlayerPrefs.SetInt("LevelToUnlock", currentSceneIndex);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
