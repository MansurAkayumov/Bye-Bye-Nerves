using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _audioObject;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _cameraFade;

    private void Start()
    {
        Invoke("HideCameraFade", 1f);
    }

    public void Update()
    {
        if(Input.GetKey(KeyCode.R))
            Restart();
    }

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
        SpawnAudioObject(2);
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        SpawnAudioObject(2);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SpawnAudioObject(2);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        SpawnAudioObject(2);
    }

    private void HideCameraFade()
    {
        _cameraFade.SetActive(false);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
        SpawnAudioObject(2);
    }

    public void Exit()
    {
        Application.Quit();
        SpawnAudioObject(2);
    }

    private void SpawnAudioObject(int number)
    {
        GameObject audioObject = Instantiate(_audioObject, Vector3.zero, Quaternion.identity);
        audioObject.GetComponent<AudioSource>().clip = audioObject.GetComponent<AudioPlayer>()._clips[number];
    }
}
