using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private float waitTime = 2f;
    [SerializeField]
    private TextMeshProUGUI captionText;
    [SerializeField]
    private Animator animator;

    public void LoadScene(string sceneName, string caption)
    {
        StartCoroutine(LoadingLevel(sceneName, caption));
    }

    public void LoadScene(int sceneID, string caption)
    {
        StartCoroutine(LoadingLevel(sceneID, caption));
    }

    public void LoadScene(string sceneName)
    {
        LoadScene(sceneName, "");
    }

    public void LoadScene(int sceneID)
    {
        LoadScene(sceneID, "");
    }

    IEnumerator LoadingLevel(string sceneName, string caption)
    {
        captionText.SetText(caption);
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadingLevel(int sceneID, string caption)
    {
        captionText.SetText(caption);
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneID);
    }

}
