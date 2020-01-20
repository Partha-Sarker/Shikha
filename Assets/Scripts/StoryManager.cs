using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    SceneLoader sceneLoader;
    [SerializeField]
    private string nextLevelName = "Turorial";
    [SerializeField]
    private int nextLevelIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void LoadNextLevel()
    {
        sceneLoader.LoadScene(nextLevelName, "TUTORIAL");
    }
}
