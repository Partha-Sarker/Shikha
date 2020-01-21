using UnityEngine;
using UnityEngine.SceneManagement;

public class FenceObstacle : MonoBehaviour
{
    [SerializeField]
    private SceneLoader sceneLoader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex, "You've been spotted.");
        }
    }

}
