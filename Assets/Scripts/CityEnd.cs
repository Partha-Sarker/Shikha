using UnityEngine;
using UnityEngine.SceneManagement;

public class CityEnd : MonoBehaviour
{
    [SerializeField]
    private SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sceneLoader.LoadScene("Story", "THE END");
        }
    }
}
