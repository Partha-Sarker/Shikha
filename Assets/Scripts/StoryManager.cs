using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public int nextSceneIndex = 1;

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
