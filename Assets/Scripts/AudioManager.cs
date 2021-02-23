using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void MuteAudio()
    {
        AudioListener.volume = 0;
    }

    public void UnmuteAudio()
    {
        AudioListener.volume = 1;
    }
}
