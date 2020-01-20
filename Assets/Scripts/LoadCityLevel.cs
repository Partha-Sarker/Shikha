using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCityLevel : MonoBehaviour
{
    public SceneLoader sceneLoader;
    [SerializeField]
    private string cityLevelName = "City Level";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            sceneLoader.LoadScene(cityLevelName);
    }
}
