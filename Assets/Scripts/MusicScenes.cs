using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScenes : MonoBehaviour
{
    private AudioSource _audioSource;

    private static MusicScenes instance = null;
    public static MusicScenes Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.buildIndex == 0 || currentScene.buildIndex == 8)
            Destroy(gameObject);
    }

    public void StopMusic()
    {
        _audioSource.Pause();
        Invoke("PlayMusic", 2f);
    }

    public void PlayMusic()
    {
        _audioSource.Play();
    }
}
