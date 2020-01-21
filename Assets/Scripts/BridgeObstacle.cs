using UnityEngine;
using UnityEngine.SceneManagement;

public class BridgeObstacle : MonoBehaviour
{
    [SerializeField]
    private SceneLoader sceneLoader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex, "You got badly injured");
        }
    }
}
